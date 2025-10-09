using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.ViewEvent
{
    public class ViewEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public ViewEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<EventViewModel?> GetEventByIdAsync(int eventId)
        {
            await using var context = _contextFactory.CreateDbContext();
            var eventEntity = await context.Events
                //.Include(e => e.OrganizerId)
                //.Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.EventId == eventId);
            if (eventEntity == null)
            {
                return null;
            }
            return _mapper.Map<EventViewModel>(eventEntity);
        }

        public async Task<List<User?>> GetAttendeesByEventIdAsync(int eventId)
        {
            using var context = _contextFactory.CreateDbContext();
            var attendees = await context.RSVPs
                .Where(r => r.EventId == eventId)
                .OrderByDescending(r => r.RSVPDate)
                .Include(r => r.User)
                .Where(r=>r.EventId == eventId && r.Status == SharedHelper.GoingStatus)
                .Select(r => r.User)
                .ToListAsync();
            return attendees;
        }
    }
}
