using AutoMapper;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;
using WatchParty.Repositories;
using WatchParty.Repositories.Abstractions;
using WatchParty.Services.Abstractions;

namespace WatchParty.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieServices(IMovieRepository movieRepository, IMapper mapper)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task CreateAsync(MovieDTO dto)
        {
            Movie movie = _mapper.Map<Movie>(dto);
            await _movieRepository.CreateAsync(movie);
        }

        public async Task DeleteAsync(int dtoId)
        {
            await _movieRepository.DeleteByIdAsync(dtoId);
        }

        public async Task<ICollection<MovieDTO>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return _mapper.Map<ICollection<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> GetByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return _mapper.Map<MovieDTO>(movie);
        }

        public ICollection<MovieDTO> GetByName(string name)
        {
            var movies = _movieRepository.GetByFilter(m => m.Name == name);
            return _mapper.Map<ICollection<MovieDTO>>(movies);
        }

        public async Task UpdateAsync(MovieDTO dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            await _movieRepository.UpdateAsync(movie);
        }
    }
}
