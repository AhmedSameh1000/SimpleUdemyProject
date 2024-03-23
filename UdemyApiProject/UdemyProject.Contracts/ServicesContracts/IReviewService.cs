using UdemyProject.Contracts.DTOs.ReviewDTOs;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface IReviewService
    {
        Task<bool> CreateReview(ReviewForCreateDto reviewForCreateDto);
    }
}