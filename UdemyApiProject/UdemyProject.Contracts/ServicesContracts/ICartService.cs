using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICartService
    {
        Task<CartForReturn> GetCartsByUser(string userId);
    }
}