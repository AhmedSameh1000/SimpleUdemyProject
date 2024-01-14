using MediatR;

using UdemyProject.Application.ResponseHandler;

namespace UdemyProject.Application.Features.CourseSection.CourseSectionCommand.Models
{
    public record CourseSectionForCreateModelCommand(int CourseId) : IRequest<ResponseModel<bool>>;
}