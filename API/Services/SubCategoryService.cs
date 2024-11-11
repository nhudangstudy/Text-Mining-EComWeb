namespace API.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubCategoryService(ISubCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task<SubCategoryResponseModel?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<SubCategoryResponseModel>> GetAllAsync() => _repository.GetAllAsync();

        public async Task UpdateAsync(int id, UpdateSubCategoryModel model)
        {
            await _repository.UpdateAsync(id, model);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task CreateAsync(CreateSubCategoryModel subCategory)
        {
            await _repository.CreateAsync(subCategory);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
