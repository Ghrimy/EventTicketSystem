namespace EventTicketSystem.Middleware.AuthenticationExceptions;

public class NoPasswordMatchException(string message) :DomainException(message, StatusCodes.Status409Conflict);