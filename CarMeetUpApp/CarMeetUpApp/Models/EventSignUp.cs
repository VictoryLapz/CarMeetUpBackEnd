using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Models
{
    public class EventSignUp
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

    }
}
