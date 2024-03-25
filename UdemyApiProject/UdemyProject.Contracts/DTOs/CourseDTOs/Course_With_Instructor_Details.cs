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

        public bool isInMylearning { get; set; }
        public int studentInThisCourseCount { get; set; }



        public int duration { get; set; }
        public string languge { get; set; }
        public decimal coursePrice { get; set; }
        public string description { get; set; }
        public bool isInCart { get; set; }

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
        public string headline { get; set; }
        public SocialAccount socialAccount { get; set; }

        public int totalStudents {  get; set; }
        public int totalReviews {  get; set; }
    }


    public class SocialAccount
    {
        public string userId { get; set; }

        public string twiter {  get; set; }
        public string facebook { get; set; }

        public string youtube { get; set; }

        public string linkedIn { get; set; }
    }

    public class courseContentSectionDto
    {
        public int sectionId { get; set; }
        public string sectionTitle { get; set; }
        public int lectureCount { get; set; }

        public int totalMinutes { get; set; }

        public List<courseLectureContentDto> lectureContent { get; set; }
    }

    public class ContantStartDToForReturn
    {
        public int rating { get; set; }
        public int lectureCount { get; set; }
        public int totalMinutes { get; set; }
        public List<CourseReviewDtoForReturn> courseReviews { get; set; }
        public List<courseContentSectionDto> courseContentSection { get; set; }
   
    
        public AboutCourseDto aboutCourseDto { get; set; }
    }


    public class courseLectureContentDto
    {
        public int lectureId { get; set; }
        public string Lecturetitle { get; set; }

        public int totalMinutes { get; set; }
    }

    public class CourseReviewDtoForReturn
    {
        public string userName { get; set; }
        public string userImagePath { get; set; }
        public int stars { get; set; }
        public string text { get; set; }
    }
}