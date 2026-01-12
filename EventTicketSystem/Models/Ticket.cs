namespace EventTicketSystem.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public int Quantity { get; set; }
    public DateTime PurchasedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int EventTicketId { get; set; }
    public EventTicket EventTicket { get; set; } = null!;
}