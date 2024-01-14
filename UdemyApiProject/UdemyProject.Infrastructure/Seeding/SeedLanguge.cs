using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Infrastructure.Seeding
{
    public class SeedLanguge
    {
        private readonly ICourseLangugeRepository _CourseLangugeRepository;

        public SeedLanguge(ICourseLangugeRepository courseLangugeRepository)
        {
            _CourseLangugeRepository = courseLangugeRepository;
        }

        public async Task seedLanguge()
        {
            if (await _CourseLangugeRepository.GetCount() > 0)
            {
                return;
            }
            await _CourseLangugeRepository.AddRange(GetLanguages());
            await _CourseLangugeRepository.SaveChanges();
        }

        public List<CourseLanguge> GetLanguages()
        {
            List<CourseLanguge> result = new List<CourseLanguge>();

            // Add specific languages to the list
            result.Add(new CourseLanguge { Name = "English" });
            result.Add(new CourseLanguge { Name = "Spanish" });
            result.Add(new CourseLanguge { Name = "French" });
            result.Add(new CourseLanguge { Name = "German" });
            result.Add(new CourseLanguge { Name = "Chinese" });
            result.Add(new CourseLanguge { Name = "Japanese" });
            result.Add(new CourseLanguge { Name = "Russian" });
            result.Add(new CourseLanguge { Name = "Arabic" });
            result.Add(new CourseLanguge { Name = "Hindi" });
            result.Add(new CourseLanguge { Name = "Portuguese" });

            return result;
        }
    }
}