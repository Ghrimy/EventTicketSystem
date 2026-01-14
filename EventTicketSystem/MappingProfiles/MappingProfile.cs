using AutoMapper;
using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Models;

namespace EventTicketSystem.MappingProfiles;

public class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<CreateEventDto, Event>();
        CreateMap<Event, ShowAllEventsDto>();
    }
}