using MeetUpWebApp.Features.CreateEvent;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace MeetUpWebApp.Data.Entities
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        [Required]
        public DateOnly BeginDate { get; set; }

        [Required]
        public TimeOnly BeginTime { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public string? Location { get; set; }

        public string? MeetupLink { get; set; }

        [Required]
        public string? Category { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public int OrganizerId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? TicketPrice { get; set; }

        public bool Refundable { get; set; }


        public List<RSVP>? RSVPs { get; set; } = new List<RSVP>();

        public List<Comment>? Comments { get; set; } = new List<Comment>();



    }
}
