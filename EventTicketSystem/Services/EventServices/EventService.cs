using AutoMapper;
using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Services.EventServices;

public class EventService(EventTicketDbContext context, IMapper mapper) : IEventService
{
    public async Task<List<ReturnEventDto>> GetEventByNameAsync(string eventName)
    {
        var findEvent = await context.Events.Where(e => e.EventName == eventName).ToListAsync();
        if(findEvent == null) throw new Exception("Event does not exist");
        
        return mapper.Map<List<ReturnEventDto>>(findEvent);
        
    }
    public async Task<List<ShowAllEventsDto>> GetAllEventsAsync()
    {
        var retrieveEvents = await context.Events.Where(e => e.EventId >= 0).ToListAsync();
        return retrieveEvents.Count == 0 ? throw new Exception("No events found") : mapper.Map<List<ShowAllEventsDto>>(retrieveEvents);
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

    public async Task<EditEventDto> EditEventAsync(EditEventDto eventDto, string eventName)
    {
        var isExistingEvent = await context.Events.FirstOrDefaultAsync(e => e.EventName == eventName);
        if(isExistingEvent == null) throw new Exception("Event does not exist");
        
        var editEvent = mapper.Map<Event>(eventDto);
        context.Events.Update(editEvent);
        await context.SaveChangesAsync();
        return eventDto;
    }

    public async Task<RemoveEventDto> RemoveEventAsync(RemoveEventDto eventDto)
    {
        var isExistingEvent = await context.Events.FirstOrDefaultAsync(e => e.EventName == eventDto.EventName || e.EventId == eventDto.EventId);
        if(isExistingEvent == null) throw new Exception("Event does not exist");

        context.Events.Remove(isExistingEvent);
        await context.SaveChangesAsync();
        return eventDto;
    }
}