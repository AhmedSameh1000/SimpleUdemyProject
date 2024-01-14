using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CoursePriceForUpdate
    {
        public int CourseId { get; set; }
        public decimal Price { get; set; }
        public string InstructorId { get; set; }
    }
}