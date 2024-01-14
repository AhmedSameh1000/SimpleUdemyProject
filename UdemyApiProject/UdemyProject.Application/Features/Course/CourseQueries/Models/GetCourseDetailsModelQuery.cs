using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CourseDTOs;

namespace UdemyProject.Application.Features.Course.CourseQueries.Models
{
    public record GetCourseDetailsModelQuery(int CourseId) : IRequest<ResponseModel<CourseForReturnDto>>;
}