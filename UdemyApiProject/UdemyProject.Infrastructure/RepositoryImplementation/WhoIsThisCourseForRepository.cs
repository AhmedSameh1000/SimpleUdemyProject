using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class WhoIsThisCourseForRepository : GenericRepository<WhoIsthisCoursefor>, IWhoIsThisCourseForRepository
    {
        public WhoIsThisCourseForRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}