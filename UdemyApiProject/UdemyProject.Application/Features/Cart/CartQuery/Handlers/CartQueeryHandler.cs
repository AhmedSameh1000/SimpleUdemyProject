using MediatR;
using Microsoft.Extensions.Localization;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.Features.Cart.CartCommand.Models;
using UdemyProject.Application.Features.Cart.CartQuery.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.CartItem;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Cart.CartQuery.Handlers
{
    public class CartQueeryHandler : ResponseHandlerModel,
        IRequestHandler<GetCartModelQuery, ResponseModel<CartForReturn>>

    {
        private readonly ICartService _CartService;
        private readonly ICartItemService _CartItemService;

        public CartQueeryHandler(
            IStringLocalizer<Sharedresources> stringLocalizer,
            ICartService cartService,
            ICartItemService cartItemService
            ) : base(stringLocalizer)
        {
            _CartService = cartService;
            _CartItemService = cartItemService;
        }

        public async Task<ResponseModel<CartForReturn>> Handle(GetCartModelQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CartService.GetCartsByUser(request.userId);

            if (Result is null)
                return BadRequest<CartForReturn>();

            return Success(Result);
        }
    }
}