using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem_DTOs.TicketDtos;

public class PurchaseTicketDto
{
    [Range(1, int.MaxValue)]
    public int EventId { get; set; }
    
    [Range(1,5)] 
    public int Quantity { get; set; }
}
