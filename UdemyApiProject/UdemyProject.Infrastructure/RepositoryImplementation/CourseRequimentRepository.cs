using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseRequimentRepository : GenericRepository<CourseRequirment>, ICourseRequimentRepository
    {
        public CourseRequimentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}