using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyProject.Contracts.DTOs.CartItem
{
    public class CartForReturn
    {
        public int cartId { get; set; }
        public string userId { get; set; }

        public decimal totlaPrice { get; set; }

        public List<cartItemsForReturn> items { get; set; }
    }

    public class cartItemsForReturn
    {
        public int cartItemId { get; set; }

        public string cartItemImage { get; set; }

        public string cartItemTitle { get; set; }

        public int courseId { get; set; }

        public decimal price { get; set; }
        public int cartItemRating { get; set; }

        public int totalMinutes { get; set; }

        public int LecturesCount { get; set; }
    }
}