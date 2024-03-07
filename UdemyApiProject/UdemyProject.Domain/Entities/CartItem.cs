using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyProject.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string cartItemTitle { get; set; }
        public decimal coursePrice { get; set; }

        public int? courseId { get; set; }
        public Course? course { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
        public string? ApplicationUserId { get; set; }
        public int? cartId { get; set; }

        public Cart? cart { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }

        public List<CartItem>? cartItems { get; set; }
        public decimal totalPrice { get; set; }

        public string? applicationUserId { get; set; }
        public ApplicationUser? applicationUser { get; set; }
        public bool isPaid { get; set; }
        public string? sessionPaymentId { get; set; }
    }
}