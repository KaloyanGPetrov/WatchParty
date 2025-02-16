using AutoMapper;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;
using WatchParty.Repositories.Abstractions;
using WatchParty.Services.Abstractions;

namespace WatchParty.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CategoryDTO dto)
        {
            Category category = _mapper.Map<Category>(dto);
            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(int dtoId)
        {
            await _categoryRepository.DeleteByIdAsync(dtoId);
        }

        public async Task<ICollection<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<ICollection<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public ICollection<CategoryDTO> GetByName(string name)
        {
            var categories = _categoryRepository.GetByFilter(cat => cat.Name == name);
            return _mapper.Map<ICollection<CategoryDTO>>(categories);
        }

        public async Task UpdateAsync(CategoryDTO dto)
        {
            Category category = _mapper.Map<Category>(dto);
            await _categoryRepository.UpdateAsync(category);
        }



    }
}
