using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseSectionRepository : GenericRepository<Section>, ICourseSectionRepository
    {
        public CourseSectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}