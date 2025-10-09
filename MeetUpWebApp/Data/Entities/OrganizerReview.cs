using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MeetUpWebApp.Data.Entities
{
    public class OrganizerReview
    {
        public int OrganizerReviewId { get; set; }

        [Range(1,5,ErrorMessage ="Lütfen 1 ile 5 arasında bir değerlendirme yapınız.")]
        public int Rating { get; set; }

        public  string? ReviewText{ get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public int OrganizerId { get; set; }

        [JsonIgnore]
        public User? Organizer { get; set; }

        public int ReviewerUserId { get; set; }

        [JsonIgnore]
        public User? ReviewerUser  { get; set; }
    }
}
