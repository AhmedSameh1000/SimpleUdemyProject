using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.FileDTO;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application.ServicesImplementation.FileServiceImplementation
{
    internal class FileService : IFileServices
    {
        private readonly IWebHostEnvironment _Host;

        public FileService(IWebHostEnvironment Host)
        {
            _Host = Host;
        }

        public void DeleteFile(string Folderpath, string fileNamewithExtension)
        {
            var path = Path.Combine(_Host.WebRootPath, Folderpath, Path.GetFileName(fileNamewithExtension));
            var IsExist = Path.Exists(path);
            if (IsExist)
            {
                File.Delete(path);
            }
        }

        public FileInformation SaveFile(IFormFile file, string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            var FileUrl = "";
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            using (FileStream fileStreams = new(Path.Combine(FolderPath,
                            fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            FileUrl = fileName + extension;
            return new FileInformation()
            {
                Path = FileUrl,
                Name = file.FileName
            };
        }
    }
}