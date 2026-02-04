namespace EventTicketSystem.Models;

public class Event
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; } = string.Empty;

    public int TotalTickets { get; set; }
    public decimal TicketPrice { get; set; }

    public string OrganizerId { get; set; } = default!;
    public ApplicationUser Organizer { get; set; } = default!;

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
