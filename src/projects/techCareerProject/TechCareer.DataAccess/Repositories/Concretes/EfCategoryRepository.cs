using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.DataAccess.Contexts;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Concretes
{
    public class EfCategoryRepository : EfRepositoryBase<Category, int, BaseDbContext>, ICategoryRepository
    {
        public EfCategoryRepository(BaseDbContext context) : base(context)
        {

        }
    }
}