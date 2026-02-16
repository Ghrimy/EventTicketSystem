namespace EventTicketSystem.Middleware.EventExceptions;

public class EventAlreadyExistsException(string eventName) : DomainException($"An event with the name '{eventName}' already exists.", StatusCodes.Status409Conflict);

