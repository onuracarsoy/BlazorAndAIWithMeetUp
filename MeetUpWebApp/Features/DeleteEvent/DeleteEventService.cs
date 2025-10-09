using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.DeleteEvent
{
    public class DeleteEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public DeleteEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<bool> DeleteEventAsync(EventViewModel? eventViewModel)
        {
            using var context = _contextFactory.CreateDbContext();
            var findEvent = await context.Events.FirstOrDefaultAsync(x=>x.EventId == eventViewModel.EventId);
            if (findEvent != null)
            {
                context.Events.Remove(findEvent);
                await context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }

}

