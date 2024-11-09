using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductColorRepository : RepositoryAutoMap<ProductColor>, IProductColorRepository
    {

        public ProductColorRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task AddColorsAsync(string productAsin, List<CreateProductColorModel> colorModels)
        {
            var colors = colorModels.Select(c => new ProductColor
            {
                Asin = productAsin,
                ColorHex = c.ColorHex
            }).ToList();

            await db.Set<ProductColor>().AddRangeAsync(colors);
            await db.SaveChangesAsync();
        }
    }

}
