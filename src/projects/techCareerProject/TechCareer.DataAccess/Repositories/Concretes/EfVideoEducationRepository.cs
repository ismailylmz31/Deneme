

using Core.Persistence.Repositories;
using TechCareer.DataAccess.Contexts;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Concretes;

public sealed class EfVideoEducationRepository : EfRepositoryBase<VideoEducation, int, BaseDbContext>, IVideoEducationRepository
{
    public EfVideoEducationRepository(BaseDbContext context) : base(context)
    {

    }
}
