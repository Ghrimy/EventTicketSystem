using AutoMapper;
using EventTicketSystem_DTOs.EventDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Models;
using EventTicketSystem.Services.EventServices;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController(IEventService eventService) : ControllerBase
{

    
    [HttpGet("Events")]
    public async Task<ActionResult<List<Event>>> GetEvents()
    {
        try
        {
            var events = await eventService.GetAllEventsAsync();
            return Ok(events);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("CreateEvent")]
    public async Task<IActionResult> CreateEvent(CreateEventDto eventDto)
    {
        try
        {
            return Ok(await eventService.CreateEventAsync(eventDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("EditEvent")]
    public async Task<IActionResult> EditEvent(EditEventDto editEventDto)
    {
        try
        {
            return Ok(await eventService.EditEventAsync(editEventDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteEvent")]
    public async Task<IActionResult> DeleteEvent(RemoveEventDto removeEventDto)
    {
        try
        {
            return Ok(await eventService.RemoveEventAsync(removeEventDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}