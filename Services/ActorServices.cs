using AutoMapper;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;
using WatchParty.Repositories;
using WatchParty.Repositories.Abstractions;
using WatchParty.Services.Abstractions;

namespace WatchParty.Services
{
    public class ActorServices : IActorServices
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        public ActorServices(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ActorDTO dto)
        {
            Actor actor = _mapper.Map<Actor>(dto);
            await _actorRepository.CreateAsync(actor);
        }

        public async Task DeleteAsync(int dtoId)
        {
            await _actorRepository.DeleteByIdAsync(dtoId);
        }

        public async Task<ICollection<ActorDTO>> GetAllAsync()
        {
            var actors = await _actorRepository.GetAllAsync();
            return _mapper.Map<ICollection<ActorDTO>>(actors);
        }

        public async Task<ActorDTO> GetByIdAsync(int id)
        {
            var actor = await _actorRepository.GetByIdAsync(id);
            return _mapper.Map<ActorDTO>(actor);
        }

        public ICollection<ActorDTO> GetByName(string name)
        {
            var actors = _actorRepository.GetByFilter(act => act.FirstName == name || act.LastName == name);
            return _mapper.Map<ICollection<ActorDTO>>(actors);
        }

        public async Task UpdateAsync(ActorDTO dto)
        {
            Actor actor = _mapper.Map<Actor>(dto);
            await _actorRepository.UpdateAsync(actor);
        }
    }
}
