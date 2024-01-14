using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CourseLandingPageForReturnDTO
    {

        public int CourseId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int LangugeId { get; set; }
        public string CourseImage { get; set; }
    }
}