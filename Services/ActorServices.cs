using AutoMapper;
using System.Security.AccessControl;
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
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public ActorServices(IActorRepository actorRepository, IMovieRepository movieRepository ,IMapper mapper)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ActorCreateEditDTO dto)
        {
            Actor actor = _mapper.Map<Actor>(dto);

            if (dto.MoviesIds != null)
            {
                var movies = dto.MoviesIds
                   .Select(item => _movieRepository.GetByIdAsync(item).Result)
                   .ToList();

                actor.Movies = movies;
            }
            else
            {
                actor.Movies = new List<Movie>();
            }
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

        public async Task<ActorCreateEditDTO> GetByIdEditAsync(int id)
        {
            var actor = await _actorRepository.GetByIdAsync(id);
            return _mapper.Map<ActorCreateEditDTO>(actor);
        }

        public ICollection<ActorDTO> GetByName(string name)
        {
            var actors =  _actorRepository.GetByFilter(act => act.FirstName == name || act.LastName == name);
            return _mapper.Map<ICollection<ActorDTO>>(actors);
        }


        public async Task UpdateAsync(ActorCreateEditDTO dto)
        {
            Actor actor = _mapper.Map<Actor>(dto);
            List<Movie> movies;

            if (dto.MoviesIds != null)
            {
                movies = dto.MoviesIds
                   .Select(item => _movieRepository.GetByIdAsync(item).Result)
                   .ToList();
            }
            else
            {
                movies = new List<Movie>();
            }


            await _actorRepository.UpdateActor(actor, movies);
        }

    }
}
