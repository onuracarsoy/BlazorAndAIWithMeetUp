using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.ManageUserRSVPEvents
{
    public class ManageUserRSVPEventsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper mapper;

        public ManageUserRSVPEventsService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<List<EventViewModel>> GetUserRSVPEventsAsync(int? userId)
        {
            using var dbContext = _contextFactory.CreateDbContext();
            var events = await dbContext.Events
                .Include(e => e.RSVPs)
                .Where(e => e.RSVPs.Any(r => r.UserId == userId))
                .ToListAsync();
            return mapper.Map<List<EventViewModel>>(events);
        }
    }
}
