using System.ComponentModel.DataAnnotations;

namespace EventTicketSystem_DTOs.EventDtos;

public class ShowAllEventsDto
{
    [Required] public string EventName { get; set; } = string.Empty;
    [Required] public string EventDescription { get; set; } = string.Empty;
    [Required] public DateTime EventDate { get; set; }
    [Required] public string EventLocation { get; set; } = string.Empty;
}