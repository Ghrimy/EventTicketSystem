namespace EventTicketSystem.Middleware.EventExceptions;

public class NoTicketFoundException(int eventId) : DomainException($"No ticket with for event: {eventId} has been found.", StatusCodes.Status404NotFound);