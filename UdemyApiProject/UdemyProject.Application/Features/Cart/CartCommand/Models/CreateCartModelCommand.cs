using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CartItem;

namespace UdemyProject.Application.Features.Cart.CartCommand.Models
{
    public record CreateCartModelCommand(CartItemForCreate CartForCreate) : IRequest<ResponseModel<bool>>;
}