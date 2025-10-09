using AutoMapper;
using MeetUpWebApp.Data;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetUpWebApp.Features.LeaveEventComments
{
    public class LeaveEventCommentsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IMapper mapper;

        public LeaveEventCommentsService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        public async Task<List<CommentViewModel>> GetCommentsByEventIdAsync(int eventId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var comments = await context.Comments
                 .Where(c => c.EventId == eventId)
                 .ToListAsync();
            var commentsViewModels = mapper.Map<List<CommentViewModel>>(comments);
            return commentsViewModels;
        }

        public async Task AddCommentAsync(CommentViewModel comment)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var commentEntity = mapper.Map<Comment>(comment);
            context.Comments.Add(commentEntity);
           await context.SaveChangesAsync();
        }
    }
}
