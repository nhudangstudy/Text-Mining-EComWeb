namespace API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseModel?> GetByIdAsync(int id)
            => _mapper.Map<CategoryResponseModel>(await _categoryRepository.GetByIdAsync<CategoryResponseModel>(id));

        public IAsyncEnumerable<CategoryResponseModel> GetAllAsync(int size, int? page = 1)
        {
            int? skip = page.HasValue ? page.Value * size : null;
            return _categoryRepository.GetAllAsync<CategoryResponseModel>(skip);
        }
        public async Task AddAsync(CreateCategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCategoryModel model)
        {
            await _categoryRepository.UpdateAsync(id, model);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task SoftDeleteAsync(int id) 
        { 
            await _categoryRepository.SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }

}
