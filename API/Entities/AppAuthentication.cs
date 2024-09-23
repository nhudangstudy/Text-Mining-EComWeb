using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class AppAuthentication
{
    public string Email { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime Expired { get; set; }

    public int Id { get; set; }

    public virtual AppAccount EmailNavigation { get; set; } = null!;
}
