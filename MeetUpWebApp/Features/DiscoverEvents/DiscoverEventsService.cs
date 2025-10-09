using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.DiscoverEvents
{
    public class DiscoverEventsService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;
        public DiscoverEventsService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<EventViewModel>> GetEventsAsync(int pageNumber, int pageSize ,string? filter = "")
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var events = await SearchEvents(filter, context, pageNumber, pageSize);
            if (!string.IsNullOrWhiteSpace(filter) && events.Count == 0)
            {
                filter = null;
                events = await SearchEvents(filter, context, pageNumber, pageSize);
            }

            return _mapper.Map<List<EventViewModel>>(events);
        }

        private async Task<List<Event>> SearchEvents(string? filter, ApplicationDbContext context, int pageNumber,int pageSize)
        {
            //var events = await (context.Events
            //        .Where(e =>
            //            (string.IsNullOrEmpty(filter) ||
            //             e.Title.Contains(filter) ||
            //             e.Description.Contains(filter) ||
            //             e.Location.Contains(filter))
            //            &&
            //            (e.BeginDate > DateOnly.FromDateTime(DateTime.Now) ||
            //            (e.BeginDate == DateOnly.FromDateTime(DateTime.Now) &&
            //             e.BeginTime > TimeOnly.FromDateTime(DateTime.Now))))
            //        .OrderByDescending(e => e.BeginDate)
            //        .ThenByDescending(e => e.BeginTime)
            //        .ToListAsync() ?? Task.FromResult(new List<Event>()));

            //return events;

            var query = context.Events?
              .Where(e => (string.IsNullOrEmpty(filter) || e.Title.Contains(filter) || e.Description.Contains(filter) || e.Location.Contains(filter)) &&
                          (e.BeginDate > DateOnly.FromDateTime(DateTime.Now) ||
                           (e.BeginDate == DateOnly.FromDateTime(DateTime.Now) && e.BeginTime > TimeOnly.FromDateTime(DateTime.Now))))
              .OrderByDescending(e => e.BeginDate)
              .ThenByDescending(e => e.BeginTime);

            if (query != null)
            {
                return await query.Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();
            }

            return new List<Event>();
        }
    }
}
