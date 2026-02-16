namespace EventTicketSystem.Middleware.TicketExceptions;

public class ConcurrencyConflictException(string message)
    : DomainException(message, StatusCodes.Status409Conflict);
