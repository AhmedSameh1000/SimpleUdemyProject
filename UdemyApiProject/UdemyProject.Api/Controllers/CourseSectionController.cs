using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.CourseSection.CourseSectionCommand.Models;
using UdemyProject.Application.Features.CourseSection.CourseSectionQuery.Models;
using UdemyProject.Contracts.DTOs.SectionDTOs;

namespace UdemyProject.Api.Controllers
{
    [Authorize]
    public class CourseSectionController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CourseSectionController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("GetSections")]
        public async Task<IActionResult> GetSections(int CourseId)
        {
            var Response = await _Mediator.Send(new GetSectionsModelQuery(CourseId));
            return NewResult(Response);
        }

        [HttpPost("CreateSection")]
        public async Task<IActionResult> CreateSection(int CourseId)
        {
            var Response = await _Mediator.Send(new CourseSectionForCreateModelCommand(CourseId));
            return NewResult(Response);
        }

        [HttpDelete("DeletedSection")]
        public async Task<IActionResult> DeleteSection(int SectionId)
        {
            var Response = await _Mediator.Send(new DeleteSectionModelCommand(SectionId));
            return NewResult(Response);
        }

        [HttpPut("UpdateSection")]
        public async Task<IActionResult> UpdateSection(SectionForUpdateDTO section)
        {
            var Response = await _Mediator.Send(new SectionForUpdateModelCommand(section));
            return NewResult(Response);
        }
    }
}