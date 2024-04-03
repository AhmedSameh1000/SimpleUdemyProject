using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Application.Features.Review.ReviewCommand.Models;
using UdemyProject.Contracts.DTOs.ReviewDTOs;

namespace UdemyProject.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ReviewController : AppBaseController
    {
        private readonly IMediator _Mediator;

        public ReviewController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewForCreateDto reviewForCreateDto)
        {
            var Response = await _Mediator.Send(new ReviewForCreateModelCommand(reviewForCreateDto));

            return NewResult(Response);
        }
    }
}