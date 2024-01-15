using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.UserProfileDTOs
{
    public class ResultOfChangePassword
    {
        public bool isSucceded { get; set; }
        public string Message { get; set; }
    }
}