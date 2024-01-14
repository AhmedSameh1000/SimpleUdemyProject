namespace UdemyProject.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }

        public string? CourseLanguge { get; set; }

        public int CategoryId { get; set; }
        public int? langugeId { get; set; }
        public string? Image { get; set; }
        public string? CoursePromotionalVideo { get; set; }

        public bool isPublished { get; set; }
        public CourseLanguge languge { get; set; }
        public CourseCategory category { get; set; }

        public List<CourseRequirment> Requirments { get; set; }
        public List<WhoIsthisCoursefor> whoIsthisCoursefors { get; set; }

        public List<WhatYouLearnFromCourse> whatYouLearnFromCourse { get; set; }

        public List<ApplicationUser> Students { get; set; }
        public List<Section> Sections { get; set; }

        public string InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }
        public string? WelcomeMessage { get; set; }
        public string? CongratulationsMessage { get; set; }
        public decimal? Price { get; set; }

        public double CountofNotNullValues()
        {
            double notNullCount = 0;

            foreach (var property in GetType().GetProperties())
            {
                var value = property.GetValue(this);

                // Check if the property is a reference type and its value is not null
                if (property.PropertyType.IsClass && value != null)
                {
                    notNullCount++;
                }

                // If the property is a nullable value type, check if its value is not null
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                    value != null)
                {
                    notNullCount++;
                }
            }

            return notNullCount;
        }
    }
}