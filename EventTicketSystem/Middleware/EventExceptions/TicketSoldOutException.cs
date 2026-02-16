namespace EventTicketSystem.Middleware.EventExceptions;

public class TicketSoldOutException(int eventId)
    : DomainException($"There are no tickets left for event with id: {eventId}", StatusCodes.Status409Conflict);
