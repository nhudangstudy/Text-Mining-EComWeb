using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Category
{
    public string? CategoryName { get; set; }

    public string? CategoryDescription { get; set; }

    public int Id { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
