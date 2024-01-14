using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contracts.DTOs.CourseCategoryDTOs;

namespace UdemyProject.Contracts.ServicesContracts
{
    public interface ICourseCategoryService
    {
        Task<List<CourseCategoryDTO>> GetCourseCategories();
    }
}