using AutoMapper;
using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using RetoStore.Entities;
using RetoStore.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Services.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventInfo, EventResponseDto>();
        CreateMap<Event, EventResponseDto>()
            .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.DateEvent.ToShortDateString()))
            .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.DateEvent.ToShortTimeString()))
            .ForMember(d => d.Status, o => o.MapFrom(x => x.Status ? "Activo" : "Inactivo"))
            .ForMember(d => d.Genre, o => o.MapFrom(x => x.Genre.Name));
        CreateMap<EventRequestDto, Event>()
            .ForMember(x => x.DateEvent, o => o.MapFrom(x => Convert.ToDateTime($"{x.DateEvent} {x.TimeEvent}")));
    }
}
