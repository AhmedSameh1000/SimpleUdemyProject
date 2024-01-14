using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseLectureRepository : GenericRepository<Lecture>, ICourseLectureRepository
    {
        public CourseLectureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}