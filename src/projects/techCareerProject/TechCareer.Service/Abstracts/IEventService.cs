using Core.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Events;
using TechCareer.Models.Dtos.Roles;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts
{
    public interface IEventService
    {
        // Sade Add, Update, Delete metotları
        Task<EventResponseDto> AddAsync(CreateEventRequestDto dto);
        Task<string> DeleteAsync(Guid id, bool permanent = false);
        Task<EventResponseDto> UpdateAsync(Guid id, UpdateEventRequestDto dto);

        // Listeleme ve gösterim metotları
        Task<List<EventResponseDto>> GetListAsync(
            Expression<Func<Event, bool>>? predicate = null,
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
            bool include = false,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );


        Task<Paginate<EventResponseDto>> GetPaginateAsync(
            Expression<Func<Event, bool>>? predicate = null,
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
            bool include = true,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<EventResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
