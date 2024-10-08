using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Models;

public class User
{
   [Key]
   public int UserId { get; set; }
   [Required]
   public string? FirstName { get; set; }
   [Required]
   public string? LastName { get; set; }
   [Required]
   public string? Email { get; set; }
   [MaxLength(500)]
   public string? Bio { get; set; }
   [MaxLength(100)]
   public string? CarInterests { get; set; }

}


