namespace API.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BrandResponseModel?> GetByIdAsync(int id)
            => _mapper.Map<BrandResponseModel>(await _brandRepository.GetByIdAsync<BrandResponseModel>(id));

 
        public IAsyncEnumerable<BrandResponseModel> GetAllAsync(int size, int? page = 1)
        {
            int? skip = page.HasValue ? page.Value * size : null;
            return _brandRepository.GetAllAsync<BrandResponseModel>(skip);
        }


        public async Task AddAsync(CreateBrandModel model)
        {
            var brand = _mapper.Map<Brand>(model);
            await _brandRepository.CreateAsync(brand);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateBrandModel model)
        {
            await _brandRepository.UpdateAsync(id, model);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task SoftDeleteAsync(int id) 
        { 
            await _brandRepository.SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

        }
    }

}
