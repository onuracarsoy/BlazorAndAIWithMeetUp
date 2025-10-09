using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MeetUpWebApp.Data.Entities
{
    public class Comment
    {
       
        public int CommentId { get; set; }

        [Required]
        [StringLength(maximumLength: 1000)]
        public string? Message { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public DateTime PostedOn { get; set; } = DateTime.Now;

        public int EventId { get; set; }

        [JsonIgnore]
        public Event? Event { get; set; }
    }
}
