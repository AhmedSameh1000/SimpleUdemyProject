using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Contracts.DTOs.UserProfileDTOs
{
    public class UserProfileDTO
    {
        public string applicationUserId { get; set; }

        public string FullName { get; set; }

        public string? Headline { get; set; }

        public string? Biography { get; set; }

        public string? WebsiteUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? YoutubeUrl { get; set; }
    }
}