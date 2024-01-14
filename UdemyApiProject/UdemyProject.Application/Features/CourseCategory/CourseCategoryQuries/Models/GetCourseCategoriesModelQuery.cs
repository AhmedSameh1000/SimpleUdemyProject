using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Contracts.DTOs.CourseCategoryDTOs;

namespace UdemyProject.Application.Features.CourseCategory.CourseCategoryQuries.Models
{
    public record GetCourseCategoriesModelQuery() : IRequest<ResponseModel<List<CourseCategoryDTO>>>;
}