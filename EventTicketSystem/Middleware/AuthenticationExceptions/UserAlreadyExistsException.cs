namespace EventTicketSystem.Middleware.AuthenticationExceptions;

public class UserAlreadyExistsException(string message) : DomainException(message, StatusCodes.Status409Conflict)
{
    
}