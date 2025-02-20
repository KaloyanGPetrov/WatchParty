using WatchParty.Data.Enteties;

namespace WatchParty.Repositories.Abstractions
{
    public interface IActorRepository : ICrudRepository<Actor>
    {
        public Task UpdateActor(Actor actor, List<Movie> movies);
    }
}
