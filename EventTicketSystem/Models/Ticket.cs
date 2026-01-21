namespace EventTicketSystem.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public int Quantity { get; set; }
    public DateTime PurchasedAt { get; set; }

    public string ApplicationUserId { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public int EventTicketId { get; set; }
    public EventTicket EventTicket { get; set; } = null!;
}
