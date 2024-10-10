using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMeetUpApp.Models;

public class Car
{
    [Key]
    public int CarId { get; set; }
    [Required]
    public string? Make { get; set; }
    [Required]
    public string? Model { get; set; }
    [Required(ErrorMessage = "Value for Year must be between 1900 and 2025.")]
    [Range(0,9999)]
    public int Year { get; set; }
    public CarTypeEnum CarType { get; set; }
    public FuelTypeEnum FuelType { get; set; }
    public TransmissionEnum TransmissionType { get; set; }
    [MaxLength(17)]
    public string? VIN { get; set; }
    public string? Color { get; set; }
 
 
}
