using MediatR;
using Microsoft.Extensions.Localization;
using UdemyProject.Application.Features.Review.ReviewCommand.Models;
using UdemyProject.Application.ResponseHandler;
using UdemyProject.Application.Shared;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.Features.Review.ReviewCommand.Handlers
{
    public class ReviewCommandHandler : ResponseHandlerModel,
          IRequestHandler<ReviewForCreateModelCommand, ResponseModel<bool>>
    {
        private readonly IReviewService _ReviewService;

        public ReviewCommandHandler(IStringLocalizer<Sharedresources> stringLocalizer,
            IReviewService reviewService) : base(stringLocalizer)
        {
            _ReviewService = reviewService;
        }

        public async Task<ResponseModel<bool>> Handle(ReviewForCreateModelCommand request, CancellationToken cancellationToken)
        {
            var isAdded = await _ReviewService.CreateReview(request.reviewForCreateDto);

            if (!isAdded)
            {
                return BadRequest<bool>();
            }
            return Success(isAdded);
        }
    }
}