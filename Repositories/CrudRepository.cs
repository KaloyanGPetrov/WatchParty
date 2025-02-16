using Microsoft.EntityFrameworkCore;
using WatchParty.Data.Enteties;
using WatchParty.Data;
using WatchParty.Repositories.Abstractions;

namespace WatchParty.Repositories
{
    public class CrudRepository<T> : ICrudRepository<T>
        where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _set;
        public CrudRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            _set.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _set.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Item with id {id} does not exist.");
            }
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public ICollection<T> GetByFilter(Func<T, bool> predicate)
        {
            return _set
                .Where(predicate)
                .ToList();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
