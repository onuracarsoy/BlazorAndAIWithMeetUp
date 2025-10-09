using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MeetUpWebApp.Data.Entities
{
    public class RSVP
    {
        public int RSVPId { get; set; }

        public int EventId { get; set; }

        [JsonIgnore]
        public Event? Event { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public DateTime RSVPDate { get; set; }

        [Required]
        public string? Status { get; set; }

    

    }
}
