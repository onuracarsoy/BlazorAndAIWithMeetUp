using System.ComponentModel.DataAnnotations;

namespace MeetUpWebApp.Data.Entities
{
    public class User
    {

        public int UserId { get; set; }

        [Required]
        [StringLength(maximumLength:200)]
        public string? Name { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string? Email { get; set; }

        public string? Role { get; set; }

        public List<RSVP>? RSVPs { get; set; } = new List<RSVP>();

        public List<OrganizerReview> OrganizerReviews { get; set; } = new List<OrganizerReview>();

        public List<OrganizerReview> ReviewsWritten { get; set; } = new List<OrganizerReview>();
    }
}
