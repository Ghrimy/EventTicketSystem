namespace EventTicketSystem.Middleware.AuthenticationExceptions;

public class UserEmailNotFoundException (string message)
    : DomainException(message, StatusCodes.Status401Unauthorized);