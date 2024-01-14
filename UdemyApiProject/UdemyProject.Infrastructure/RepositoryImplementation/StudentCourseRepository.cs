using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}