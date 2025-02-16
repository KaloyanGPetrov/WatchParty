using  WatchParty.Data.Enteties;

namespace WatchParty.Repositories.Abstractions
{
    public interface ICrudRepository<T>
        where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        ICollection<T> GetByFilter(Func<T, bool> predicate);
        Task<T?> GetByIdAsync(int id);
    }
}
