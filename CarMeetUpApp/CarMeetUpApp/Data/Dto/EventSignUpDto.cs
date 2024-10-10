namespace CarMeetUpApp.Data.Dto;

public class EventSignUpDto
{

    public int EventId { get; set; }
    public string? EventName { get; set; }
    public string? EventDescription { get; set; }
    public string? EventLocation { get; set; }
    public DateTime EventTime { get; set; }
}
