using MediatR;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Application.Features.Cart.CartCommand.Models
{
    public record CheckoutModelCommand(CheckOutProperties checkOutProperties) : IRequest<ResponseModel<Session>>;
}