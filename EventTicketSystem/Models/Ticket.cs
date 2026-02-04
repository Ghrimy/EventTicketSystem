namespace EventTicketSystem.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public decimal PricePaid { get; set; }
    public DateTime PurchasedAt { get; set; }
    public int Quantity { get; set; }

    public string ApplicationUserId { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
}

