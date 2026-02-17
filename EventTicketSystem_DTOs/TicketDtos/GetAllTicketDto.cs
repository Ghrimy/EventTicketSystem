namespace EventTicketSystem_DTOs.TicketDtos;

public class GetAllTicketDto
{
    public int TicketId { get; set; }
    public decimal PricePaid { get; set; }
    public DateTime PurchasedAt { get; set; }
    public int Quantity { get; set; } 
}