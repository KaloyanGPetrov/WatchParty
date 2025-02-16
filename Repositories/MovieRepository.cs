using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.Repositories.Abstractions;

namespace WatchParty.Repositories
{
    public class MovieRepository : CrudRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context) 
        {
            
        }
    }
}
