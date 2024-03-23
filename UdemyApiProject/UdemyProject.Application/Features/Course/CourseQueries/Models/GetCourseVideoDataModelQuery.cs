using MediatR;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CourseDTOs;

namespace UdemyProject.Application.Features.Course.CourseQueries.Models
{
    public record GetCourseVideoDataModelQuery(string userId, int courseId, int lectureId) : IRequest<ResponseModel<CourseVideoData>>;
}