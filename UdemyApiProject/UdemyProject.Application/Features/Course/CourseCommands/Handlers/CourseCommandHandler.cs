using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using UdemyProject.Application.Features.Course.CourseCommands.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.DTOs.Course;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Course.CourseCommands.Handlers
{
    public class CourseCommandHandler : ResponseHandlerModel,
        IRequestHandler<CreateBasicCourseModelCommand, ResponseModel<int>>,
        IRequestHandler<CreateCourseRequirmentModelCommand, ResponseModel<bool>>,
        IRequestHandler<SaveCourseLandingModelCommand, ResponseModel<bool>>,
        IRequestHandler<UpdateCourseMessagesModelCommand, ResponseModel<bool>>,
        IRequestHandler<UpdateCoursePriceModelCommand, ResponseModel<bool>>,
        IRequestHandler<DeleteCourseModelCommand, ResponseModel<bool>>
    {
        private readonly ICourseService _CourseService;
        private readonly IValidator<CourseBasicDataDTO> _BasicCoursevalidation;
        private readonly IValidator<CoursePrerequisiteDTO> _CoursePrerequisiteDTOValidator;

        public CourseCommandHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            ICourseService courseService,
            IValidator<CourseBasicDataDTO> BasicCoursevalidation,
            IValidator<CoursePrerequisiteDTO> CoursePrerequisiteDTOValidator

            ) : base(stringLocalizer)
        {
            _CourseService = courseService;
            _BasicCoursevalidation = BasicCoursevalidation;
            _CoursePrerequisiteDTOValidator = CoursePrerequisiteDTOValidator;
        }

        public async Task<ResponseModel<int>> Handle(CreateBasicCourseModelCommand request, CancellationToken cancellationToken)
        {
            var ValidationResponse = await _BasicCoursevalidation.ValidateAsync(request.CourseDTO);

            if (!ValidationResponse.IsValid)
            {
                return BadRequest<int>(string.Join(',', ValidationResponse.Errors.Select(c => c.ErrorMessage)));
            }

            var CourseId = await _CourseService.CreateBasicCourse(request.CourseDTO);

            return Success(CourseId);
        }

        public async Task<ResponseModel<bool>> Handle(CreateCourseRequirmentModelCommand request, CancellationToken cancellationToken)
        {
            var ResponseValidation = await _CoursePrerequisiteDTOValidator.ValidateAsync(request.CoursePrerequisiteDTO);
            if (!ResponseValidation.IsValid)
            {
                return BadRequest<bool>(string.Join(',', ResponseValidation.Errors.Select(c => c.ErrorMessage)));
            }

            await _CourseService.CreateRequimentCourse(request.CoursePrerequisiteDTO);

            return Success(true);
        }

        public async Task<ResponseModel<bool>> Handle(SaveCourseLandingModelCommand request, CancellationToken cancellationToken)
        {
            var isSaved = await _CourseService.SaveCourseLanding(request.Courselanding);

            if (!isSaved)
            {
                return BadRequest<bool>();
            }

            return Success(isSaved);
        }

        public async Task<ResponseModel<bool>> Handle(UpdateCourseMessagesModelCommand request, CancellationToken cancellationToken)
        {
            var IsUpdated = await _CourseService.UpdateCourseMessage(request.CourseMessage);

            if (!IsUpdated)
            {
                return BadRequest<bool>();
            }

            return Success(IsUpdated);
        }

        public async Task<ResponseModel<bool>> Handle(UpdateCoursePriceModelCommand request, CancellationToken cancellationToken)
        {
            var CourseUpdated = await _CourseService.UpdateCourseprice(request.Course);

            if (!CourseUpdated)
            {
                return BadRequest<bool>();
            }
            return Success(CourseUpdated);
        }

        public async Task<ResponseModel<bool>> Handle(DeleteCourseModelCommand request, CancellationToken cancellationToken)
        {
            var IsDeleted = await _CourseService.DeleteCourse(request.CourseId, request.InstructorId);

            if (!IsDeleted)
            {
                return BadRequest<bool>();
            }
            return Success(IsDeleted);
        }
    }
}