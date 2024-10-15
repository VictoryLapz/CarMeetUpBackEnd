using System.ComponentModel.DataAnnotations;

namespace CarMeetUpApp.Models
{
    public class Event : EventBaseModel
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        public string? Description { get; set; }
        public int OrganizedId { get; set; }

        [Range(1, 10000)]
        public int Capacity { get; set; }

        // this will be the foreign key
        public int CarId { get; set; }

        // this will be used to connect above foreign key to the car table/model
        public Car? CarSearch { get; set; }
    }
}


