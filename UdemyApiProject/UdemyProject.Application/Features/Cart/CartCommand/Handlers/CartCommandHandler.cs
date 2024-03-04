using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
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
        IRequestHandler<CreateCartModelCommand, ResponseModel<bool>>
    {
        private readonly IValidator<CartItemForCreate> _CartItemForCreateValidator;
        private readonly ICartItemService _CartService;

        public CartCommandHandler(
            IStringLocalizer<Sharedresources> stringLocalizer,
            IValidator<CartItemForCreate> CartItemForCreateValidator,
            ICartItemService cartService

            ) : base(stringLocalizer)
        {
            _CartItemForCreateValidator = CartItemForCreateValidator;
            _CartService = cartService;
        }

        public async Task<ResponseModel<bool>> Handle(CreateCartModelCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _CartItemForCreateValidator.ValidateAsync(request.CartForCreate);

            if (!ValidationResult.IsValid)
            {
                return BadRequest<bool>(string.Join(',', ValidationResult.Errors.Select(c => c.ErrorMessage)));
            }

            var Result = await _CartService.AddCartItem(request.CartForCreate);

            if (!Result)
            {
                return BadRequest<bool>();
            }

            return Success(Result);
        }
    }
}