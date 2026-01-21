using AutoMapper;
using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Data;

namespace EventTicketSystem.Services.TicketService;

public class TicketService(EventTicketDbContext context, IMapper mapper) : ITicketService
{
    public async Task FindCurrentUser()
    {
        var user = await context.ApplicationUsers.FindAsync();
    }
    public async Task<BookTicketDto> BookTicketAsync(int ticketId, int quantity)
    {
        var ticket = await context.Tickets.FindAsync(ticketId);
        if(ticket == null) throw new Exception("Ticket does not exist");
        return null;

    }

    public async Task<List<GetTicketInformationDto>> GetAllTicketsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CancelTicketDto> CancelTicketAsync(int ticketId)
    {
        throw new NotImplementedException();
    }

    public async Task<GetTicketInformationDto> GetTicketByNameAsync(string ticketName)
    {
        throw new NotImplementedException();
    }
}