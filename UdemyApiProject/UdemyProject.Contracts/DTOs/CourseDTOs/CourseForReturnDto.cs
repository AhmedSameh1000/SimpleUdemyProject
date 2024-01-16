using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CourseForReturnDTO
    {
        public CourseForReturnDTO(int id, string title, string subTitle, decimal price, string intrcutorName, string instructorId, string courseimage)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            Price = price;
            IntrcutorName = intrcutorName;
            InstructorId = instructorId;
            CourseImage = courseimage;
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public decimal Price { get; set; }

        public string CourseImage { get; set; }
        public string IntrcutorName { get; set; }
        public string InstructorId { get; set; }
    }
}