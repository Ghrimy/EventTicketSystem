using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Services.TicketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("purchase")]
    public async Task<IActionResult> Purchase([FromBody] PurchaseTicketDto dto)
    {
        try
        {
            var result = await ticketService.PurchaseTicketAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("my-tickets")]
    public async Task<IActionResult> GetMyTickets()
    {
        try
        {
            var tickets = await ticketService.GetAllTicketsForCurrentUserAsync();
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
