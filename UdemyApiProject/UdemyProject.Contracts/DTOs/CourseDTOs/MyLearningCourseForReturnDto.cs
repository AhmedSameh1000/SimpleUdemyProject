using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class MyLearningCourseForReturnDto
    {
        public int courseId { get; set; }
    
        public string instructorId { get; set; }
        public string courseName { get; set; }
        public string instructorName { get; set; }
        public int rating { get; set; }
        public string courseImage {  get; set; }
    }
}
