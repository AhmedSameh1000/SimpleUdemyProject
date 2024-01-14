namespace UdemyProject.Domain.Entities
{
    public class CourseLanguge
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}