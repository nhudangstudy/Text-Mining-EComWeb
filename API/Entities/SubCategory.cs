using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class SubCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public string? SubCategoryDescription { get; set; }

    public bool IsArchived { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
