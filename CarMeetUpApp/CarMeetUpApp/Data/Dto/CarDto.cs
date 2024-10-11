using CarMeetUpApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Data.Dto;

public class CarDto
{
    public string? Make { get; set; }
    [Required]
    public string? Model { get; set; }
    [Required(ErrorMessage = "Value for Year must be between 1900 and 2025.")]
    [Range(0, 9999)]
    public int Year { get; set; }
    public CarTypeEnum CarType { get; set; }
    public FuelTypeEnum FuelType { get; set; }
    public TransmissionEnum TransmissionType { get; set; }
    public string? Color { get; set; }
}
