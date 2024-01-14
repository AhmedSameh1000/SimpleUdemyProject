using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.LectureDTOs
{
    public class LectureForReturnDTO
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string? Title { get; set; }
        public string? VideoSectionUrl { get; set; }
        public string? Description { get; set; }
        public int? Menutes { get; set; }
    }
}