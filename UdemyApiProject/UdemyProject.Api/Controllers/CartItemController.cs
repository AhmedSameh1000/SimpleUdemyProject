using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.Cart.CartCommand.Models;
using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Api.Controllers
{
    public class CartItemController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CartItemController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CartItemForCreate cartItemForCreate)
        {
            var Result = await _Mediator.Send(new CreateCartModelCommand(cartItemForCreate));
            return NewResult(Result);
        }
    }
}