using WatchParty.Data.Enteties;
using WatchParty.DTOs;

namespace WatchParty.Services.Abstractions
{
    public interface IActorServices
    {
        Task<ICollection<ActorDTO>> GetAllAsync();
        Task<ActorDTO> GetByIdAsync(int id);
        Task<ActorCreateEditDTO> GetByIdEditAsync(int id);
        ICollection<ActorDTO> GetByName (string name);
        Task CreateAsync(ActorCreateEditDTO model);
        Task DeleteAsync(int id);
        Task UpdateAsync(ActorCreateEditDTO model);

    }
}
