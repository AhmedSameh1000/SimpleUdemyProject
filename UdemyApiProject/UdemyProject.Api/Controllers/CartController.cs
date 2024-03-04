using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.Cart.CartQuery.Models;

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
    }
}