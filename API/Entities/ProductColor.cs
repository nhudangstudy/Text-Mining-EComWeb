using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class ProductColor
{
    public int Id { get; set; }

    public string ColorHex { get; set; } = null!;

    public string Asin { get; set; } = null!;

    public bool IsArchived { get; set; }

    public virtual Product AsinNavigation { get; set; } = null!;
}
