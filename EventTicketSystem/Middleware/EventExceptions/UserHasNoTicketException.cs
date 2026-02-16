namespace EventTicketSystem.Middleware.EventExceptions;

public class UserHasNoTicketException(int ticketId) : DomainException($"User has no ticket with id: {ticketId}", StatusCodes.Status404NotFound);