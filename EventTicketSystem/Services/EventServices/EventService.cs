using AutoMapper;
using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Middleware.EventExceptions;
using EventTicketSystem.Models;
using EventTicketSystem.Services.AuthServices;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Services.EventServices;

public class EventService(EventTicketDbContext context, IMapper mapper, IAuthService authService) : IEventService
{
    public async Task<ReturnEventDto> GetEventByIdAsync(int eventId)
    {
        var findEvent = context.Events.Where(e => e.EventId == eventId);
        await Task.CompletedTask;
        return mapper.Map<ReturnEventDto>(findEvent) ?? throw new EventNotFoundException(eventId);
    }
    public async Task<List<ShowAllEventsDto>> GetAllEventsAsync()
    {
        var retrieveEvents = await context.Events.Where(e => e.EventId >= 0).ToListAsync();
        return retrieveEvents.Count == 0 ? throw new EventNotFoundException(0) : mapper.Map<List<ShowAllEventsDto>>(retrieveEvents);
    }

    public async Task<int> CreateEventAsync(CreateEventDto eventDto)
    {
        var authUser = authService.GetUserId();
        var isExistingEvent = await context.Events.Where(e => e.EventName == eventDto.EventName).AnyAsync();
        if (isExistingEvent) throw new EventAlreadyExistsException(eventDto.EventName);

        var createNewEvent = new Event()
        {
            EventName = eventDto.EventName,
            EventDate = eventDto.EventDate,
            EventLocation = eventDto.EventLocation,
            TotalTickets = eventDto.TotalTickets,
            EventDescription = eventDto.EventDescription,
            TicketPrice = eventDto.TicketPrice,
            OrganizerId = authUser
        };
        
        context.Events.Add(createNewEvent);
        await context.SaveChangesAsync();
        return createNewEvent.EventId;
    }

    public async Task<EditEventDto> EditEventAsync(int eventId, EditEventDto eventDto)
    {
        var isExistingEvent = await context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);
        if(isExistingEvent == null) throw new EventNotFoundException(eventId);
        
        var editEvent = mapper.Map<Event>(eventDto);
        context.Events.Update(editEvent);
        await context.SaveChangesAsync();
        return eventDto;
    }

    public async Task<RemoveEventDto> RemoveEventAsync(int eventId, RemoveEventDto eventDto)
    {
        var isExistingEvent = await context.Events.FirstOrDefaultAsync(e => e.EventName == eventDto.EventName || e.EventId == eventId);
        if(isExistingEvent == null) throw new EventNotFoundException(eventId);

        context.Events.Remove(isExistingEvent);
        await context.SaveChangesAsync();
        return eventDto;
    }
}