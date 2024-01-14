using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.SectionDTOs
{
    public class SectionForUpdateDTO
    {
        public int CourseId { get; set; }
        public int SectionId { get; set; }
        public string? SectionTitle { get; set; }
        public string? SectionDescription { get; set; }
    }
}