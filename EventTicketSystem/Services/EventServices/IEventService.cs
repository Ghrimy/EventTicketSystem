using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Models;

namespace EventTicketSystem.Services.EventServices;

public interface IEventService
{
    public Task<ReturnEventDto> GetEventByIdAsync(int eventId);
    public Task<List<ShowAllEventsDto>> GetAllEventsAsync();
    public Task<int> CreateEventAsync(CreateEventDto eventDto);
    public Task<EditEventDto> EditEventAsync(int eventId, EditEventDto eventDto);
    public Task<RemoveEventDto> RemoveEventAsync(int eventId, RemoveEventDto eventDto);
}