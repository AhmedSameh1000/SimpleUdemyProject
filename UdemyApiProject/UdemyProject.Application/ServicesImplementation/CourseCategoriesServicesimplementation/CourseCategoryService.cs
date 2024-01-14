using AutoMapper;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.CourseCategoryDTOs;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.ServicesImplementation.CourseCategoriesServicesimplementation
{
    internal class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _CourseCategoryRepository;
        private readonly IMapper _Mapper;

        public CourseCategoryService(
            ICourseCategoryRepository courseCategoryRepository,
            IMapper mapper
            )
        {
            _CourseCategoryRepository = courseCategoryRepository;
            _Mapper = mapper;
        }

        public async Task<List<CourseCategoryDTO>> GetCourseCategories()
        {
            var Categories = await _CourseCategoryRepository.GetAllAsNoTracking();

            var CategoriesForReturn = _Mapper.Map<List<CourseCategoryDTO>>(Categories);


            return CategoriesForReturn;
        }
    }
}