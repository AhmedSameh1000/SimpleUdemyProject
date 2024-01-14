using MediatR;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CourseDTOs;

namespace UdemyProject.Application.Features.Course.CourseCommands.Models
{
    public record UpdateCoursePriceModelCommand(CoursePriceForUpdate Course) : IRequest<ResponseModel<bool>>;
}