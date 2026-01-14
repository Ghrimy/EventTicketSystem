using AutoMapper;
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
    
}