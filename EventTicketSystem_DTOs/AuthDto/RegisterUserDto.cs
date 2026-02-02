using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem_DTOs.AuthDto;

public class RegisterUserDto
{
    [Required][MaxLength(255)] public string UserName { get; set; } = string.Empty;
    [Required][MaxLength(255)][DataType(DataType.EmailAddress)] public string Email { get; set; } = string.Empty;
    [Required][MaxLength(255)][DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
    [Required][MaxLength(255)][DataType(DataType.Password)] public string ConfirmPassword { get; set; } = string.Empty;
}