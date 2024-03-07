using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.Cart.CartCommand.Models;
using UdemyProject.Application.Features.Cart.CartQuery.Models;
using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Api.Controllers
{
    public class CartController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CartController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetCartByUserId(string userId)
        {
            var Result = await _Mediator.Send(new GetCartModelQuery(userId));

            return NewResult(Result);
        }

        [HttpGet("CoursePaymentConfirmation")]
        public async Task<ActionResult> CoursePaymentConfirmation(string userId)
        {
            var Result = await _Mediator.Send(new CoursePaymentModelCommand(userId));

            return NewResult(Result);
        }

        [HttpPost("CheckOut")]
        public async Task<ActionResult> Checkout([FromBody] CheckOutProperties checkOutProperties)
        {
            var Result = await _Mediator.Send(new CheckoutModelCommand(checkOutProperties));

            return NewResult(Result);
        }
    }
}