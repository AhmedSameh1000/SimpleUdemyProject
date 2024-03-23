using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int stars { get; set; }
        public int courseId { get; set; }
        public string userId { get; set; }

        public Course course { get; set; }

        public ApplicationUser user { get; set; }
    }
}