using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Review
{
    public Guid Id { get; set; }

    public string Asin { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string OwnedBy { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public double StarRating { get; set; }

    public string ReviewContent { get; set; } = null!;

    public virtual Product AsinNavigation { get; set; } = null!;

    public virtual AppAccount OwnedByNavigation { get; set; } = null!;
}
