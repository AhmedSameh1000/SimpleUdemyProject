namespace UdemyProject.Domain.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string? WhatStudentLearnFromthisSection { get; set; }

        public List<Lecture>? Lecture { get; set; }
    }
}