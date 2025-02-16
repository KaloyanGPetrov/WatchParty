using AutoMapper;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;

namespace WatchParty.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>()
                .ReverseMap();
        }
    }
}
