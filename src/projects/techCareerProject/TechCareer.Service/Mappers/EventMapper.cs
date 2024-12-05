using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Events;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Mappers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {

            CreateMap<CreateEventRequestDto, Event>();
            CreateMap<Event, EventResponseDto>();
            CreateMap<UpdateEventRequestDto, Event>();


        }
    }
}
