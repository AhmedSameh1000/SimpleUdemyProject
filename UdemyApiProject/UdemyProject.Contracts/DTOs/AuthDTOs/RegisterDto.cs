using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}