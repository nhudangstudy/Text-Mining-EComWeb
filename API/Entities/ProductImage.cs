using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class ProductImage
{
    public int Id { get; set; }

    public string Asin { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool IsArchived { get; set; }

    public virtual Product AsinNavigation { get; set; } = null!;
}
