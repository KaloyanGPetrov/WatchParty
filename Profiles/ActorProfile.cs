using AutoMapper;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;

namespace WatchParty.Profiles
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorDTO>()
                .ReverseMap();

            CreateMap<Actor, ActorCreateEditDTO>()
                .ReverseMap();
        }
    }
}
