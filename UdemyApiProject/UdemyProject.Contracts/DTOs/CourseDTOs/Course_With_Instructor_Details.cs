using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class Course_With_Instructor_Details
    {
        public int courseId { get; set; }
        public string courseTitle { get; set; }
        public string courseSubTitle { get; set; }

        public string courseImage { get; set; }

        public int courseRating { get; set; }
        public DateTime lastUpdated { get; set; }

        public int duration { get; set; }
        public string languge { get; set; }
        public string description { get; set; }

        public bool isPaidforCurrentUser { get; set; }

        public InstructoreDetaisl instructoreDetaisl { get; set; }
        public List<string> courrseRequirments { get; set; }
        public List<string> whateyoulearn { get; set; }
        public List<string> whoIsCourseFor { get; set; }
        public List<courseContentSectionDto> contentSections { get; set; }
    }

    public class InstructoreDetaisl
    {
        public string instructorId { get; set; }
        public string name { get; set; }
        public string biography { get; set; }
        public string instructorImage { get; set; }
        public int courseCount { get; set; }
    }

    public class courseContentSectionDto
    {
        public int sectionId { get; set; }
        public string sectionTitle { get; set; }
        public int lectureCount { get; set; }

        public int totalMinutes { get; set; }

        public List<courseLectureContentDto> lectureContent { get; set; }
    }

    public class courseLectureContentDto
    {
        public int lectureId { get; set; }
        public string Lecturetitle { get; set; }

        public int totalMinutes { get; set; }
    }
}