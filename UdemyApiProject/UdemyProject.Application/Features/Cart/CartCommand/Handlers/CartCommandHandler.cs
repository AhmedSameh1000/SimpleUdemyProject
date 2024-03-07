using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.Features.Cart.CartCommand.Models;
using UdemyProject.Application.Features.Course.CourseCommands.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.CartItem;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Cart.CartCommand.Handlers
{
    public class CartCommandHandler : ResponseHandlerModel,
        IRequestHandler<CreateCartModelCommand, ResponseModel<bool>>,
        IRequestHandler<RemoveCartitemModelCommand, ResponseModel<bool>>,
           IRequestHandler<CheckoutModelCommand, ResponseModel<Session>>,
           IRequestHandler<CoursePaymentModelCommand, ResponseModel<bool>>
    {
        private readonly IValidator<CartItemForCreate> _CartItemForCreateValidator;
        private readonly ICartService _CartService;
        private readonly ICartItemService _CartItemService;

        public CartCommandHandler(
            IStringLocalizer<Sharedresources> stringLocalizer,
            IValidator<CartItemForCreate> CartItemForCreateValidator,
            ICartService cartService,
            ICartItemService cartItemService

            ) : base(stringLocalizer)
        {
            _CartItemForCreateValidator = CartItemForCreateValidator;
            _CartService = cartService;
            _CartItemService = cartItemService;
        }

        public async Task<ResponseModel<Session>> Handle(CheckoutModelCommand request, CancellationToken cancellationToken)
        {
            var Result = await _CartService.CheckOut(request.checkOutProperties);
            if (Result is null)
                return BadRequest<Session>();

            return Success(Result);
        }

        public async Task<ResponseModel<bool>> Handle(CreateCartModelCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _CartItemForCreateValidator.ValidateAsync(request.CartForCreate);

            if (!ValidationResult.IsValid)
            {
                return BadRequest<bool>(string.Join(',', ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }

            var Result = await _CartItemService.AddCartItem(request.CartForCreate);

            if (!Result)
            {
                return BadRequest<bool>();
            }

            return Success(Result);
        }

        public async Task<ResponseModel<bool>> Handle(RemoveCartitemModelCommand request, CancellationToken cancellationToken)
        {
            var IsDeleted = await _CartItemService.RemoveCartItem(request.CartitemId, request.userId);

            if (!IsDeleted)
                return BadRequest<bool>();

            return Success(IsDeleted);
        }

        public async Task<ResponseModel<bool>> Handle(CoursePaymentModelCommand request, CancellationToken cancellationToken)
        {
            await _CartService.CoursePaymentConfirmation(request.userId);
            return Success(true);
        }
    }
}