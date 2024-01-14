using UdemyProject.Contracts.DTOs.CourseLangugeDTOs;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICourseLangugeService
    {
        Task<List<CourselangugeDTO>> GetAlllanguge();
    }
}