using EventTicketSystem_DTOs.AuthDto;
using EventTicketSystem.Models;

namespace EventTicketSystem.Services.AuthServices;

public interface IAuthService
{
// current user
    public Task<AuthResultDto> RegisterUserAsync(RegisterUserDto registerUserDto);
    public Task<AuthResultDto> LoginUserAsync(LoginUserDto loginUserDto);
    public Task LogoutUserAsync();
    public ReturnCurrentUserDto GetCurrentUser();
    public Task<string> GenerateJwtToken(ApplicationUser user);
    public string GetUserId();
}