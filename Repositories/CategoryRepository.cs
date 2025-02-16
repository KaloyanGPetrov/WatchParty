using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.Repositories.Abstractions;

namespace WatchParty.Repositories
{
    public class CategoryRepository: CrudRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) 
        {
            
        }
    }
}
