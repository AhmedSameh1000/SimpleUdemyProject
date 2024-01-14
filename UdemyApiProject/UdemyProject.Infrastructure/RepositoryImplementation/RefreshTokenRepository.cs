using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}