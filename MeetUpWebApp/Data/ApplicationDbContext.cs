using MeetUpWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<OrganizerReview> OrganizerReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RSVP>()
                .HasOne(e => e.User)
                .WithMany(u => u.RSVPs)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<RSVP>()
                .HasOne(e => e.Event)
                .WithMany(ev => ev.RSVPs)
                .HasForeignKey(r => r.EventId);

            modelBuilder.Entity<Comment>()
            .HasOne(e => e.Event)
            .WithMany(c => c.Comments)
            .HasForeignKey(r => r.EventId);

            modelBuilder.Entity<OrganizerReview>()
                .HasOne(r => r.Organizer)
                .WithMany(u => u.OrganizerReviews)
                .HasForeignKey(r => r.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrganizerReview>()
             .HasOne(r => r.ReviewerUser)
             .WithMany(u => u.ReviewsWritten)
             .HasForeignKey(r => r.ReviewerUserId)
             .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

        }
    }
}
