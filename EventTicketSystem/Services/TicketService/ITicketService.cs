using EventTicketSystem_DTOs.AuthDto;
using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Models;

namespace EventTicketSystem.Services.TicketService;

public interface ITicketService
{
    public Task<PurchaseResultDto> PurchaseTicketAsync(PurchaseTicketDto purchaseTicketDto);
    public Task<List<GetTicketInformationDto>> GetAllTicketsForCurrentUserAsync();
    public Task<CancelTicketDto> CancelTicketAsync(int ticketId);
    public Task<List<GetTicketInformationDto>> GetTicketByIdAsync(int eventId);
}