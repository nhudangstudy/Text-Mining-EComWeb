using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductPriceHistoryRepository : RepositoryAutoMap<ProductPriceHistory>, IProductPriceHistoryRepository
    {

        public ProductPriceHistoryRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task AddPriceHistoryAsync(string productAsin, CreateProductPriceModel priceModel)
        {
            var priceHistory = new ProductPriceHistory
            {
                Asin = productAsin,
                Price = priceModel.Price,
                Discount = priceModel.Discount,
                DateUpdated = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await db.Set<ProductPriceHistory>().AddAsync(priceHistory);
            await db.SaveChangesAsync();
        }
    }

}
