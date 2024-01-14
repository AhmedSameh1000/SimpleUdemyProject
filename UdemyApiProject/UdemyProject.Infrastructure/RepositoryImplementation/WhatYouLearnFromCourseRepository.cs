using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;
using UdemyProject.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class WhatYouLearnFromCourseRepository : GenericRepository<WhatYouLearnFromCourse>, IWhatYouLearnFromCourseRepository
    {
        public WhatYouLearnFromCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}