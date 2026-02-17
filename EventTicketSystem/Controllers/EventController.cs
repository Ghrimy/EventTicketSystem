using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Services.EventServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[ApiController]
[Route("api/events")]
public class EventController(IEventService eventService) : ControllerBase
{

    [HttpGet("{eventId:int}")]
    public async Task<ActionResult<ShowAllEventsDto>> GetEventById(int eventId)
    {
        return Ok(await eventService.GetEventByIdAsync(eventId));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ShowAllEventsDto>>> GetEvents()
    {
        return Ok(await eventService.GetAllEventsAsync());
    }
    
    [Authorize(Roles = "Organizer", AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
    {
        var createdEventId = await eventService.CreateEventAsync(eventDto);
        return CreatedAtAction(nameof(GetEventById), new {eventId = createdEventId}, null);
    }

    [Authorize(Roles = "Organizer", AuthenticationSchemes = "Bearer")]
    [HttpPatch("{eventId:int}")]
    public async Task<IActionResult> EditEvent(int eventId, EditEventDto editEventDto)
    {
        return Ok(await eventService.EditEventAsync(eventId, editEventDto));
    }

    [Authorize(Roles = "Organizer", AuthenticationSchemes = "Bearer")]
    [HttpDelete("{eventId:int}")]
    public async Task<IActionResult> DeleteEvent(int eventId, RemoveEventDto removeEventDto)
    {
        await eventService.GetEventByIdAsync(eventId);
        return NoContent();
    }
}