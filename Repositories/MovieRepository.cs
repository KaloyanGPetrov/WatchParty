using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.Repositories.Abstractions;

namespace WatchParty.Repositories
{
    public class MovieRepository : CrudRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateMovie (Movie movie, List<Actor> actors, List<Category> categories)
        {
            _context.ChangeTracker.LazyLoadingEnabled = true;
            _context.Movie.Attach(movie);

            if (!_context.Entry(movie).Collection(a => a.Actors).IsLoaded)
            {
                await _context.Entry(movie).Collection(a => a.Actors).LoadAsync();
            }

            if (!_context.Entry(movie).Collection(a => a.Categories).IsLoaded)
            {
                await _context.Entry(movie).Collection(a => a.Categories).LoadAsync();
            }

            movie.Actors = actors;
            movie.Categories = categories;

            await UpdateAsync(movie);
        }
    }
}
