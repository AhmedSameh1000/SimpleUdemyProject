using UdemyProject.Contracts.DTOs.LectureDTOs;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICourseLectureService
    {
        Task<bool> CreateLecture(int sectionId);

        Task<bool> UpdateLecture(LectureForUpdateDTO lectureForUpdateDTO);
    }
}