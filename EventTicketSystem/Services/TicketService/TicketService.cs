using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Models;
using EventTicketSystem.Services.AuthServices;
using Microsoft.EntityFrameworkCore;


namespace EventTicketSystem.Services.TicketService;

public class TicketService(EventTicketDbContext context, IMapper mapper, IAuthService authService) : ITicketService
{
    public async Task<PurchaseResultDto> PurchaseTicketAsync(PurchaseTicketDto purchaseTicketDto)
    {
        var currentUser = authService.GetUserId();
        
        var existingEvent = context.Events.Include(@event => @event.Tickets).FirstOrDefault(e => e.EventId == purchaseTicketDto.EventId);
        if(existingEvent == null) 
            throw new Exception("Event does not exist");
        
        if(existingEvent.EventDate < DateTime.UtcNow)
            throw new Exception("Event has already ended");
        
        if(purchaseTicketDto.Quantity <= 0)
            throw new Exception("Quantity must be greater than 0");
        
        var remainingTickets = existingEvent.TotalTickets - existingEvent.Tickets.Sum(t => t.Quantity);
        if (remainingTickets < 1 || remainingTickets < purchaseTicketDto.Quantity)
            throw new Exception("Not enough tickets left");

        var totalPrice = purchaseTicketDto.Quantity * existingEvent.TicketPrice;
        var purchasedAt = DateTime.UtcNow;
        var newTicket = new Ticket()
        {
            EventId = existingEvent.EventId,
            ApplicationUserId = currentUser,
            Quantity = purchaseTicketDto.Quantity,
            PricePaid = totalPrice,
            PurchasedAt = purchasedAt
        };

        context.Tickets.Add(newTicket);
        await context.SaveChangesAsync();
        return new PurchaseResultDto()
        {
                EventId = purchaseTicketDto.EventId,
                EventName = existingEvent.EventName,
                Quantity = purchaseTicketDto.Quantity,
                PricePerTicket = existingEvent.TicketPrice,
                TotalPrice = totalPrice,
                PurchasedAt = purchasedAt
        };
    }

    public async Task<List<GetTicketInformationDto>> GetAllTicketsForCurrentUserAsync()
    {
        var currentUser = authService.GetUserId();
        var tickets =  await context.Tickets.Where(t => t.ApplicationUserId == currentUser).Select(t => mapper.Map<GetTicketInformationDto>(t)).ToListAsync();
        return mapper.Map<List<GetTicketInformationDto>>(tickets);
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