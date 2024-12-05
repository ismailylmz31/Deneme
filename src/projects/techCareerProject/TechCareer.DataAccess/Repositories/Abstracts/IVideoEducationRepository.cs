

using Core.Persistence.Repositories;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Abstracts;

public interface IVideoEducationRepository : IAsyncRepository<VideoEducation, int>
{

}
