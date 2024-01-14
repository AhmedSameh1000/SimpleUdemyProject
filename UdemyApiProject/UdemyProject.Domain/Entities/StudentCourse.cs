namespace UdemyProject.Domain.Entities
{
    public class StudentCourse
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string InstructorId { get; set; }

        public Course Course { get; set; }
        public ApplicationUser Instructor { get; set; }
    }
}