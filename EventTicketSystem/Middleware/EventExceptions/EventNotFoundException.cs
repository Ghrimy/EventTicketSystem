namespace EventTicketSystem.Middleware.EventExceptions;

public class EventNotFoundException(int eventId) : DomainException($"There was no event found with id: {eventId}", StatusCodes.Status404NotFound);