using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem.Models;


public class EventTicket
{
    public int EventTicketId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalAvailable { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
}
