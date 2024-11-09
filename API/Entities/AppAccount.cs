using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class AppAccount
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<AppAuthentication> AppAuthentications { get; set; } = new List<AppAuthentication>();

    public virtual ICollection<AppRefreshToken> AppRefreshTokens { get; set; } = new List<AppRefreshToken>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<AppScope> Scopes { get; set; } = new List<AppScope>();
}
