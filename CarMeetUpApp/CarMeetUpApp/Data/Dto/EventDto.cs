using System.ComponentModel.DataAnnotations;
using CarMeetUpApp.Models;

namespace CarMeetUpApp.Data.Dto;

public class EventDto
{
    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Location { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public int Capacity { get; set; }
}
