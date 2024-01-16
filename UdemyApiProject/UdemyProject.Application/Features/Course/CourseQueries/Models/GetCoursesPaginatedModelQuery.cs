using MediatR;
using SchoolProject.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Contracts.Helpers;
using X.PagedList;

namespace UdemyProject.Application.Features.Course.CourseQueries.Models
{
    public record GetCoursesPaginatedModelQuery(PaginationQuery query) : IRequest<PaginatedResult<CourseForReturnDTO>>;
}