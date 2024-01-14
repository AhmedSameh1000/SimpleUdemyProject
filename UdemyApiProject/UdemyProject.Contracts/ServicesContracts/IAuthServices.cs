using UdemyProject.Contracts.DTOs.AuthDTOs;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);

        Task<AuthModel> LoginAsync(LogInDTo model);

        Task<string> GenerateToken(ApplicationUser user);

        Task<AuthModel> RefreshTokenAsync(string userId);
    }
}