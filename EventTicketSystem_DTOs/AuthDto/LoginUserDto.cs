using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem_DTOs.AuthDto;

public class LoginUserDto
{
    [Required][MaxLength(255)] public string Email { get; set; } = string.Empty;
    [Required][MaxLength(255)] public string Password { get; set; } = string.Empty;
}