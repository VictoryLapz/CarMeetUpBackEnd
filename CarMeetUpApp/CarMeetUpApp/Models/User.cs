using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Models;

public class User
{
   [Key]
   int UserId { get; set; }
   [Required]
   string FirstName { get; set; }
   [Required]
   string LastName { get; set; }
   [Required]
   string Email { get; set; }
   [MaxLength(500)]
   string Bio { get; set; }
   [MaxLength(100)]
   string CarInterests { get; set; }


}


