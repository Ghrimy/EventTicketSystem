using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Services.TicketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/tickets")]
public class TicketController(ITicketService ticketService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseTicketDto dto)
    {
        var ticketId = await ticketService.PurchaseTicketAsync(dto);

        return CreatedAtAction(
            nameof(GetTicket),
            new { ticketId },
            null);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTickets()
    {
        return Ok(await ticketService.GetAllTicketsForCurrentUserAsync());
    }

    [HttpGet("{ticketId:int}")]
    public async Task<IActionResult> GetTicket(int ticketId)
    {
        return Ok(await ticketService.GetTicketByIdAsync(ticketId));
    }
    
    [HttpDelete("{ticketId:int}")]
    public async Task<IActionResult> CancelTicket(int ticketId)
    {
        await ticketService.CancelTicketAsync(ticketId);
        return NoContent();
    }
}
