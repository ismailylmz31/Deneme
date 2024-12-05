using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.DataAccess.Repositories.Concretes;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules
{

    public class EventBusinessRules
    {
        private readonly IEventRepository _eventRepository;

        public EventBusinessRules(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public EventBusinessRules()
        {
        }

        public virtual async Task<Event> EventMustExist(Guid id)
        {
            var eventEntity = await _eventRepository.GetAsync(e => e.Id == id);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException(EventMessages.EventNotFound);
            }
            return eventEntity;
        }

        public virtual async Task EventTitleMustBeUnique(string title)
        {
            var exists = await _eventRepository.AnyAsync(e => e.Title == title);
            if (exists)
            {
                throw new InvalidOperationException(EventMessages.EventTitleAlreadyExists);
            }
        }
    }





}
