using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.Helpers
{
    public class PaginationQuery
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 30;

        public int? langugeId { get; set; }
        public int? categoryId { get; set; }
        public string? search { get; set; }
        public decimal? maxPrice { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxHours { get; set; }
        public decimal? minHours { get; set; }
    }
}