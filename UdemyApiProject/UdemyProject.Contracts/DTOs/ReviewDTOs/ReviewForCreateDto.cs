namespace UdemyProject.Contracts.DTOs.ReviewDTOs
{
    public class ReviewForCreateDto
    {
        public string userId { get; set; }
        public int stars { get; set; }
        public string text { get; set; }
        public int courseId { get; set; }
    }
}