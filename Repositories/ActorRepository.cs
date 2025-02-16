using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.Repositories.Abstractions;

namespace WatchParty.Repositories
{
    public class ActorRepository: CrudRepository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext context) : base (context) 
        {
            
        }
    }
}
