using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CartItem
{
    public class CartItemForCreate
    {
        public string userId { get; set; }
        public string cartItemTitle { get; set; }
        public decimal price { get; set; }
        public int courseId { get; set; }
    }
}