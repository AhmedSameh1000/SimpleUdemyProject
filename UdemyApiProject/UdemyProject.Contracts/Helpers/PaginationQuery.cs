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
        public int pageSize { get; set; } = 10;

        public string? search { get; set; }
    }
}