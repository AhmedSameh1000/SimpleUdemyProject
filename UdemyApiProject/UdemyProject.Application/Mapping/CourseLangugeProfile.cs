using AutoMapper;
using UdemyProject.Contracts.DTOs.CourseLangugeDTOs;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.Mapping
{
    public class CourseLangugeProfile : Profile
    {
        public CourseLangugeProfile()
        {
            MapFrom_CourseLanguge_CourseLangugeDTO();
        }

        public void MapFrom_CourseLanguge_CourseLangugeDTO()
        {
            CreateMap<CourseLanguge, CourselangugeDTO>()
                .ForMember(c=>c.CoursesCount,opt=>opt.MapFrom(c=>c.Courses.Count()))
                .ReverseMap();
        }
    }
}