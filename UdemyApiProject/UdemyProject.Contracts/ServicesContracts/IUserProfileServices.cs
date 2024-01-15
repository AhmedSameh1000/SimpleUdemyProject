using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.UserProfileDTOs;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface IUserProfileServices
    {
        Task<bool> UpdateUserprofile(UserProfileDTO userProfile);

        Task<UserProfileDTO> GetUserprofile(string userId);

        Task<bool> UploadProfileImage(UploadUserprofileDTO userprofileDTO);

        Task<string> GetUserProfileImage(string userId);

        Task<ResultOfChangePassword> ChangePassword(ChangePasswrodDTO passwrodDTO);
    }
}