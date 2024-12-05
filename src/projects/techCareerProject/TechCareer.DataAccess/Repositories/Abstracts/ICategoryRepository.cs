using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Abstracts
{
    public interface ICategoryRepository : IAsyncRepository<Category, int>
    {
    }
}