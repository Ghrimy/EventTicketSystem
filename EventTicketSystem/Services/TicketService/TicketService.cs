using AutoMapper;
using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Models;
using EventTicketSystem.Services.AuthServices;
using Microsoft.AspNetCore.Identity;

namespace EventTicketSystem.Services.TicketService;

public class TicketService(EventTicketDbContext context, IMapper mapper, IAuthService authService) : ITicketService
{
    public async Task<PurchaseResultDto> PurchaseTicketAsync(PurchaseTicketDto purchaseTicketDto)
    {
        var currentUser = authService.GetUserId();
        if(currentUser == null)
            throw new Exception("User is not authenticated");
        
        var existingEvent = context.Events.FirstOrDefault(e => e.EventId == purchaseTicketDto.EventId);
        if(existingEvent == null) 
            throw new Exception("Event does not exist");
        
        
        throw new NotImplementedException();
    }

    public async Task<List<GetTicketInformationDto>> GetAllTicketsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CancelTicketDto> CancelTicketAsync(int ticketId)
    {
        throw new NotImplementedException();
    }

    public async Task<GetTicketInformationDto> GetTicketByNameAsync(int eventId)
    {
        throw new NotImplementedException();
    }
}