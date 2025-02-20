using WatchParty.DTOs;

namespace WatchParty.Services.Abstractions
{
    public interface IMovieServices 
    {
        Task<ICollection<MovieDTO>> GetAllAsync();
        Task<MovieDTO> GetByIdAsync(int id);
        Task<MovieCreateEditDTO> GetByIdEditAsync(int id);
        ICollection<MovieDTO> GetByName(string name);
        Task CreateAsync(MovieCreateEditDTO model);
        Task DeleteAsync(int id);
        Task UpdateAsync(MovieCreateEditDTO model);
    }
}
