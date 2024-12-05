using Core.Persistence.Repositories;
using Microsoft.Extensions.Logging;
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
    public class EfEventRepository : EfRepositoryBase<Event, Guid, BaseDbContext>, IEventRepository
    {
        public EfEventRepository(BaseDbContext context) : base(context)
        {

        }
    }
}
