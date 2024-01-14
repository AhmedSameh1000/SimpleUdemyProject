using UdemyProject.Contracts.DTOs.SectionDTOs;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICourseSectionService
    {
        Task<bool> CreateSection(int CourseId);

        Task<bool> UpdateSection(SectionForUpdateDTO forUpdateDTO);

        Task<List<SectionForReturnDTO>> GetSections(int CourseId);

        Task<bool> DeleteSection(int SectionId);
    }
}