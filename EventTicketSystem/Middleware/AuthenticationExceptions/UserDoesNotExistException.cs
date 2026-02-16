namespace EventTicketSystem.Middleware.AuthenticationExceptions;

public class UserDoesNotExistException(string message) : DomainException(message, StatusCodes.Status404NotFound);