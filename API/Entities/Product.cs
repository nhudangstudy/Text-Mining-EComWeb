using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Product
{
    public string Asin { get; set; } = null!;

    public int SubCategoryId { get; set; }

    public int? BrandId { get; set; }

    public string OwnedBy { get; set; } = null!;

    public string? ProductShortDescription { get; set; }

    public string? ProductDetailDescription { get; set; }

    public string? ProductName { get; set; }

    public bool IsArchived { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual AppAccount OwnedByNavigation { get; set; } = null!;

    public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; set; } = new List<ProductPriceHistory>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual SubCategory SubCategory { get; set; } = null!;
}
