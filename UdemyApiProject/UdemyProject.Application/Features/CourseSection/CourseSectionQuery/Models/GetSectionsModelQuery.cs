using MediatR;

using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.SectionDTOs;

namespace UdemyProject.Application.Features.CourseSection.CourseSectionQuery.Models
{
    public record GetSectionsModelQuery(int CourseId) : IRequest<ResponseModel<List<SectionForReturnDTO>>>;
}