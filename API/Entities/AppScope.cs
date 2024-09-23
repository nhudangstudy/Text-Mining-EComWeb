using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class AppScope
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<AppAccount> Accounts { get; set; } = new List<AppAccount>();
}
