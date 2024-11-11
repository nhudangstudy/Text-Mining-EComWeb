using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{

    public record CreateProductRequestModel
    {
        public required string Asin { get; set; }
        public int SubCategoryId { get; set; }
        public int? BrandId { get; set; }
        public string OwnedBy { get; set; } = null!;
        public string? ProductShortDescription { get; set; }
        public string? ProductDetailDescription { get; set; }
        public string? ProductName { get; set; }

        // Nested data for related entities
        public List<CreateProductColorModel> ProductColors { get; set; } = new();
        public List<CreateProductImageModel> ProductImages { get; set; } = new();
        public CreateProductPriceModel ProductPriceHistories { get; set; } = new();
    }

    public record CreateProductModel
    {
        public required string Asin {  get; set; }
        public int SubCategoryId { get; set; }
        public int? BrandId { get; set; }
        public string OwnedBy { get; set; } = null!;
        public string? ProductShortDescription { get; set; }
        public string? ProductDetailDescription { get; set; }
        public string? ProductName { get; set; }

        // Nested data for related entities
        public List<CreateProductColorModel> ProductColors { get; set; } = new();
        public List<CreateProductImageModel> ProductImages { get; set; } = new();
        public List<CreateProductPriceHistoryModel> ProductPriceHistories { get; set; } = new();
    }

    public record CreateProductColorModel
    {
        public string ColorHex { get; set; } = null!;
    }

    public record CreateProductImageModel
    {
        public string ImageUrl { get; set; } = null!;
    }

    public record CreateProductPriceHistoryModel
    {
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public DateOnly? DateUpdated { get; set; }
    }

    public record ProductResponseModel
    {
        public string Asin { get; set; } = null!;
        public int SubCategoryId { get; set; }
        public int? BrandId { get; set; }
        public string OwnedBy { get; set; } = null!;
        public string? ProductShortDescription { get; set; }
        public string? ProductDetailDescription { get; set; }
        public string? ProductName { get; set; }
        public bool IsArchived { get; set; }

        public List<ProductColorResponseModel> ProductColors { get; set; } = new();
        public List<ProductImageResponseModel> ProductImages { get; set; } = new();
        public List<ProductPriceHistoryResponseModel> ProductPriceHistories { get; set; } = new();
    }

    public record ProductColorResponseModel
    {
        public int Id { get; set; }
        public string ColorHex { get; set; } = null!;
    }

    public record ProductImageResponseModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsArchived { get; set; }
    }

    public record ProductPriceHistoryResponseModel
    {
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public DateOnly? DateUpdated { get; set; }
    }

    public record CreateProductPriceModel
    {
        public double? Price { get; set; }
        public double? Discount { get; set; }

    }

    public record CreateProductPriceRequestModel
    {
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public DateOnly? DateUpdated { get; set; }


    }

}
