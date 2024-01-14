using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.Authentication.AuthenticationCommands.Models;
using UdemyProject.Application.Features.CourseCategory.CourseCategoryQuries.Models;

namespace UdemyProject.Api.Controllers
{
    public class CourseCategoryController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public CourseCategoryController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var Response = await _Mediator.Send(new GetCourseCategoriesModelQuery());
            return NewResult(Response);
        }

        [HttpGet("Getlanguges")]
        public async Task<IActionResult> Getlanguges()
        {
            var Response = await _Mediator.Send(new GetLangugesModelQuery());
            return NewResult(Response);
        }
    }
}