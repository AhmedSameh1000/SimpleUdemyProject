using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.UserProfileDTOs
{
    public class ChangePasswrodDTO
    {
        public string UserId { get; set; }
        public string CurrntPassword { get; set; }
        public string newPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}