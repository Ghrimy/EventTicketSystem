using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Services.EventServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IEventService eventService) : ControllerBase
{

    [HttpGet("events/{id:int}")]
    public ActionResult<ShowAllEventsDto> GetEventById([FromBody] int eventId)
    {
        var result =  eventService.GetEventByIdAsync(eventId);
        return Ok(result);
    }
    
    [HttpGet("events")]
    public async Task<ActionResult<List<ShowAllEventsDto>>> GetEvents()
    {
        var events = await eventService.GetAllEventsAsync();
        return Ok(events);
    }
    

    [Authorize(Roles = "Organizer", AuthenticationSchemes = "Bearer")]
    [HttpPost("events")]
    public async Task<IActionResult> CreateEvent(CreateEventDto eventDto)
    {
        var result = await eventService.CreateEventAsync(eventDto);
        return Ok(result);
    }

    [Authorize(Roles = "Organizer")]
    [HttpPatch("edit-event")]
    public async Task<IActionResult> EditEvent(EditEventDto editEventDto)
    {
        var result = await eventService.EditEventAsync(editEventDto);
        return Ok(result);
    }

    [Authorize(Roles = "Organizer")]
    [HttpDelete("delete-event")]
    public async Task<IActionResult> DeleteEvent(RemoveEventDto removeEventDto)
    {
        var result = await eventService.RemoveEventAsync(removeEventDto);
        return Ok(result);
    }
}