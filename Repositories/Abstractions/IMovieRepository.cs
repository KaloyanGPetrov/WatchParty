using WatchParty.Data.Enteties;

namespace WatchParty.Repositories.Abstractions
{
    public interface IMovieRepository : ICrudRepository<Movie>
    {
        public Task UpdateMovie(Movie movie, List<Actor> actors, List<Category> categories);
    }
}
