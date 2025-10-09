using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.RSVPEvent
{
    public class RSVPEventService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public RSVPEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<bool> RSVPToEventAsync(int? eventId, string? email)
        {

            using var context = _contextFactory.CreateDbContext();
            var user = await context.Users?.FirstOrDefaultAsync(u => u.Email == email);
            int userId = user?.UserId ?? 0;
            var eventExists = await context.Events?.AnyAsync(e => e.EventId == eventId);

            if (!eventExists)
            {
                // User has already RSVPed
                return false;
            }
            var rsvpExists = await context.RSVPs?.AnyAsync(r => r.EventId == eventId && r.UserId == userId);
            if(rsvpExists)
            {
                // User has already RSVPed
                return false;
            }

            var rsvp = new RSVP
            {
                EventId = eventId ?? 0,
                UserId = userId,
                RSVPDate = DateTime.Now,
                Status = Shared.SharedHelper.GoingStatus,
            };
            context.RSVPs?.Add(rsvp);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckUserRSVPStatusAsync(int? eventId, string? email)
        {
            var context = _contextFactory.CreateDbContext();
            var user = await context.Users?.FirstOrDefaultAsync(u => u.Email == email);
            int userId = user?.UserId ?? 0;
            var rsvpExists = await context.RSVPs?.AnyAsync(r => r.EventId == eventId && r.UserId == userId);
            return rsvpExists;
        }

        //public async Task<bool> CancelRSVPAsync(int? eventId, string? email)
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    var user = await context.Users?.FirstOrDefaultAsync(u => u.Email == email);
        //    int userId = user?.UserId ?? 0;
        //    var rsvp = await context.RSVPs?.FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);
        //    if (rsvp == null)
        //    {

        //        return false;
        //    }
        //    context.RSVPs?.Remove(rsvp);
        //    await context.SaveChangesAsync();
        //    return true;
        //}
    }
}
