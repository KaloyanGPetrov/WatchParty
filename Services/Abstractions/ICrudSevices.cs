using WatchParty.DTOs;

namespace WatchParty.Services.Abstractions
{
    public interface ICrudSevices<T>
        where T : BaseDTO
    {
        Task<T> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task CreateAsync(T dto);
        Task UpdateAsync(T dto);
        Task DeleteAsync(int dtoId);
        ICollection<T> GetByName(string name);
    }
}
