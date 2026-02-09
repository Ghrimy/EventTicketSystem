namespace EventTicketSystem_DTOs.TicketDtos;

public class GetTicketInformationDto
{
    public int TicketId { get; set; }
    public decimal PricePaid { get; set; }
    public DateTime PurchasedAt { get; set; }
    public int Quantity { get; set; }
    
    public string EventName { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; } = string.Empty;
}