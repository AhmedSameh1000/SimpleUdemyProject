using Stripe.Checkout;
using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICartService
    {
        Task<CartForReturn> GetCartsByUser(string userId);

        Task<Session> CheckOut(CheckOutProperties checkOutProperties);

        Task CoursePaymentConfirmation(string userId);
    }
}