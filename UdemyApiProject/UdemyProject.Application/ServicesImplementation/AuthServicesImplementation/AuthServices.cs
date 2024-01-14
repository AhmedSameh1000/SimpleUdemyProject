using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.AuthDTOs;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.Constant;
using UdemyProject.Infrastructure.Helpers;

namespace UdemyProject.Application.ServicesImplementation.AuthServicesImplementation
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRefreshTokenRepository _RefreshTokenRepository;
        private readonly JWT _jwt;

        public AuthServices(UserManager<ApplicationUser> userManager,
            IOptions<JWT> jwt, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _RefreshTokenRepository = refreshTokenRepository;
            _jwt = jwt.Value;
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.ToLocalTime().AddMinutes(1),
                signingCredentials: signingCredentials);

            var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Token;
        }

        public async Task<AuthModel> LoginAsync(LogInDTo model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var RefreshToken = await _RefreshTokenRepository.GetFirstOrDefault(c => c.RevokedOn == null && c.UserId == user.Id, new[] { "User" });

            if (RefreshToken is null)
            {
                var newRefreshToken = GenerateNewRefreshToken(user.Id);
                await _RefreshTokenRepository.Add(newRefreshToken);

                await _RefreshTokenRepository.SaveChanges();

                return await GetAuthModel(user, newRefreshToken);
            }
            else
            {
                return await RefreshTokenAsync(user.Id);
            }
        }

        public async Task<AuthModel> RefreshTokenAsync(string userId)
        {
            var CurrentRefreshToken = await _RefreshTokenRepository.GetFirstOrDefault(c => c.RevokedOn == null && c.UserId == userId, new[] { "User" });
            if (CurrentRefreshToken is null || CurrentRefreshToken.User is null)
            {
                return new AuthModel()
                {
                    Message = "Invalid token"
                };
            }
            var user = CurrentRefreshToken.User;

            if (CurrentRefreshToken.ExpiresOn.ToLocalTime() > DateTime.UtcNow.ToLocalTime())
            {
                return await GetAuthModel(user, CurrentRefreshToken);
            }
            else
            {
                CurrentRefreshToken.RevokedOn = DateTime.UtcNow;

                _RefreshTokenRepository.Update(CurrentRefreshToken);

                var newRefreshToken = GenerateNewRefreshToken(user.Id);

                await _RefreshTokenRepository.Add(newRefreshToken);
                await _RefreshTokenRepository.SaveChanges();

                return await GetAuthModel(user, newRefreshToken);
            }
        }

        private async Task<AuthModel> GetAuthModel(ApplicationUser user, UserRefreshToken refreshToken)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return new AuthModel()
            {
                Token = await GenerateToken(user),
                Email = user.Email,
                Username = user.UserName,
                Roles = roles.ToList(),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn.ToLocalTime(),
                refreshTokenId = refreshToken.Id,
                IsAuthenticated = true
            };
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            var user = new ApplicationUser
            {
                Name = model.FullName,
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var Token = await this.GenerateToken(user);

            return new AuthModel
            {
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { RolesType.User },
                Token = Token,
                Username = user.UserName,
            };
        }

        private UserRefreshToken GenerateNewRefreshToken(string UserId)
        {
            TimeSpan refreshTokenExpiration = TimeSpan.FromMinutes(2);
            return new UserRefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString(),
                ExpiresOn = DateTime.UtcNow.Add(refreshTokenExpiration),
                CreatedOn = DateTime.UtcNow,
                UserId = UserId,
            };
        }
    }
}