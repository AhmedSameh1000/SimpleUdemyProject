using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.UserProfileDTOs;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.ServicesImplementation.UserProfileimplementation
{
    public class UserProfileServices : IUserProfileServices
    {
        private readonly IUserProfileRepository _UserProfileRepository;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IFileServices _FileServices;
        private readonly IWebHostEnvironment _Host;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public UserProfileServices(
            IUserProfileRepository userProfileRepository,
            UserManager<ApplicationUser> userManager,
            IFileServices fileServices,
            IWebHostEnvironment Host,
            IHttpContextAccessor httpContextAccessor)
        {
            _UserProfileRepository = userProfileRepository;
            _UserManager = userManager;
            _FileServices = fileServices;
            _Host = Host;
            _HttpContextAccessor = httpContextAccessor;
        }

        public async Task<UserProfileDTO> GetUserprofile(string userId)
        {
            var UserProfile = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == userId);
            if (UserProfile is null)
            {
                return null;
            }
            var UserForReturn = new UserProfileDTO()
            {
                applicationUserId = userId,
                Biography = UserProfile.Biography,
                FacebookUrl = UserProfile.FacebookUrl,
                FullName = UserProfile.FullName,
                Headline = UserProfile.Headline,
                LinkedInUrl = UserProfile.LinkedInUrl,
                TwitterUrl = UserProfile.TwitterUrl,
                WebsiteUrl = UserProfile.WebsiteUrl,
                YoutubeUrl = UserProfile.YoutubeUrl,
            };

            return UserForReturn;
        }

        public async Task<bool> UpdateUserprofile(UserProfileDTO userProfileModel)
        {
            var User = await _UserManager.FindByIdAsync(userProfileModel.applicationUserId);

            if (User is null)
            {
                return false;
            }
            User.Name = userProfileModel.FullName;
            await _UserManager.UpdateAsync(User);

            var UserProfile = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == userProfileModel.applicationUserId);

            UserProfile.FullName = userProfileModel.FullName;
            UserProfile.Headline = userProfileModel.Headline;
            UserProfile.Biography = userProfileModel.Biography;
            UserProfile.TwitterUrl = userProfileModel.TwitterUrl;
            UserProfile.FacebookUrl = userProfileModel.FacebookUrl;
            UserProfile.YoutubeUrl = userProfileModel.YoutubeUrl;
            UserProfile.LinkedInUrl = userProfileModel.LinkedInUrl;
            UserProfile.WebsiteUrl = userProfileModel.WebsiteUrl;
            _UserProfileRepository.Update(UserProfile);
            return await _UserProfileRepository.SaveChanges();
        }

        public async Task<bool> UploadProfileImage(UploadUserprofileDTO userprofileDTO)
        {
            var UserProfile = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == userprofileDTO.userId);
            if (UserProfile is null)
            {
                return false;
            }
            if (userprofileDTO.Image != null)
            {
                if (UserProfile.ImageUrl != null)
                {
                    _FileServices.DeleteFile("UsersImagesProfile", UserProfile.ImageUrl);
                }
                var Result = _FileServices.SaveFile(userprofileDTO.Image, Path.Combine(_Host.WebRootPath, "UsersImagesProfile"));

                UserProfile.ImageUrl = Result.Path;
            }

            _UserProfileRepository.Update(UserProfile);
            return await _UserProfileRepository.SaveChanges();
        }

        public async Task<string> GetUserProfileImage(string userId)
        {
            var UserProfile = await _UserProfileRepository.GetFirstOrDefault(c => c.applicationUserId == userId);

            if (UserProfile == null || UserProfile.ImageUrl == null)
                return null;
            return Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "UsersImagesProfile", UserProfile.ImageUrl);
        }

        public async Task<ResultOfChangePassword> ChangePassword(ChangePasswrodDTO passwrodDTO)
        {
            var User = await _UserManager.FindByIdAsync(passwrodDTO.UserId);

            if (User == null)
                return null;

            var ResultCheckPassword = await _UserManager.CheckPasswordAsync(User, passwrodDTO.CurrntPassword);

            if (!ResultCheckPassword)
            {
                return new ResultOfChangePassword()
                {
                    isSucceded = false,
                    Message = "Password is Wrong"
                };
            }
            var ResultOFChangePassword = await _UserManager.ChangePasswordAsync(User, passwrodDTO.CurrntPassword, passwrodDTO.newPassword);
            if (!ResultOFChangePassword.Succeeded)
            {
                return new ResultOfChangePassword()
                {
                    isSucceded = false,
                    Message = "Bad Request"
                };
            }
            return new ResultOfChangePassword()
            {
                isSucceded = true,
                Message = "Password Changed Succesfuly"
            };
        }
    }
}