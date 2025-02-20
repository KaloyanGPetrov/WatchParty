using System.Drawing.Text;
using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.Repositories.Abstractions;

namespace WatchParty.Repositories
{
    public class ActorRepository: CrudRepository<Actor>, IActorRepository
    {
        private readonly ApplicationDbContext _context;
        public ActorRepository(ApplicationDbContext context) : base (context) 
        {
            _context = context;
        }

        public async Task UpdateActor(Actor actor, List<Movie> movies)
        {
            _context.ChangeTracker.LazyLoadingEnabled = true;
            _context.Actor.Attach(actor);

            if(!_context.Entry(actor).Collection(a => a.Movies).IsLoaded)
            {
                await _context.Entry(actor).Collection(a => a.Movies).LoadAsync();
            }
            actor.Movies = movies;

            await UpdateAsync(actor);
        }
    }
}
