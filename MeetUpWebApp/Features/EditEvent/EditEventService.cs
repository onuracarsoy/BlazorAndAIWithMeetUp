using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.EditEvent
{
    public class EditEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public EditEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<EventViewModel?> GetEventByIdAsync(int eventId)
        {
            using var context = _contextFactory.CreateDbContext();
            var eventEntity = await context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return null;
            }
            return _mapper.Map<EventViewModel>(eventEntity);
        }

        public async Task UpdateEventAsync(EventViewModel eventViewModel)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingEvent = await context.Events.FindAsync(eventViewModel.EventId);
            _mapper.Map(eventViewModel, existingEvent);
            await context.SaveChangesAsync();
       
        }
    }
}

