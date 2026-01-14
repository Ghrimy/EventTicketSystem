using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Models;

namespace EventTicketSystem.Services.EventServices;

public interface IEventService
{
    public Task<List<ShowAllEventsDto>> GetAllEventsAsync();
    public Task<CreateEventDto> CreateEventAsync(CreateEventDto eventDto);
    public Task<EditEventDto> EditEventAsync(EditEventDto eventDto);
    public Task<RemoveEventDto> RemoveEventAsync(RemoveEventDto eventDto);
}