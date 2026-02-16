namespace EventTicketSystem.Middleware.EventExceptions;

public class ValidPurchaseAmountException(int amount)
    : DomainException($"You can't buy {amount} of tickets.", StatusCodes.Status406NotAcceptable);