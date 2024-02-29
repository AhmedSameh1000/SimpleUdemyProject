using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Wrappers;
using System;
using System.Linq.Expressions;
using UdemyProject.Application.Features.Course.CourseQueries.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Contracts.Helpers;
using UdemyProject.Contracts.ServicesContracts;
using X.PagedList;

namespace UdemyProject.Application.Features.Course.CourseQueries.Handlers
{
    public class CourseHandlerQuery : ResponseHandlerModel,
        IRequestHandler<GetCourseDetailsModelQuery, ResponseModel<CourseDetailsForReturnDto>>,
        IRequestHandler<GetCourseLandingPageQuery, ResponseModel<CourseLandingPageForReturnDTO>>,
        IRequestHandler<GetCourseVideoPromotionpathQuery, string>,
        IRequestHandler<GetCoursesForInstructorModelQuery, ResponseModel<List<InstructorMinimalCourses>>>,
        IRequestHandler<GetCourseMessageModelQuery, ResponseModel<CourseMessageForReturnDTo>>,
        IRequestHandler<GetCoursePriceModelQuery, ResponseModel<CoursePriceForReturnDTO>>,
        IRequestHandler<GetCoursesPaginatedModelQuery, PaginatedResult<CourseForReturnDTO>>,
        IRequestHandler<GetCourseFullDetailsQuery, ResponseModel<Course_With_Instructor_Details>>

    {
        private readonly ICourseService _CourseService;
        private readonly IWebHostEnvironment _WebHost;

        public CourseHandlerQuery(IStringLocalizer<Sharedresources> stringLocalizer, ICourseService courseService, IWebHostEnvironment webHost) : base(stringLocalizer)
        {
            _CourseService = courseService;
            _WebHost = webHost;
        }

        public async Task<ResponseModel<CourseDetailsForReturnDto>> Handle(GetCourseDetailsModelQuery request, CancellationToken cancellationToken)
        {
            var CourseDetails = await _CourseService.GetCourse(request.CourseId);
            if (CourseDetails == null)
            {
                return NotFound<CourseDetailsForReturnDto>();
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

        public async Task<PaginatedResult<CourseForReturnDTO>> Handle(GetCoursesPaginatedModelQuery request, CancellationToken cancellationToken)
        {
            var Querable = _CourseService.GetCoursesQuerable(request.query);

            //var PaginatedList = await Querable.ToPaginatedListAsync(request.query.pageNumber, request.query.pageSize);
            var PaginatedList = await Querable.ToPaginatedListAsync(request.query.pageNumber, request.query.pageSize);

            return PaginatedList;
        }

        public async Task<ResponseModel<Course_With_Instructor_Details>> Handle(GetCourseFullDetailsQuery request, CancellationToken cancellationToken)
        {
            var Result = await _CourseService.GetFullCourseDetails(request.courseId);
            if (Result is null)
                return BadRequest<Course_With_Instructor_Details>();

            return Success(Result);
        }

        /*

            public async Task<IPagedList<CourseForReturnDTO>> Handle(GetCoursesPaginatedModelQuery request, CancellationToken cancellationToken)
        {
            var Querable = _CourseService.GetCoursesQuerable(request.query);

            //var PaginatedList = await Querable.ToPaginatedListAsync(request.query.pageNumber, request.query.pageSize);
            var PaginatedList = await Querable.ToPagedListAsync(request.query.pageNumber, request.query.pageSize);

            return PaginatedList;
        }
         */
    }
}