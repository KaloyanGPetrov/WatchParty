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
        private readonly IActorRepository _actorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public MovieServices(IMovieRepository movieRepository, IMapper mapper, IActorRepository actorRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _movieRepository = movieRepository;
        }

        public async Task CreateAsync(MovieCreateEditDTO dto)
        {
            Movie movie = _mapper.Map<Movie>(dto);
            if (dto.CategoryIds != null)
            {
                var categories = dto.CategoryIds.Select(item => _categoryRepository.GetByIdAsync(item).Result).ToList();
                movie.Categories = categories;
            }
            else
            {
                movie.Categories = new List<Category>();
            }

            if (dto.ActorIds != null)
            {
                var actors = dto.ActorIds.Select(item => _actorRepository.GetByIdAsync(item).Result).ToList();
                movie.Actors = actors; 
            }
            else
            {
                movie.Actors = new List<Actor>();
            }


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

        public async Task<MovieCreateEditDTO> GetByIdEditAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return _mapper.Map<MovieCreateEditDTO>(movie);
        }

        public ICollection<MovieDTO> GetByName(string name)
        {
            var movies = _movieRepository.GetByFilter(m => m.Name == name);
            return _mapper.Map<ICollection<MovieDTO>>(movies);
        }

        public  ICollection<MovieDTO> GetByCategory(string category)
        {
            var categoryModel = _categoryRepository.GetByFilter(c => c.Name == category);
            if (categoryModel.Count > 0)
            {
                var movies = _movieRepository.GetByFilter(m => m.Categories.Contains(categoryModel.First()));
                return _mapper.Map<ICollection<MovieDTO>>(movies);
            }
            else
            {
                var movies = new List<MovieDTO>();
                return _mapper.Map<ICollection<MovieDTO>>(movies);
            }
        }

        public async Task UpdateAsync(MovieCreateEditDTO dto)
        {
            Movie movie = _mapper.Map<Movie>(dto);

            List<Category> categories;
            List<Actor> actors;

            if (dto.CategoryIds != null)
            {
                categories = dto.CategoryIds.Select(item => _categoryRepository.GetByIdAsync(item).Result).ToList();
                
            }
            else
            {
                categories = new List<Category>();
            }

            if (dto.ActorIds != null)
            {
                actors = dto.ActorIds.Select(item => _actorRepository.GetByIdAsync(item).Result).ToList();
              
            }
            else
            {
                actors = new List<Actor>();
            }

            await _movieRepository.UpdateMovie(movie, actors, categories);
        }
    }
}
