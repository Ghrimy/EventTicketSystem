using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventTicketSystem_DTOs.AuthDto;
using EventTicketSystem.Middleware.AuthenticationExceptions;
using EventTicketSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EventTicketSystem.Services.AuthServices;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration) : IAuthService
{
    public async Task<AuthResultDto> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var existingUser = await userManager.FindByEmailAsync(registerUserDto.Email);
        if(existingUser != null) 
            throw new UserAlreadyExistsException("Email is already used.");
        
        if(registerUserDto.Password != registerUserDto.ConfirmPassword) 
            throw new NoPasswordMatchException("Passwords do not match");
        
        var user = new ApplicationUser {UserName = registerUserDto.UserName, Email = registerUserDto.Email };
        var result = await userManager.CreateAsync(user, registerUserDto.Password);
        
        if (!result.Succeeded)
        {
            return new AuthResultDto
            {
                Succeeded = false,
                Error = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
        
        await userManager.AddToRoleAsync(user, "User");
        return new AuthResultDto()
        {
            Succeeded = result.Succeeded
        };
    }

    public async Task<AuthResultDto> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var user = await userManager.FindByEmailAsync(loginUserDto.Email);
        if (user == null) throw new UserDoesNotExistException("User does not exist");

        var result = await signInManager.PasswordSignInAsync(user, loginUserDto.Password, false, false);
        if (!result.Succeeded) throw new NoPasswordMatchException("Password is incorrect");

        var token = await GenerateJwtToken(user);

        return new AuthResultDto { Succeeded = true, Token = token };
    }


    public async Task LogoutUserAsync()
    {
       await signInManager.SignOutAsync();
    }

    public ReturnCurrentUserDto GetCurrentUser()
    {
        var user =  httpContextAccessor.HttpContext?.User;
        if(user == null || !user.Identity.IsAuthenticated) throw new UnauthorizedAccessException("User is not authenticated");
        
        return new ReturnCurrentUserDto()
        {
            Id = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UserIdNotFoundException("Authenticated user has no Id claim."),
            Email = user.FindFirstValue(ClaimTypes.Email) ?? throw new UserEmailNotFoundException("Authenticated user has no Email claim."),
            Roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList()
        };
    }

    public async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity!.IsAuthenticated)
            throw new UnauthorizedAccessException("User is not authenticated");

        return user.FindFirstValue(ClaimTypes.NameIdentifier)
               ?? throw new UserIdNotFoundException("Authenticated user has no Id claim.");
    }
}