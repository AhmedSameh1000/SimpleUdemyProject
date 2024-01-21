using AutoMapper;
using UdemyProject.Contracts.DTOs.CourseCategoryDTOs;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.Mapping
{
    public class CourseCategoryProfile : Profile
    {
        public CourseCategoryProfile()
        {
            MapFrom_CourseCategory_CourseCategoryDTO();
        }

        public void MapFrom_CourseCategory_CourseCategoryDTO()
        {
            CreateMap<CourseCategory, CourseCategoryDTO>()
                .ForMember(c => c.coursesCount, opt => opt.MapFrom(c => c.Courses.Count())).ReverseMap();
        }
    }
}