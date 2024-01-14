using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CourseMessageForUpdateDTO
    {
        public int CourseId { get; set; }
        public string InstructorId { get; set; }

        public string WelcomeMessage { get; set; }
        public string CongratulationsMessage { get; set; }
    }
}