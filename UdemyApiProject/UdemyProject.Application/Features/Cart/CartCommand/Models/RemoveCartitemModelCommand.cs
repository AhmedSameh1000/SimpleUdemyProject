using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.Cart.CartCommand.Models
{
    public record RemoveCartitemModelCommand(int CartitemId, string userId) : IRequest<ResponseModel<bool>>;
}