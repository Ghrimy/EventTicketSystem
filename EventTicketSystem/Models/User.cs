using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem.Models;

public enum Role
{
    Admin,
    User
}
public class User
{
    //id + login
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public string UserEmail { get; set; } = string.Empty;
    public Role Role { get; set; }
    
    // Personal details
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    
    public ICollection<EventTicket> Tickets { get; set; } = new List<EventTicket>();
}