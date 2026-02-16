namespace EventTicketSystem.Middleware.EventExceptions;

public class EventEndedException(string eventName)
    : DomainException($"The event '{eventName}' has ended.", StatusCodes.Status400BadRequest);
