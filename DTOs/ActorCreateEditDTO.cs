namespace WatchParty.DTOs
{
    public class ActorCreateEditDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public string ImageUrl { get; set; }

        public List<MovieDTO>? Movies { get; set; }

        public List<int> ?MoviesIds { get; set; } 
    }
}
