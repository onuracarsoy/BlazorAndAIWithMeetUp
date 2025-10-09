using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MeetUpWebApp.Features.ViewCreatedEvents
{
    public class ViewCreatedEventService
    {

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public ViewCreatedEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<EventViewModel>> GetEventsCreatorIdAsync(string strCreatorId)
        {
            if(int.TryParse(strCreatorId, out int creatorId))
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                var events = await (context.Events.Where(e=>e.OrganizerId == creatorId)
                    .ToListAsync() ?? Task.FromResult(new List<Event>()));

                return _mapper.Map<List<EventViewModel>>(events);
            }
            return new List<EventViewModel>();
       
         
        }
    }
}
