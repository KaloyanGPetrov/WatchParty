namespace WatchParty.DTOs
{
    public class MovieCreateEditDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string VideoPath { get; set; }

        public List<ActorDTO> ?Actors { get; set; }
        public List<CategoryDTO> ?Categories { get; set; }
        public List<int> ?ActorIds { get; set; } 

        public List<int> ?CategoryIds { get; set; }
    }
}
