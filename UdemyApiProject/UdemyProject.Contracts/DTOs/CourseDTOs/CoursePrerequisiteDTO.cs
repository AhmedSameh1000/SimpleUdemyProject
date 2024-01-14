using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.Course;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CoursePrerequisiteDTO
    {
        public int Id { get; set; }
        public List<string> Requiments { get; set; }
        public List<string> WhateYouLearnFromCourse { get; set; }

        public List<string> WhoIsCourseFor { get; set; }
    }
}