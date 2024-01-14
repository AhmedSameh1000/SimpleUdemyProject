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
        private readonly IWebHostEnvironment _Host;

        public CourseLectureService(ICourseSectionRepository courseSectionRepository, ICourseLectureRepository courseLectureRepository, IWebHostEnvironment Host)
        {
            _CourseSectionRepository = courseSectionRepository;
            _CourseLectureRepository = courseLectureRepository;
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

            Lecture.Title = lectureForUpdateDTO.Title;
            Lecture.Description = lectureForUpdateDTO.Description;

            if (lectureForUpdateDTO.Video != null)
            {
                if (Lecture.VideoLectureUrl != null)
                {
                    var CurrentVideo = Path.Combine(_Host.WebRootPath, "CoursesVideos", Lecture.VideoLectureUrl);

                    if (Path.Exists(CurrentVideo))
                    {
                        DeleteFileInWWWRoot("CoursesVideos", Lecture.VideoLectureUrl);
                    }
                }

                FFmpeg.SetExecutablesPath(Path.Combine(_Host.WebRootPath, "FFPEG"), "ffmpeg", "ffprobe");
                var Url = SaveFile(lectureForUpdateDTO.Video, Path.Combine(_Host.WebRootPath, "CoursesVideos"));
                Lecture.VideoLectureUrl = Url;
                var LectureVideoPath = Path.Combine(_Host.WebRootPath, "CoursesVideos", Url);

                if (Path.Exists(LectureVideoPath))
                {
                    var mediaInfo = await FFmpeg.GetMediaInfo(LectureVideoPath);
                    Lecture.VideoMinuteLength = mediaInfo.Duration.Minutes;
                }
            }
            _CourseLectureRepository.Update(Lecture);
            return await _CourseLectureRepository.SaveChanges();
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
                    DeleteFileInWWWRoot("CoursesVideos", Lecture.VideoLectureUrl);
                }
            }

            _CourseLectureRepository.Remove(Lecture);
            return await _CourseLectureRepository.SaveChanges();
        }
    }
}