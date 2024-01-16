using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.Course;
using UdemyProject.Contracts.DTOs.CourseDTOs;
using UdemyProject.Contracts.DTOs.SectionDTOs;
using UdemyProject.Contracts.Helpers;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.ServicesImplementation.CourseServicesimplementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _CourseRepository;
        private readonly ICourseRequimentRepository _CourseRequimentRepository;
        private readonly IWhatYouLearnFromCourseRepository _WhatYouLearnFromCourseRepository;
        private readonly IWhoIsThisCourseForRepository _WhoIsThisCourseForRepository;
        private readonly IWebHostEnvironment _Host;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IMapper _Mapper;

        public CourseService(ICourseRepository courseRepository, ICourseRequimentRepository courseRequimentRepository,
            IWhatYouLearnFromCourseRepository whatYouLearnFromCourseRepository,
            IWhoIsThisCourseForRepository whoIsThisCourseForRepository,
            IWebHostEnvironment host, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _CourseRepository = courseRepository;
            _CourseRequimentRepository = courseRequimentRepository;
            _WhatYouLearnFromCourseRepository = whatYouLearnFromCourseRepository;
            _WhoIsThisCourseForRepository = whoIsThisCourseForRepository;
            _Host = host;
            _HttpContextAccessor = httpContextAccessor;
            _Mapper = mapper;
        }

        public async Task<int> CreateBasicCourse(CourseBasicDataDTO courseBasic)
        {
            if (courseBasic is null)
            {
                return -1;
            }

            var Course = new Course();
            _Mapper.Map(courseBasic, Course);
            await _CourseRepository.Add(Course);
            await _CourseRepository.SaveChanges();

            return Course.Id;
        }

        public async Task CreateRequimentCourse(CoursePrerequisiteDTO prerequisiteDTO)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == prerequisiteDTO.Id, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse" });

            if (Course is null)
            {
                return;
            }

            var Req = await _CourseRequimentRepository.GetAllAsTracking(c => c.CourseId == Course.Id);
            _CourseRequimentRepository.RemoveRange(Req);

            var what = await _WhatYouLearnFromCourseRepository.GetAllAsTracking(c => c.CourseId == Course.Id);
            _WhatYouLearnFromCourseRepository.RemoveRange(what);

            var who = await _WhoIsThisCourseForRepository.GetAllAsTracking(c => c.CourseId == Course.Id);
            _WhoIsThisCourseForRepository.RemoveRange(who);
            await _WhoIsThisCourseForRepository.SaveChanges();

            var Requirment = new List<CourseRequirment>();

            _Mapper.Map(prerequisiteDTO, Requirment);

            Course.Requirments.AddRange(Requirment);

            var WhoisTHisCoureFor = new List<WhoIsthisCoursefor>();

            _Mapper.Map(prerequisiteDTO, WhoisTHisCoureFor);

            Course.whoIsthisCoursefors.AddRange(WhoisTHisCoureFor);

            var whatYouLearnFromCourse = new List<WhatYouLearnFromCourse>();

            _Mapper.Map(prerequisiteDTO, whatYouLearnFromCourse);

            Course.whatYouLearnFromCourse.AddRange(whatYouLearnFromCourse);

            _CourseRepository.Update(Course);
            await _CourseRepository.SaveChanges();
        }

        public async Task<CourseDetailsForReturnDto> GetCourse(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse" });
            if (Course is null)
                return null;

            var CourseForReturn = _Mapper.Map<CourseDetailsForReturnDto>(Course);

            return CourseForReturn;
        }

        public async Task<CourseLandingPageForReturnDTO> GetCourseLandingPage(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);
            if (Course is null)
            {
                return null;
            }
            var CourseLandingPageForReturnDTO = new CourseLandingPageForReturnDTO()
            {
                CourseId = Id,
                CategoryId = Course.CategoryId,
                CourseImage = Course.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", Course.Image),
                Description = Course?.Description,
                LangugeId = Course.langugeId.HasValue ? Course.langugeId.Value : default,
                SubTitle = Course?.SubTitle,
                Title = Course?.Title,
            };
            return CourseLandingPageForReturnDTO;
        }

        public async Task<CourseMessageForReturnDTo> GetCourseMessage(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);

            if (Course is null)
            {
                return null;
            }

            var CourseForReturn = _Mapper.Map<CourseMessageForReturnDTo>(Course);
            return CourseForReturn;
        }

        public async Task<CoursePriceForReturnDTO> GetCoursePrice(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);

            if (Course is null)
                return null;

            var CoursePriceForReturn = _Mapper.Map<CoursePriceForReturnDTO>(Course);

            return CoursePriceForReturn;
        }

        public async Task<bool> DeleteCourse(int CourseId, string InstructorId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == CourseId && c.InstructorId == InstructorId);

            if (Course is null)
                return false;

            _CourseRepository.Remove(Course);
            var isDeleted = await _CourseRepository.SaveChanges();

            if (isDeleted && Course.Image != null)
            {
                DeleteFileInWWWRoot("CourseImages", Course.Image);
            }

            if (isDeleted && Course.CoursePromotionalVideo != null)
            {
                DeleteFileInWWWRoot("PromotionalVideo", Course.CoursePromotionalVideo);
            }

            return isDeleted;
        }

        public void DeleteFileInWWWRoot(string Folderpath, string fileNamewithExtension)
        {
            var path = Path.Combine(_Host.WebRootPath, Folderpath, Path.GetFileName(fileNamewithExtension));
            var IsExist = Path.Exists(path);
            if (IsExist)
            {
                File.Delete(path);
            }
        }

        public async Task<List<InstructorMinimalCourses>> GetInstructorCourse(string InstructorId)
        {
            var Courses = await _CourseRepository.GetAllAsNoTracking(c => c.InstructorId == InstructorId, new[] { "Requirments", "whoIsthisCoursefors", "whatYouLearnFromCourse", "Instructor", "category", "languge" });

            var CourseToReturn = Courses.Select(c => new InstructorMinimalCourses()
            {
                Id = c.Id,
                Name = c.Title,
                SubTitle = c.SubTitle,
                Image = c.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", c.Image),
                Progress = (int)(100 * (c.CountofNotNullValues() / 19))
            }).ToList();

            return CourseToReturn;
        }

        public async Task<string> GetVideoPromotionCourse(int Id)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == Id);

            if (Course is null || Course.CoursePromotionalVideo is null)
            {
                return null;
            }
            //var CourseVideoPath = Course.CoursePromotionalVideo;
            var CourseVideoPath = Path.Combine(_Host.WebRootPath, "PromotionalVideo", Course.CoursePromotionalVideo);
            return CourseVideoPath;
        }

        public async Task<bool> SaveCourseLanding(CourseLandingDTO courseLanding)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseLanding.CourseId);
            if (Course is null) return false;

            if (courseLanding.Image is not null)
            {
                if (Course.Image != null)
                {
                    DeleteFileInWWWRoot("CourseImages", Course.Image);
                }
                Course.Image = SaveFile(courseLanding.Image, Path.Combine(_Host.WebRootPath, "CourseImages"));
            }

            if (courseLanding.PromotionVideo is not null)
            {
                if (Course.CoursePromotionalVideo != null)
                {
                    DeleteFileInWWWRoot("PromotionalVideo", Course.CoursePromotionalVideo);
                }
                Course.CoursePromotionalVideo = SaveFile(courseLanding.PromotionVideo, Path.Combine(_Host.WebRootPath, "PromotionalVideo"));
            }

            Course.Title = courseLanding.Title;

            Course.SubTitle = courseLanding.SubTitle;
            Course.Description = courseLanding.Description;
            Course.langugeId = courseLanding.LangugeId;
            Course.CategoryId = courseLanding.CategoryId.Value;
            _CourseRepository.Update(Course);
            return await _CourseRepository.SaveChanges();
        }

        public async Task<bool> UpdateCourseMessage(CourseMessageForUpdateDTO courseMessageForUpdateDTO)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == courseMessageForUpdateDTO.CourseId && c.InstructorId == courseMessageForUpdateDTO.InstructorId);

            if (Course is null)
            {
                return false;
            }

            _Mapper.Map(courseMessageForUpdateDTO, Course);

            _CourseRepository.Update(Course);
            return await _CourseRepository.SaveChanges();
        }

        public async Task<bool> UpdateCourseprice(CoursePriceForUpdate coursePriceForUpdate)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == coursePriceForUpdate.CourseId && c.InstructorId == coursePriceForUpdate.InstructorId);

            if (Course is null)
            {
                return false;
            }
            _Mapper.Map(coursePriceForUpdate, Course);

            _CourseRepository.Update(Course);
            return await _CourseRepository.SaveChanges();
        }

        public string SaveFile(IFormFile file, string FolderPath)
        {
            var FileUrl = "";
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            using (FileStream fileStreams = new(Path.Combine(FolderPath,
                            fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            FileUrl = fileName + extension;
            return FileUrl;
        }

        public IQueryable<CourseForReturnDTO> GetCoursesQuerable(PaginationQuery paginationQuery)
        {
            Expression<Func<UdemyProject.Domain.Entities.Course, CourseForReturnDTO>> expression = e => new CourseForReturnDTO(e.Id, e.Title, e.SubTitle, e.Price.HasValue ? e.Price.Value : 0, e.Instructor.Name, e.InstructorId, Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", e.Image));

            var Query = _CourseRepository.GetAllQuerableAsNoTracking(new[] { "Instructor" }).AsQueryable();

            if (paginationQuery.search != null)
            {
                Query = Query.Where(c => c.Title.ToLower().Contains(paginationQuery.search.ToLower()));
            }

            return Query.Select(expression);
        }
    }
}