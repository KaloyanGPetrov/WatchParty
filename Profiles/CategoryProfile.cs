using WatchParty.Data.Enteties;
using WatchParty.DTOs;
using AutoMapper;

namespace WatchParty.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>()
               .ReverseMap();
        }
    }
}
