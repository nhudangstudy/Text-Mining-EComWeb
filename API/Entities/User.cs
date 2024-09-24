using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class User
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public bool IsArchived { get; set; }

    public int Id { get; set; }

    public virtual AppAccount EmailNavigation { get; set; } = null!;
}
