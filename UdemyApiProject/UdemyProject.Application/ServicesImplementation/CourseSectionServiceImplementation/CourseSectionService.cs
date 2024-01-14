using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.LectureDTOs;
using UdemyProject.Contracts.DTOs.SectionDTOs;
using UdemyProject.Contracts.RepositoryContracts;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.ServicesImplementation.CourseSectionServiceImplementation
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly ICourseSectionRepository _CourseSectionRepository;
        private readonly ICourseRepository _CourseRepository;

        public CourseSectionService(
            ICourseSectionRepository courseSectionRepository,
            ICourseRepository courseRepository
            )
        {
            _CourseSectionRepository = courseSectionRepository;
            _CourseRepository = courseRepository;
        }

        public async Task<bool> CreateSection(int CourseId)
        {
            var Course = await _CourseRepository.GetFirstOrDefault(c => c.Id == CourseId);

            if (Course is null)
                return false;
            var Section = new Section()
            {
                CourseId = CourseId,
            };

            await _CourseSectionRepository.Add(Section);
            return await _CourseSectionRepository.SaveChanges();
        }

        public async Task<List<SectionForReturnDTO>> GetSections(int CourseId)
        {
            var Sections = await _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == CourseId, new[] { "Lecture" });

            if (Sections is null)
            {
                return null;
            }

            var SectionsForReturnDto = new List<SectionForReturnDTO>();
            foreach (var Section in Sections)
            {
                SectionsForReturnDto.Add(new SectionForReturnDTO()
                {
                    CourseId = Section.CourseId,
                    Id = Section.Id,
                    Title = Section.Title,
                    WhatStudentLearnFromthisSection = Section.WhatStudentLearnFromthisSection,
                    Lectures = Section.Lecture.Select(c => new LectureForReturnDTO()
                    {
                        Id = c.Id,
                        SectionId = c.SectionId,
                        Title = c.Title,
                        VideoSectionUrl = c.VideoLectureUrl,
                        Description = c.Description,
                        Menutes = c.VideoMinuteLength
                    }).ToList(),
                });
            }
            return SectionsForReturnDto;
        }

        public async Task<bool> UpdateSection(SectionForUpdateDTO forUpdateDTO)
        {
            var Section = await _CourseSectionRepository.GetFirstOrDefault(c => c.Id == forUpdateDTO.SectionId && c.CourseId == forUpdateDTO.CourseId);

            if (Section is null)
                return false;

            Section.Title = forUpdateDTO.SectionTitle;
            Section.WhatStudentLearnFromthisSection = forUpdateDTO.SectionDescription;

            _CourseSectionRepository.Update(Section);
            return await _CourseSectionRepository.SaveChanges();
        }
    }
}