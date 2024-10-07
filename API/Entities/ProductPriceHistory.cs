using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class ProductPriceHistory
{
    public int Id { get; set; }

    public string Asin { get; set; } = null!;

    public double? Price { get; set; }

    public double? Discount { get; set; }

    public DateOnly? DateUpdated { get; set; }

    public virtual Product AsinNavigation { get; set; } = null!;
}
