using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.LectureDTOs
{
    public class LectureForUpdateDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [AllowedExtensionsAttributes(".mp4,.webm")]
        public IFormFile? Video { get; set; }
    }
}