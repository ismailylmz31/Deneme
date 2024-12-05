using Core.Persistence.Extensions;
using Core.Security.Entities;
using System.Linq.Expressions;
using TechCareer.Models.Dtos.VideoEducation;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;

public interface IVideoEducationService
{
    Task<VideoEducationResponseDto> AddAsync(CreateVideoEducationRequestDto dto);
    Task<string> DeleteAsync(int id, bool permanent = false);
    Task<VideoEducationResponseDto> UpdateAsync(int id, UpdateVideoEducationRequestDto dto);
    Task<List<VideoEducationResponseDto>> GetListAsync(
        Expression<Func<VideoEducation, bool>>? predicate = null,
        Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);
    Task<Paginate<VideoEducationResponseDto>> GetPaginateAsync(
        Expression<Func<VideoEducation, bool>>? predicate = null,
        Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null,
        bool include = true,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);
    Task<VideoEducationResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
