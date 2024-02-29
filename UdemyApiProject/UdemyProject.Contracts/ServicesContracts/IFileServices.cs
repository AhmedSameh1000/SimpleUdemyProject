using Microsoft.AspNetCore.Http;
using UdemyProject.Contracts.DTOs.FileDTO;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface IFileServices
    {
        void DeleteFile(string Folderpath, string fileNamewithExtension);

        FileInformation SaveFile(IFormFile file, string FolderPath);
    }
}