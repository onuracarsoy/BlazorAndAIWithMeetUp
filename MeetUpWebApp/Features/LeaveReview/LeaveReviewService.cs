using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Stripe;

namespace MeetUpWebApp.Features.LeaveReview
{
    public class LeaveReviewService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public LeaveReviewService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _dbContextFactory = contextFactory;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            return await context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> ReviewOrganizerAsync(OrganizerReview organizerReview)
        {
            if (organizerReview == null ||
               organizerReview.OrganizerId <= 0 ||
               organizerReview.ReviewerUserId <= 0 ||
               organizerReview.Rating < 1 ||
               organizerReview.Rating > 5 ||
               organizerReview.ReviewerUserId == organizerReview.OrganizerId)
            {
                return false; // Invalid review data
            }

            await using var context = await _dbContextFactory.CreateDbContextAsync();

            // Check if the review attended any events organized by this organizer
            if (!await context.RSVPs
                .Include(r => r.Event)
                .AnyAsync(r => r.UserId == organizerReview.ReviewerUserId &&
                    r.Status == SharedHelper.GoingStatus &&
                    r.Event.OrganizerId == organizerReview.OrganizerId &&
                    r.Event.EndDate < DateOnly.FromDateTime(DateTime.Now)))
                return false; // User has not attended any events organized by this organizer

            // Check if the reviewer has already reviewed this organizer
            if (await context.OrganizerReviews
                .AnyAsync(r => r.OrganizerId == organizerReview.OrganizerId && r.ReviewerUserId == organizerReview  .ReviewerUserId))
                return false; // User has already reviewed this organizer

            context.OrganizerReviews.Add(organizerReview);
            await context.SaveChangesAsync();

            return true;


        }
    }
}
