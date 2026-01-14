using AutoMapper;
using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Services.EventServices;

public class EventService(EventTicketDbContext context, IMapper mapper) : IEventService
{
    public async Task<List<ShowAllEventsDto>> GetAllEventsAsync()
    {
        var retrieveEvents = await context.Events.Where(e => e.EventDate >= DateTime.Today).ToListAsync();
        if(retrieveEvents.Count == 0) throw new Exception("No events found");

        var mappedEvents = mapper.Map<List<ShowAllEventsDto>>(retrieveEvents);
        return mappedEvents;
    }

    public async Task<CreateEventDto> CreateEventAsync(CreateEventDto eventDto)
    {
        var isExistingEvent = await context.Events.Where(e => e.EventName == eventDto.EventName).AnyAsync();
        if(isExistingEvent) throw new Exception("Event already exists");

        var createNewEvent = mapper.Map<Event>(eventDto);
        
        context.Events.Add(createNewEvent);
        await context.SaveChangesAsync();
        return eventDto;
        
    }

    public async Task<EditEventDto> EditEventAsync(EditEventDto eventDto)
    {
        throw new NotImplementedException();
    }

    public async Task<RemoveEventDto> RemoveEventAsync(RemoveEventDto eventDto)
    {
        throw new NotImplementedException();
    }
}