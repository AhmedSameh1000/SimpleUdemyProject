using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.CartItem;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.ServicesImplementation.CartItemService
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _CartItemRepository;
        private readonly ICartRepository _CartRepository;

        public CartItemService(ICartItemRepository cartItemRepository, ICartRepository cartRepository)
        {
            _CartItemRepository = cartItemRepository;
            _CartRepository = cartRepository;
        }

        public async Task<bool> AddCartItem(CartItemForCreate cartItemForCreate)
        {
            var AvailableCart = await _CartRepository.GetFirstOrDefault(c => c.applicationUserId == cartItemForCreate.userId && !c.isDeleted, new[] { "cartItems" });

            if (AvailableCart is null)
            {
                //Create new cart
                await _CartRepository.Add(new Domain.Entities.Cart()
                {
                    applicationUserId = cartItemForCreate.userId,
                    totalPrice = cartItemForCreate.price,
                    cartItems = new List<Domain.Entities.CartItem>()
                    {
                        new Domain.Entities.CartItem()
                        {
                            courseId = cartItemForCreate.courseId,
                            coursePrice = cartItemForCreate.price,
                        }
                    }
                });
            }
            else
            {
                AvailableCart.cartItems.Add(new Domain.Entities.CartItem()
                {
                    courseId = cartItemForCreate.courseId,
                    coursePrice = cartItemForCreate.price,
                });
                AvailableCart.totalPrice += cartItemForCreate.price;
                _CartRepository.Update(AvailableCart);
            }

            return await _CartRepository.SaveChanges();
        }
    }
}