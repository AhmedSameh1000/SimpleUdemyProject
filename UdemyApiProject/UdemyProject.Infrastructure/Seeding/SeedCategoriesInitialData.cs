using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Infrastructure.Seeding
{
    public class SeedCategoriesInitialData
    {
        private readonly ICourseCategoryRepository _CategoryRepository;

        public SeedCategoriesInitialData(ICourseCategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }

        public async Task SeedCategories()
        {
            if (await _CategoryRepository.GetCount() > 0)
            {
                return;
            }
            await _CategoryRepository.AddRange(GetCategories());
            await _CategoryRepository.SaveChanges();
        }

        public List<CourseCategory> GetCategories()
        {
            return new List<CourseCategory>()
            {
                new CourseCategory(){Name="Development"},
                new CourseCategory(){Name="Business"},
                new CourseCategory(){Name="Finance & Accounting"},
                new CourseCategory(){Name="IT & Software"},
                new CourseCategory(){Name="Office Productivity"},
                new CourseCategory(){Name="Personal Development"},
                new CourseCategory(){Name="Design"},
                new CourseCategory(){Name="Marketing"},
                new CourseCategory(){Name="Lifestyle"},
                new CourseCategory(){Name="Photography & Video"},
                new CourseCategory(){Name="Health & Fitness"},
                new CourseCategory(){Name="Music"},
                new CourseCategory(){Name="Teaching & Academics"},
            };
        }
    }
}