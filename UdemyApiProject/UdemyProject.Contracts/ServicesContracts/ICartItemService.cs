using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICartItemService
    {
        Task<bool> AddCartItem(CartItemForCreate cartItemForCreate);
    }
}