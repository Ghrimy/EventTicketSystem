namespace EventTicketSystem.Middleware.AuthenticationExceptions;

public class UserIdNotFoundException(string message) : DomainException(message, StatusCodes.Status401Unauthorized);