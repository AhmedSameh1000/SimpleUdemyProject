namespace UdemyProject.Domain.Entities
{
    public class Lecture
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public string? Title { get; set; }
        public string? VideoLectureUrl { get; set; }
        public string? Description { get; set; }

        public int? VideoMinuteLength { get; set; }
    }
}