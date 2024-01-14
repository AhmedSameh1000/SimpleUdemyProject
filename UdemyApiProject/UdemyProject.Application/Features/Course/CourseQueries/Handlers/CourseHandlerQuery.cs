using MediatR;
using Microsoft.Extensions.Localization;

using UdemyProject.Application.Features.Course.CourseQueries.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Course.CourseQueries.Handlers
{
    public class CourseHandlerQuery : ResponseHandlerModel,
        IRequestHandler<GetCourseDetailsModelQuery, ResponseModel<CourseForReturnDto>>,
        IRequestHandler<GetCourseLandingPageQuery, ResponseModel<CourseLandingPageForReturnDTO>>,
        IRequestHandler<GetCourseVideoPromotionpathQuery, string>,
        IRequestHandler<GetCoursesForInstructorModelQuery, ResponseModel<List<InstructorMinimalCourses>>>,
        IRequestHandler<GetCourseMessageModelQuery, ResponseModel<CourseMessageForReturnDTo>>,
        IRequestHandler<GetCoursePriceModelQuery, ResponseModel<CoursePriceForReturnDTO>>
    {
        private readonly ICourseService _CourseService;

        public CourseHandlerQuery(IStringLocalizer<Sharedresources> stringLocalizer, ICourseService courseService) : base(stringLocalizer)
        {
            _CourseService = courseService;
        }

        public async Task<ResponseModel<CourseForReturnDto>> Handle(GetCourseDetailsModelQuery request, CancellationToken cancellationToken)
        {
            var CourseDetails = await _CourseService.GetCourse(request.CourseId);
            if (CourseDetails == null)
            {
                return NotFound<CourseForReturnDto>();
            }

            return Success(CourseDetails);
        }

        public async Task<ResponseModel<CourseLandingPageForReturnDTO>> Handle(GetCourseLandingPageQuery request, CancellationToken cancellationToken)
        {
            var Course = await _CourseService.GetCourseLandingPage(request.CourseId);
            if (Course == null)
            {
                return NotFound<CourseLandingPageForReturnDTO>();
            }

            return Success(Course);
        }

        public async Task<string> Handle(GetCourseVideoPromotionpathQuery request, CancellationToken cancellationToken)
        {
            var VideoPath = await _CourseService.GetVideoPromotionCourse(request.Id);

            if (VideoPath is null)
            {
                return null;
            }

            return VideoPath;
        }

        public async Task<ResponseModel<List<InstructorMinimalCourses>>> Handle(GetCoursesForInstructorModelQuery request, CancellationToken cancellationToken)
        {
            var Response = await _CourseService.GetInstructorCourse(request.Id);

            return Success(Response);
        }

        public async Task<ResponseModel<CourseMessageForReturnDTo>> Handle(GetCourseMessageModelQuery request, CancellationToken cancellationToken)
        {
            var CourseForeturn = await _CourseService.GetCourseMessage(request.Id);
            if (CourseForeturn is null)
                return NotFound<CourseMessageForReturnDTo>();

            return Success(CourseForeturn);
        }

        public async Task<ResponseModel<CoursePriceForReturnDTO>> Handle(GetCoursePriceModelQuery request, CancellationToken cancellationToken)
        {
            var Course = await _CourseService.GetCoursePrice(request.CourseId);

            if (Course is null)
            {
                return NotFound<CoursePriceForReturnDTO>();
            }

            return Success(Course);
        }
    }
}