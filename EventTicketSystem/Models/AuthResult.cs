namespace EventTicketSystem.Models;

public class AuthResult
{
    public bool Succeeded { get; init; }
    public string? Error { get; init; }
    public string? AccessToken { get; init; }
    public ApplicationUser? User { get; init; }

}
