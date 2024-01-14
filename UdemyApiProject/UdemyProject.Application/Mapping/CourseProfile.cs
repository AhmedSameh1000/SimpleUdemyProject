using AutoMapper;
using UdemyProject.Contracts.DTOs.Course;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            MapFromCourseBasicData_Course();
            mapFromCourseRequirmentDTO_CourseRequirment();
            mapFromCourseRequirmentDTO_whoIsthisCoursefors();
            mapFromCourseRequirmentDTO_whatYouLearnFromCourse();
            MapFromGenralModelCourseDetailsDTo_Requirment_Who_What();
            MapFromCourseMessageForUpdateDTO_Course();
            MapFromCourseMessageForReturnDTO_Course();
            MapFromCoursePriceForUpdate_Course();
            MapFromCoursePriceForReturn_Course();
        }

        public void MapFromCourseBasicData_Course()
        {
            CreateMap<CourseBasicDataDTO, Course>()
             .ForMember(src => src.Title, dst => dst.MapFrom(d => d.Name))
             .ForMember(src => src.CategoryId, dst => dst.MapFrom(d => d.Category))
             .ForMember(src => src.InstructorId, dst => dst.MapFrom(d => d.InstructorId))
             .ForMember(src => src.category, opt => opt.Ignore()) // Ignore the Category for now to avoid the mapping error
             .ReverseMap();
        }

        public void mapFromCourseRequirmentDTO_CourseRequirment()
        {
            CreateMap<CoursePrerequisiteDTO, List<CourseRequirment>>()
             .AfterMap((src, dest) =>
             {
                 dest.AddRange(src.Requiments.Select(c => new CourseRequirment { Text = c }));
             });
        }

        public void mapFromCourseRequirmentDTO_whoIsthisCoursefors()
        {
            CreateMap<CoursePrerequisiteDTO, List<WhoIsthisCoursefor>>()
                .AfterMap((src, dest) =>
                {
                    dest.AddRange(src.WhoIsCourseFor.Select(c => new WhoIsthisCoursefor { Text = c }));
                });
        }

        public void mapFromCourseRequirmentDTO_whatYouLearnFromCourse()
        {
            CreateMap<CoursePrerequisiteDTO, List<WhatYouLearnFromCourse>>()
              .AfterMap((src, dest) =>
              {
                  dest.AddRange(src.WhateYouLearnFromCourse.Select(c => new WhatYouLearnFromCourse { Text = c }));
              });
        }

        public void MapFromCourseMessageForUpdateDTO_Course()
        {
            CreateMap<CourseMessageForUpdateDTO, Course>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.CourseId))
                .ForMember(c => c.InstructorId, opt => opt.Ignore())
                .ForMember(c => c.CongratulationsMessage, opt => opt.MapFrom(c => c.CongratulationsMessage))
                .ForMember(c => c.WelcomeMessage, opt => opt.MapFrom(c => c.WelcomeMessage)).ReverseMap();
        }

        public void MapFromCourseMessageForReturnDTO_Course()
        {
            CreateMap<CourseMessageForReturnDTo, Course>().ReverseMap();
        }

        public void MapFromCoursePriceForUpdate_Course()
        {
            CreateMap<CoursePriceForUpdate, Course>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.InstructorId, opt => opt.Ignore())
                .ReverseMap();
        }

        public void MapFromCoursePriceForReturn_Course()
        {
            CreateMap<Course, CoursePriceForReturnDTO>()

                .ForMember(c => c.CourseId, opt => opt.MapFrom(c => c.Id))
                .ForMember(c => c.Price, opt => opt.MapFrom(c => c.Price))
                .ReverseMap();
        }

        public void MapFromGenralModelCourseDetailsDTo_Requirment_Who_What()
        {
            CreateMap<GenralModelCourseDetailsDTo, CourseRequirment>()
                .ForMember(src => src.Text, dst => dst.MapFrom(c => c.Name))
                .ForMember(src => src.Id, dst => dst.MapFrom(c => c.Id))
                 .ForMember(src => src.Course, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<GenralModelCourseDetailsDTo, WhatYouLearnFromCourse>()
                .ForMember(src => src.Text, dst => dst.MapFrom(c => c.Name))
                .ForMember(src => src.Id, dst => dst.MapFrom(c => c.Id))
                  .ForMember(src => src.Course, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GenralModelCourseDetailsDTo, WhoIsthisCoursefor>()
                .ForMember(src => src.Text, dst => dst.MapFrom(c => c.Name))
                .ForMember(src => src.Id, dst => dst.MapFrom(c => c.Id))
                  .ForMember(src => src.Course, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CourseForReturnDto, Course>()
                .ForMember(c => c.Requirments, opt => opt.MapFrom(c => c.Requirments))
                .ForMember(c => c.whatYouLearnFromCourse, opt => opt.MapFrom(c => c.WhateWillYouLearnFromCourse))
                .ForMember(c => c.whoIsthisCoursefors, opt => opt.MapFrom(c => c.WhoIsThisCourseFor))
                .ForMember(src => src.Id, dst => dst.MapFrom(c => c.CourseId)).ReverseMap();
        }
    }
}