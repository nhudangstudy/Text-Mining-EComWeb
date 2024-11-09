namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductColorRepository _colorRepository;
        private readonly IProductImageRepository _imageRepository;
        private readonly IProductPriceHistoryRepository _priceHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IProductColorRepository colorRepository,
            IProductImageRepository imageRepository,
            IProductPriceHistoryRepository priceHistoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _imageRepository = imageRepository;
            _priceHistoryRepository = priceHistoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductResponseModel>> GetAllProductsPublicAsync()
        {
            var products = _productRepository.GetAllAsync<ProductResponseModel>();

            // Convert IAsyncEnumerable to List
            var productList = await products.ToListAsync();

            // Filter in memory
            var filteredProducts = productList.Where(p => !p.IsArchived);
            return filteredProducts;
        }

        public async Task<IEnumerable<ProductResponseModel>> GetAllProductsAdminAsync()
        {
            var products = _productRepository.GetAllAsync<ProductResponseModel>();

            // Convert IAsyncEnumerable to List
            var productList = await products.ToListAsync();

            return productList;
        }

        public async Task AddWithDetailsAsync(CreateProductModel model)
        {
            await _productRepository.AddWithDetailsAsync(model);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(string asin, CreateProductRequestModel model)
        {
            var existingProduct = await _productRepository.GetByIdAsync<CreateProductRequestModel>(asin);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // Update product and related entities
            model.Asin = asin; // Ensure ASIN is set in the model
            await _productRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProductPriceAsync(string asin, CreateProductPriceModel priceModel)
        {
            var product = await _productRepository.GetByIdAsync<CreateProductModel>(asin);
            if (product != null)
            {
                await _priceHistoryRepository.AddPriceHistoryAsync(asin, priceModel);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Product not found.");
            }
        }

        public async IAsyncEnumerable<ProductResponseModel> GetAllProductAsync(int size, int? page = 1)
        {
            int? skip = page.HasValue ? page.Value * size : null;
            var products = await _productRepository.GetAllAsync<ProductResponseModel>(skip).ToListAsync();

            // Random shuffle logic
            var random = new Random();
            products = products.OrderBy(_ => random.Next()).ToList();

            foreach (var product in products)
            {
                yield return product;
            }
        }


        public async Task<ProductResponseModel> GetProductById(string asin)
        {
            var product = await _productRepository.GetByIdAsync<ProductResponseModel>(asin);
            return product;
        }
    }
}
