using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.Features.CourseLecture.CourseLectureCommand.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.CourseLecture.CourseLectureCommand.handlers
{
    internal class LectureCommandhandler : ResponseHandlerModel,
        IRequestHandler<CreateLectureModelCommand, ResponseModel<bool>>,
        IRequestHandler<UpdateLectureModelCommand, ResponseModel<bool>>
    {
        private readonly ICourseLectureService _CourseLectureService;

        public LectureCommandhandler(IStringLocalizer<Sharedresources> stringLocalizer, ICourseLectureService courseLectureService) : base(stringLocalizer)
        {
            _CourseLectureService = courseLectureService;
        }

        public async Task<ResponseModel<bool>> Handle(CreateLectureModelCommand request, CancellationToken cancellationToken)
        {
            var Response = await _CourseLectureService.CreateLecture(request.SectionId);
            if (!Response)
            {
                return BadRequest<bool>();
            }
            return Success(Response);
        }

        public async Task<ResponseModel<bool>> Handle(UpdateLectureModelCommand request, CancellationToken cancellationToken)
        {
            var Response = await _CourseLectureService.UpdateLecture(request.Lecture);
            if (!Response)
            {
                return BadRequest<bool>();
            }
            return Success(Response);
        }
    }
}