using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseLangugeRepository : GenericRepository<CourseLanguge>, ICourseLangugeRepository
    {
        public CourseLangugeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
