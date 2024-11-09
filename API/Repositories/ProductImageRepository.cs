using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductImageRepository : RepositoryAutoMap<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task AddImagesAsync(string productAsin, List<CreateProductImageModel> imageModels)
        {
            var images = imageModels.Select(i => new ProductImage
            {
                Asin = productAsin,
                ImageUrl = i.ImageUrl
            }).ToList();

            await db.Set<ProductImage>().AddRangeAsync(images);
            await db.SaveChangesAsync();
        }

        public async Task ArchiveImagesAsync(List<int> imageIds)
        {
            var images = await db.Set<ProductImage>().Where(img => imageIds.Contains(img.Id)).ToListAsync();
            foreach (var image in images)
            {
                image.IsArchived = true;
            }
            db.Set<ProductImage>().UpdateRange(images);
            await db.SaveChangesAsync();
        }
    }

}
