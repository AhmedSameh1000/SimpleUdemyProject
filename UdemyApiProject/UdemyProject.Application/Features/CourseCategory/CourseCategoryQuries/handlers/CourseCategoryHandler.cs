using MediatR;
using Microsoft.Extensions.Localization;
using UdemyProject.Application.Features.CourseCategory.CourseCategoryQuries.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.CourseCategoryDTOs;
using UdemyProject.Contracts.DTOs.CourseLangugeDTOs;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.CourseCategory.CourseCategoryQuries.handlers
{
    internal class CourseCategoryHandler : ResponseHandlerModel,
        IRequestHandler<GetCourseCategoriesModelQuery, ResponseModel<List<CourseCategoryDTO>>>,
        IRequestHandler<GetLangugesModelQuery, ResponseModel<List<CourselangugeDTO>>>
    {
        private readonly ICourseCategoryService _CourseCategoryService;
        private readonly ICourseLangugeService _CourseLangugeService;

        public CourseCategoryHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            ICourseCategoryService courseCategoryService, ICourseLangugeService courseLangugeService) : base(stringLocalizer)
        {
            _CourseCategoryService = courseCategoryService;
            _CourseLangugeService = courseLangugeService;
        }

        public async Task<ResponseModel<List<CourseCategoryDTO>>> Handle(GetCourseCategoriesModelQuery request, CancellationToken cancellationToken)
        {
            var Categories = await _CourseCategoryService.GetCourseCategories();

            return Success(Categories);
        }

        public async Task<ResponseModel<List<CourselangugeDTO>>> Handle(GetLangugesModelQuery request, CancellationToken cancellationToken)
        {
            var courselangugeDTOs = await _CourseLangugeService.GetAlllanguge();

            return Success(courselangugeDTOs);
        }
    }
}