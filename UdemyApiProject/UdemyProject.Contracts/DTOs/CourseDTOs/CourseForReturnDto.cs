using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CourseForReturnDTO
    {
        public CourseForReturnDTO(
            int id,
            string title,
            string subTitle,
            decimal price,
            string intrcutorName,
            string instructorId,
            string courseimage,
            int lectureCount,
            int totalMinutes)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            Price = price;
            IntrcutorName = intrcutorName;
            InstructorId = instructorId;
            CourseImage = courseimage;
            LectureCount = lectureCount;
            this.totalMinutes = totalMinutes;
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public decimal Price { get; set; }

        public int totalMinutes { get; }
        public string CourseImage { get; set; }
        public int LectureCount { get; }
        public string IntrcutorName { get; set; }
        public string InstructorId { get; set; }
    }
}