using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.ReviewDTOs;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.ServicesImplementation.ReviewServiceImplementation
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _ReviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _ReviewRepository = reviewRepository;
        }

        public async Task<bool> CreateReview(ReviewForCreateDto reviewForCreateDto)
        {
            if (reviewForCreateDto is null)
                return false;

            var review = new Review()
            {
                Text = reviewForCreateDto.text,
                stars = reviewForCreateDto.stars,
                userId = reviewForCreateDto.userId,
                courseId = reviewForCreateDto.courseId
            };

            await _ReviewRepository.Add(review);

            return await _ReviewRepository.SaveChanges();
        }
    }
}