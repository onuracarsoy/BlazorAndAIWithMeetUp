using AutoMapper;
using MeetUpWebApp.Data.Entities;
using MeetUpWebApp.Shared.ViewModels;

namespace MeetUpWebApp.Shared
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<EventViewModel, Event>().ReverseMap();
            CreateMap<CommentViewModel, Comment>().ReverseMap();
        }
    }
}
