using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.LectureDTOs;
using UdemyProject.Contracts.RepositoryContracts;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;
using Xabe.FFmpeg;

namespace UdemyProject.Application.ServicesImplementation.CourseLectureServiceImplementation
{
    internal class CourseLectureService : ICourseLectureService
    {
        private readonly ICourseSectionRepository _CourseSectionRepository;
        private readonly ICourseLectureRepository _CourseLectureRepository;
        private readonly IFileServices _FileServices;
        private readonly IWebHostEnvironment _Host;

        public CourseLectureService(
            ICourseSectionRepository courseSectionRepository,
            ICourseLectureRepository courseLectureRepository,
            IFileServices fileServices,
            IWebHostEnvironment Host)
        {
            _CourseSectionRepository = courseSectionRepository;
            _CourseLectureRepository = courseLectureRepository;
            _FileServices = fileServices;
            _Host = Host;
        }

        public async Task<bool> CreateLecture(int sectionId)
        {
            var Section = await _CourseSectionRepository.GetFirstOrDefault(c => c.Id == sectionId);

            if (Section is null)
            {
                return false;
            }
            var Lecture = new Lecture()
            {
                SectionId = sectionId,
            };
            await _CourseLectureRepository.Add(Lecture);
            return await _CourseLectureRepository.SaveChanges();
        }

        public async Task<bool> UpdateLecture(LectureForUpdateDTO lectureForUpdateDTO)
        {
            var Lecture = await _CourseLectureRepository.GetFirstOrDefault(c => c.Id == lectureForUpdateDTO.Id);

            if (Lecture is null)
                return false;

            
            Lecture.Description = lectureForUpdateDTO.Description;

            if (lectureForUpdateDTO.Video != null)
            {
                if (Lecture.VideoLectureUrl != null)
                {
                    var CurrentVideo = Path.Combine(_Host.WebRootPath, "CoursesVideos", Lecture.VideoLectureUrl);

                    if (Path.Exists(CurrentVideo))
                    {
                        _FileServices.DeleteFile("CoursesVideos", Lecture.VideoLectureUrl);
                    }
                }

                FFmpeg.SetExecutablesPath(Path.Combine(_Host.WebRootPath, "FFPEG"), "ffmpeg", "ffprobe");
                var Res = _FileServices.SaveFile(lectureForUpdateDTO.Video, Path.Combine(_Host.WebRootPath, "CoursesVideos"));
                Lecture.VideoLectureUrl = Res.Path;
                Lecture.Title = Res.Name;
                var LectureVideoPath = Path.Combine(_Host.WebRootPath, "CoursesVideos", Res.Path);

                if (Path.Exists(LectureVideoPath))
                {
                    var mediaInfo = await FFmpeg.GetMediaInfo(LectureVideoPath);
                    Lecture.VideoMinuteLength = mediaInfo.Duration.Minutes;
                }
            }
            _CourseLectureRepository.Update(Lecture);
            return await _CourseLectureRepository.SaveChanges();
        }
        public async Task<bool> DeleteLecture(int lectureId)
        {
            var Lecture = await _CourseLectureRepository.GetFirstOrDefault(c => c.Id == lectureId);

            if (Lecture == null)
                return false;
            if (Lecture.VideoLectureUrl != null)
            {
                var CurrentVideo = Path.Combine(_Host.WebRootPath, "CoursesVideos", Lecture.VideoLectureUrl);

                if (Path.Exists(CurrentVideo))
                {
                    _FileServices.DeleteFile("CoursesVideos", Lecture.VideoLectureUrl);
                }
            }

            _CourseLectureRepository.Remove(Lecture);
            return await _CourseLectureRepository.SaveChanges();
        }
    }
}