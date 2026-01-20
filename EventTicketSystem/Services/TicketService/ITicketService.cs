using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Models;

namespace EventTicketSystem.Services.TicketService;

public interface ITicketService
{
    public Task<BookTicketDto> BookTicketAsync(int ticketId, int quantity);
    public Task<List<GetTicketInformationDto>> GetAllTicketsAsync();
    public Task<CancelTicketDto> CancelTicketAsync(int ticketId);
    public Task<GetTicketInformationDto> GetTicketByNameAsync(string ticketName);
}