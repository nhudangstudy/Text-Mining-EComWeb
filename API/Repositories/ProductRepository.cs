﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;

    namespace API.Repositories
    {
        public class ProductRepository : IProductRepository
        {
            private readonly DbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IProductColorRepository _colorRepository;
            private readonly IProductImageRepository _imageRepository;
            private readonly IProductPriceHistoryRepository _priceHistoryRepository;

            public ProductRepository(
                DbContext dbContext,
                IMapper mapper,
                IProductColorRepository colorRepository,
                IProductImageRepository imageRepository,
                IProductPriceHistoryRepository priceHistoryRepository)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _colorRepository = colorRepository;
                _imageRepository = imageRepository;
                _priceHistoryRepository = priceHistoryRepository;
            }

            public async Task AddWithDetailsAsync(CreateProductModel model)
            {
                var product = _mapper.Map<Product>(model);

                // Add the main product
                await _dbContext.Set<Product>().AddAsync(product);
                await _dbContext.SaveChangesAsync(); // Save to generate ASIN for related entities

                // Add related data through other repositories
                if (model.ProductColors.Any())
                {
                    await _colorRepository.AddColorsAsync(product.Asin, model.ProductColors);
                }

                if (model.ProductImages.Any())
                {
                    await _imageRepository.AddImagesAsync(product.Asin, model.ProductImages);
                }

                if (model.ProductPriceHistories.Any())
                {
                    foreach (var priceModel in model.ProductPriceHistories)
                    {
                        CreateProductPriceModel price = new()
                        {
                            Price = priceModel.Price,
                            Discount = priceModel.Discount,
                        };
                        await _priceHistoryRepository.AddPriceHistoryAsync(product.Asin, price);
                    }
                }
            }

            public async Task UpdateAsync(CreateProductRequestModel model)
            {
                // Fetch the existing product with related data
                var existingProduct = await _dbContext.Set<Product>()
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductPriceHistories.OrderByDescending(p => p.DateUpdated))
                    .FirstOrDefaultAsync(p => p.Asin == model.Asin);


                if (existingProduct == null)
                {
                    throw new KeyNotFoundException("Product not found.");
                }

                // Update the main product properties
                _mapper.Map(model, existingProduct);

                // Update related data

                // Update ProductColors
                existingProduct.ProductColors.Clear();
                await _colorRepository.AddColorsAsync(existingProduct.Asin, model.ProductColors);

                // Update ProductImages
                existingProduct.ProductImages.Clear();
                await _imageRepository.AddImagesAsync(existingProduct.Asin, model.ProductImages);

                // Update ProductPriceHistories
                // Optionally, archive old price histories instead of clearing them
                existingProduct.ProductPriceHistories.Clear();

                // Update ProductPriceHistories: keep only the most recent entry and add the new one if needed
                if (model.ProductPriceHistories is not null)
                {
                    await _priceHistoryRepository.AddPriceHistoryAsync(existingProduct.Asin, model.ProductPriceHistories);

                }

                // Update the main product
                _dbContext.Set<Product>().Update(existingProduct);
                await _dbContext.SaveChangesAsync();
            }

            public async IAsyncEnumerable<TModel> GetAllAsync<TModel>(
    int? take = null,
    int? skip = null,
    Expression<Func<TModel, bool>>? condition = null,
    bool readOnly = true) where TModel : class
            {
                // Step 1: Get the primary keys of the top 100 products (or based on `take`)
                var productKeysQuery = _dbContext.Set<Product>()
                    .AsQueryable();

                if (condition != null)
                {
                    productKeysQuery = productKeysQuery.Where(_mapper.Map<Expression<Func<Product, bool>>>(condition));
                }

                var productKeys = await productKeysQuery
                    .OrderBy(_ => Guid.NewGuid())
                    .Select(p => p.Asin) // Select only the primary key or unique identifier
                    .Take(take ?? 100) // Fetch top `take` or 100 products' keys
                    .ToListAsync();

                // Step 2: Retrieve the full details for the filtered products using the keys
                var fullProductsQuery = _dbContext.Set<Product>()
                    .Where(p => productKeys.Contains(p.Asin))
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductPriceHistories)
                    .Include(p => p.SubCategory);

                // Step 3: Apply skip if provided and stream results using async enumeration
                if (skip.HasValue)
                {
                    fullProductsQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Product, SubCategory>)fullProductsQuery.Skip(skip.Value);
                }

                await foreach (var product in fullProductsQuery.AsAsyncEnumerable())
                {
                    yield return _mapper.Map<TModel>(product);
                }
            }




            public async Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
            {
                var product = await _dbContext.Set<Product>()
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductPriceHistories)
                    .Include(p => p.SubCategory)
                    .FirstOrDefaultAsync(p => p.Asin == id);

                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found.");
                }

                return _mapper.Map<TModel>(product);
            }

        }
    }



}
