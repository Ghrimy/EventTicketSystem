using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EventTicketSystem_DTOs.TicketDtos;
using EventTicketSystem.Data;
using EventTicketSystem.Middleware.EventExceptions;
using EventTicketSystem.Middleware.TicketExceptions;
using EventTicketSystem.Models;
using EventTicketSystem.Services.AuthServices;
using Microsoft.EntityFrameworkCore;


namespace EventTicketSystem.Services.TicketService;

public class TicketService(EventTicketDbContext context, IMapper mapper, IAuthService authService) : ITicketService
{
    public async Task<int> PurchaseTicketAsync(PurchaseTicketDto purchaseTicketDto)
    {
        var currentUser = authService.GetUserId();
        
        var existingEvent = context.Events
            .FirstOrDefault(e => e.EventId == purchaseTicketDto.EventId);
        
        if(existingEvent == null) 
            throw new EventNotFoundException(purchaseTicketDto.EventId);

        if (existingEvent.EventDate < DateTime.UtcNow)
            throw new EventEndedException(existingEvent.EventName);
        
        if(purchaseTicketDto.Quantity <= 0)
            throw new ValidPurchaseAmountException(purchaseTicketDto.Quantity);

        var remainingTickets = existingEvent.TotalTickets - existingEvent.TicketsSold;
        if (remainingTickets < 1 || remainingTickets < purchaseTicketDto.Quantity)
            throw new TicketSoldOutException(purchaseTicketDto.EventId);
        
        existingEvent.TicketsSold += purchaseTicketDto.Quantity;
        
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
        try
        {
            context.Tickets.Add(newTicket);
            await context.SaveChangesAsync();
            return newTicket.TicketId;
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await context.Events
                .AnyAsync(e => e.EventId == purchaseTicketDto.EventId);

            if (!exists)
                throw new EventNotFoundException(purchaseTicketDto.EventId);

            throw new ConcurrencyConflictException(
                "This event was modified by another user. Please refresh and try again.");
        }
    }

    public async Task<List<GetTicketInformationDto>> GetAllTicketsForCurrentUserAsync()
    {
        var currentUser = authService.GetUserId();
        return await context.Tickets.Where(t => t.ApplicationUserId == currentUser)
            .Select(t => mapper.Map<GetTicketInformationDto>(t)).ToListAsync();
    }

    public async Task<CancelTicketDto> CancelTicketAsync(int ticketId)
    {
        var user = authService.GetUserId();
        var existingEvent = await context.Events.Include(e => e.Tickets).FirstOrDefaultAsync(e => e.Tickets.Any(t => t.TicketId == ticketId));
        if (existingEvent == null) throw new EventDoesNotExistException("No event was found");
        if (existingEvent.EventDate < DateTime.UtcNow) throw new EventEndedException(existingEvent.EventName);
        
        var userTickets = await context.Tickets.FirstOrDefaultAsync(t => t.ApplicationUserId == user && t.TicketId == ticketId);
        if (userTickets == null) throw new UserHasNoTicketException(ticketId);
        
        context.Tickets.Remove(userTickets);
        existingEvent.TicketsSold -= userTickets.Quantity;
        
        await context.SaveChangesAsync();
        return mapper.Map<CancelTicketDto>(userTickets);
    }

    public async Task<List<GetTicketInformationDto>> GetTicketByIdAsync(int eventId)
    {
        var user = authService.GetUserId();
        var userTickets = await context.Tickets.FirstOrDefaultAsync(t => t.ApplicationUserId == user && t.EventId == eventId);
        return mapper.Map<List<GetTicketInformationDto>>(userTickets) ?? throw new NoTicketFoundException(eventId);
    }
}