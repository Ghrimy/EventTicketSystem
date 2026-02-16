namespace EventTicketSystem.Middleware.EventExceptions;

public class EventDoesNotExistException(string message) : DomainException(message, StatusCodes.Status404NotFound);