using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem_DTOs.AuthDto;

public class AuthResultDto
{
    public bool Succeeded { get; set; }
    public string? Error { get; set; }
    public string? Token { get; set; }
}
