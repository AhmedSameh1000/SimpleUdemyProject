using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class AboutCourseDto
    {

        public int studentsCount { get; set; }
        public string languge { get; set; }


        public string discription {  get; set; }

        public string headline {  get; set; }

        public List<string> whatWillYouLearn {  get; set; }
   
        public InstructoreDetaisl instructoreDetails { get; set; }
    }

}