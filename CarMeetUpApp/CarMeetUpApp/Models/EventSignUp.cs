using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Models
{
    public class EventSignUp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EventId { get; set; }

    }
}
