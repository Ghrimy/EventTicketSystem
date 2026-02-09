using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem.Models;

public class Event
{
    // Event
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; } = string.Empty;

    //Tickets
    public int TotalTickets { get; set; }
    public int TicketsSold { get; set; }
    public decimal TicketPrice { get; set; }

    //Organizer features
    public string OrganizerId { get; set; } = default!;
    public ApplicationUser Organizer { get; set; } = default!;
    
    // All Tickets sold
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    //Concurrency check
    [ConcurrencyCheck] public byte[] RowVersion { get; set; }
}
