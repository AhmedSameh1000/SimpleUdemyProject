using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int courseId { get; set; }
        public decimal coursePrice { get; set; }
        public Course course { get; set; }
        public int cartId { get; set; }
        public Cart cart { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public string applicationUserId { get; set; }
        public List<CartItem> Items { get; set; }

        public decimal totalPrice { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}