using MediatR;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CourseDTOs;

namespace UdemyProject.Application.Features.Course.CourseQueries.Models
{
    public record GetCourseFullDetailsQuery(int courseId) : IRequest<ResponseModel<Course_With_Instructor_Details>>;
}