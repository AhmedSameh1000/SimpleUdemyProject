using MediatR;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.SectionDTOs;

namespace UdemyProject.Application.Features.CourseSection.CourseSectionCommand.Models
{
    public record SectionForUpdateModelCommand(SectionForUpdateDTO Section) : IRequest<ResponseModel<bool>>;
}