using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.UserProfileDTOs
{
    public class UploadUserprofileDTO
    {
        public string userId { get; set; }

        public IFormFile Image { get; set; }
    }
}