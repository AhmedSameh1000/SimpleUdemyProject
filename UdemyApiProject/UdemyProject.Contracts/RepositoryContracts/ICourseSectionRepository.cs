using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Contracts.RepositoryContracts
{
    public interface ICourseSectionRepository : IGenericRepository<Section>
    {
    }
}