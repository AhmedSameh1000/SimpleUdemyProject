using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseCategory> courseCategories { get; set; }

        public DbSet<CourseRequirment> courseRequirments { get; set; }

        public DbSet<StudentCourse> students { get; set; }

        public DbSet<WhatYouLearnFromCourse> whatyouWillLearn { get; set; }
        public DbSet<WhoIsthisCoursefor> whoIsthisCoursefor { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}