using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Application.Features.CourseSection.CourseSectionCommand.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.CourseSection.CourseSectionCommand.Handlers
{
    internal class CourseSectionCommandHandler : ResponseHandlerModel,
        IRequestHandler<CourseSectionForCreateModelCommand, ResponseModel<bool>>,
        IRequestHandler<SectionForUpdateModelCommand, ResponseModel<bool>>,
        IRequestHandler<DeleteSectionModelCommand, ResponseModel<bool>>
    {
        private readonly ICourseSectionService _CourseSectionService;

        public CourseSectionCommandHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            ICourseSectionService courseSectionService) : base(stringLocalizer)
        {
            _CourseSectionService = courseSectionService;
        }

        public async Task<ResponseModel<bool>> Handle(CourseSectionForCreateModelCommand request, CancellationToken cancellationToken)
        {
            var Response = await _CourseSectionService.CreateSection(request.CourseId);

            return Success(Response);
        }

        public async Task<ResponseModel<bool>> Handle(SectionForUpdateModelCommand request, CancellationToken cancellationToken)
        {
            var isUpdated = await _CourseSectionService.UpdateSection(request.Section);

            if (!isUpdated)
            {
                return BadRequest<bool>();
            }

            return Success(isUpdated);
        }

        public async Task<ResponseModel<bool>> Handle(DeleteSectionModelCommand request, CancellationToken cancellationToken)
        {
            var IsDeleted = await _CourseSectionService.DeleteSection(request.SectionId);

            if (!IsDeleted)
                return BadRequest<bool>();

            return Success(IsDeleted);
        }
    }
}