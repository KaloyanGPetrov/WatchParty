using WatchParty.Data.Enteties;

namespace WatchParty.DTOs
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<MovieDTO> ?Movies { get; } = [];
    }
}
