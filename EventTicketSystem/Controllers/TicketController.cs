using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Services.TicketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketService ticketService) : ControllerBase
{

    [HttpPost("purchase-ticket")]
    public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseTicketDto dto)
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

    [HttpPost("get-ticket/{ticketId:int}")]
    public async Task<IActionResult> GetTicket(int ticketId)
    {
        try
        {
            var ticket = await ticketService.GetTicketByIdAsync(ticketId);
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("cancel-ticket/{ticketId:int}")]
    public async Task<IActionResult> CancelTicket(int ticketId)
    {
        try
        {
            var result = await ticketService.CancelTicketAsync(ticketId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
