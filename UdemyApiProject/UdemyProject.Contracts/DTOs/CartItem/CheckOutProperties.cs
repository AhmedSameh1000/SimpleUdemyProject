using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CartItem
{
    public class CheckOutProperties
    {
        public string url { get; set; }
        public int cartId { get; set; }

        public string userId { get; set; }
    }
}