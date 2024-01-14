using MediatR;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CourseDTOs;

namespace UdemyProject.Application.Features.Course.CourseCommands.Models
{
    public record UpdateCourseMessagesModelCommand(CourseMessageForUpdateDTO CourseMessage) : IRequest<ResponseModel<bool>>;
}