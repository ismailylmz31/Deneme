using AutoMapper;
using Core.AOP.Aspects;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.Persistence.Extensions;
using System.Linq.Expressions;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Events;
using TechCareer.Models.Dtos.Roles;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Constants;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly EventBusinessRules _businessRules;

        public EventService(IEventRepository eventRepository, IMapper mapper, EventBusinessRules businessRules)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        //[LoggerAspect]
        //[ClearCacheAspect("Events")]
        //[AuthorizeAspect("Admin")]
        public async Task<EventResponseDto> AddAsync(CreateEventRequestDto dto)
        {
            try
            {
                await _businessRules.EventTitleMustBeUnique(dto.Title);
                var eventEntity = _mapper.Map<Event>(dto);
                eventEntity.Id = Guid.NewGuid();

                var addedEvent = await _eventRepository.AddAsync(eventEntity);

                EventResponseDto responseDto = _mapper.Map<EventResponseDto>(addedEvent);
                return responseDto;
            }
            catch (Exception ex)
            {

                throw new BusinessException($"Error occurred while adding event: {ex.Message}", ex);
            }
        }

        //[LoggerAspect]
        //[ClearCacheAspect("Events")]
        //[AuthorizeAspect("Admin")]
        public async Task<string> DeleteAsync(Guid id, bool permanent = false)
        {
            try
            {
                var eventEntity = await _businessRules.EventMustExist(id);
                await _eventRepository.DeleteAsync(eventEntity, permanent);
                return EventMessages.EventDeleted;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error occurred while deleting event: {ex.Message}", ex);
            }
        }

        //[LoggerAspect]
        //[ClearCacheAspect("Events")]
        //[AuthorizeAspect("Admin")]
        public async Task<EventResponseDto> UpdateAsync(Guid id, UpdateEventRequestDto dto)
        {
            try
            {
                var eventEntity = await _businessRules.EventMustExist(id);

                _mapper.Map(dto, eventEntity);

                var updatedEvent = await _eventRepository.UpdateAsync(eventEntity);
                EventResponseDto responseDto = _mapper.Map<EventResponseDto>(updatedEvent);
                return responseDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error occurred while updating event: {ex.Message}", ex);
            }
        }

        //[CacheAspect(cacheKeyTemplate: "EventList", bypassCache: false, cacheGroupKey: "Events")]
        public async Task<List<EventResponseDto>> GetListAsync(
            Expression<Func<Event, bool>>? predicate = null,
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
            bool include = false,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var events = await _eventRepository.GetListAsync(predicate, orderBy, include, withDeleted, enableTracking, cancellationToken);
                return _mapper.Map<List<EventResponseDto>>(events);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error occurred while fetching event list: {ex.Message}", ex);
            }
        }

        public async Task<Paginate<EventResponseDto>> GetPaginateAsync(
            Expression<Func<Event, bool>>? predicate = null,
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
            bool include = true,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            try

            {
                var events = await _eventRepository.GetPaginateAsync(
                    predicate,
                    orderBy,
                    include,
                    index,
                    size,
                    withDeleted,
                    enableTracking,
                    cancellationToken
                );

                return new Paginate<EventResponseDto>
                {
                    Items = _mapper.Map<IList<EventResponseDto>>(events.Items),
                    Index = events.Index,
                    Size = events.Size,
                    Count = events.Count,
                    Pages = events.Pages
                };
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error occurred while fetching paginated events: {ex.Message}", ex);
            }
        }

        public async Task<EventResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var eventEntity = await _businessRules.EventMustExist(id);
                EventResponseDto responseDto = _mapper.Map<EventResponseDto>(eventEntity);
                return responseDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error occurred while fetching event by ID: {ex.Message}", ex);
            }
        }
    }
}
