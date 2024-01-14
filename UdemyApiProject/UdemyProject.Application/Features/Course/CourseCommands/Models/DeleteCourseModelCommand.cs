using MediatR;
using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.Course.CourseCommands.Models
{
    public record DeleteCourseModelCommand(int CourseId, string InstructorId) : IRequest<ResponseModel<bool>>;
}