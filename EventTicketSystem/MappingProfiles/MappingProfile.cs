using AutoMapper;
using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Models;

namespace EventTicketSystem.MappingProfiles;

public class MappingProfile :Profile
{
    public MappingProfile()
    {
        //CreateEventDto -> Event
        CreateMap<CreateEventDto, Event>();
        CreateMap<Event, ShowAllEventsDto>();
        
        //RemoveEventDto -> Event
        CreateMap<Event, RemoveEventDto>();
        CreateMap<RemoveEventDto, Event>();
        
        //EditEventDto -> Event
        CreateMap<Event, EditEventDto>();
        CreateMap<EditEventDto, Event>();
        
        //ReturnEvent -> Event
        CreateMap<ReturnEventDto, Event>();
        CreateMap<Event, ReturnEventDto>();
        
        
        //BookTicketDto -> EventTicket
        CreateMap<BookTicketDto, EventTicket>();
        CreateMap<EventTicket, BookTicketDto>();
    }
}