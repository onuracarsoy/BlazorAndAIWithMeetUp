using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.ViewOrganizerReviews
{
    public class ViewOrganizerReviewsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ViewOrganizerReviewsService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            return await context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<OrganizerReview>> GetOrganizerReviewsAsync(int organizerId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.OrganizerReviews
                .Where(r=>r.OrganizerId == organizerId)
                .Include(r=>r.ReviewerUser)
                .ToListAsync();

        }

        public async Task<(int reviewCount, int averageRating)> GetOrganizerAverageRatingAsync(int organizerId)
        {
            int reviewCount = 0;
            double averageRating = 0;

            await using var context = await _dbContextFactory.CreateDbContextAsync();

             reviewCount = await context.OrganizerReviews.CountAsync(r => r.OrganizerId == organizerId);
            if(reviewCount > 0)
            {
                averageRating = await context.OrganizerReviews
             .Where(r => r.OrganizerId == organizerId)
             .AverageAsync(r => r.Rating);

            }



            averageRating = Math.Round(averageRating, MidpointRounding.AwayFromZero);

            return (reviewCount, (int)averageRating);
        }



    }
}
