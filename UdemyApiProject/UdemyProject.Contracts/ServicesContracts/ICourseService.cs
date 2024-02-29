using Microsoft.AspNetCore.Http;
using UdemyProject.Contracts.DTOs.Course;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Contracts.DTOs.SectionDTOs;
using UdemyProject.Contracts.Helpers;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICourseService
    {
        Task<int> CreateBasicCourse(CourseBasicDataDTO courseBasic);

        Task CreateRequimentCourse(CoursePrerequisiteDTO prerequisiteDTO);

        Task<CourseDetailsForReturnDto> GetCourse(int Id);

        Task<bool> SaveCourseLanding(CourseLandingDTO courseLanding);

        Task<CourseLandingPageForReturnDTO> GetCourseLandingPage(int Id);

        Task<string> GetVideoPromotionCourse(int Id);

        Task<List<InstructorMinimalCourses>> GetInstructorCourse(string InstructorId);

        Task<bool> UpdateCourseMessage(CourseMessageForUpdateDTO courseMessageForUpdateDTO);

        Task<CourseMessageForReturnDTo> GetCourseMessage(int Id);

        Task<bool> UpdateCourseprice(CoursePriceForUpdate coursePriceForUpdate);

        Task<CoursePriceForReturnDTO> GetCoursePrice(int Id);

        public IQueryable<CourseForReturnDTO> GetCoursesQuerable(PaginationQuery paginationQuery);

        Task<bool> DeleteCourse(int CourseId, string InstructorId);

        Task<Course_With_Instructor_Details> GetFullCourseDetails(int CourseId);
    }
}