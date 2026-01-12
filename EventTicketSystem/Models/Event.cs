using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem.Models;

public class Event
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; } = string.Empty;

    public ICollection<EventTicket> TicketTypes { get; set; } = new List<EventTicket>();
}