using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class AppRefreshToken
{
    public string AccountId { get; set; } = null!;

    public Guid RefreshTokenId { get; set; }

    public DateTime? ExpiredTime { get; set; }

    public virtual AppAccount Account { get; set; } = null!;
}
