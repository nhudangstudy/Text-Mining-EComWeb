using AutoMapper;
using Common.Models;
using API.Entities;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Mapping from Product to CreateProductModel
        CreateMap<Product, CreateProductModel>()
            .ForMember(dest => dest.ProductPriceHistories, opt => opt.MapFrom(src => src.ProductPriceHistories.ToList()))
            .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors.ToList()))
            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.ToList()));

        // Mapping from Product to CreateProductRequestModel
        CreateMap<Product, CreateProductRequestModel>()
            .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors.ToList()))
            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.ToList()))
            .ForMember(dest => dest.ProductPriceHistories, opt => opt.MapFrom(src => src.ProductPriceHistories.FirstOrDefault()));

        // Mapping from Product to ProductResponseModel
        CreateMap<Product, ProductResponseModel>()
            .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ProductColors.Select(c => new ProductColorResponseModel
            {
                Id = c.Id,
                ColorHex = c.ColorHex
            }).ToList()))
            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(i => new ProductImageResponseModel
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                IsArchived = i.IsArchived
            }).ToList()))
            .ForMember(dest => dest.ProductPriceHistories, opt => opt.MapFrom(src => src.ProductPriceHistories.Select(p => new ProductPriceHistoryResponseModel
            {
                Id = p.Id,
                Price = p.Price,
                Discount = p.Discount,
                DateUpdated = p.DateUpdated
            }).ToList()));
    }
}
