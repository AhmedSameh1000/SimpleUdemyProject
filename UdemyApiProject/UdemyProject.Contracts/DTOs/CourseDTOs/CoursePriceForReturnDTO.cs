using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CourseDTOs
{
    public class CoursePriceForReturnDTO
    {

        public int CourseId { get; set; }
        public decimal? Price { get; set; }
    }
}
