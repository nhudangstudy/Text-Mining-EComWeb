using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Brand
{
    public int Id { get; set; }

    public string BrandName { get; set; } = null!;

    public bool IsArchived { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
