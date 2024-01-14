using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseCategoryRepository : GenericRepository<CourseCategory>, ICourseCategoryRepository
    {
        public CourseCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}