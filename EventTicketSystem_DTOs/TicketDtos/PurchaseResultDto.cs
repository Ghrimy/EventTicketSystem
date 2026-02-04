namespace EventTicketSystem_DTOs.TicketDtos;

public class PurchaseResultDto
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public decimal PricePerTicket { get; set; }
    public decimal TotalPrice { get; set; }

    public DateTime PurchasedAt { get; set; }
}
