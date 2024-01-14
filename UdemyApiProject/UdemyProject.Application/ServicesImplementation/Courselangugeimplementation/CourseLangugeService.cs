using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.CourseLangugeDTOs;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.ServicesImplementation.Courselangugeimplementation
{
    public class CourseLangugeService : ICourseLangugeService
    {
        private readonly ICourseLangugeRepository _CourseLangugeRepository;
        private readonly IMapper _Mapper;

        public CourseLangugeService(
            ICourseLangugeRepository courseLangugeRepository,
            IMapper mapper
            )
        {
            _CourseLangugeRepository = courseLangugeRepository;
            _Mapper = mapper;
        }

        public async Task<List<CourselangugeDTO>> GetAlllanguge()
        {
            var languges = await _CourseLangugeRepository.GetAllAsNoTracking();
            var Languges = _Mapper.Map<List<CourselangugeDTO>>(languges);

            return Languges;
        }
    }
}