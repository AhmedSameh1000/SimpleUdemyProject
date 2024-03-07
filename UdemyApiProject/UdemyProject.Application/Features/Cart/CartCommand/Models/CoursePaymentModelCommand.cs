using MediatR;

using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.Cart.CartCommand.Models
{
    public record CoursePaymentModelCommand(string userId) : IRequest<ResponseModel<bool>>;
}