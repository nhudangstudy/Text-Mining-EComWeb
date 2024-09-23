using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public record LoginRequestAccountModel
    {
        [Required]
        [EmailAddress]
        public string ClientId { get; init; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; } = null!;
    }

    public record GetActiveByIdAccountModel
    {
        public bool? IsActive { get; init; }
    }

    public record LoginAccountRepoModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; } = null!;

        public IEnumerable<int>? ScopeIds { get; init; }
    }

    public record GetByIdPublicAccountModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; } = null!;

        [Required]
        public string FullName { get; init; } = null!;

        public bool? IsActive { get; set; }
    }

    public record RegisterRequestRepoAccountModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public int RoleId { get; init; }
        public bool? IsActive { get; set; }
    }

    public record RegisterRequestAccountModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public string RepeatPassword { get; set; } = null!;
    }

    public record UpdateRequestRepoAccountModel
    {
        [Required]
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public record GetLastNameByIdAccountModel
    {
        [Required]
        public string LastName { get; init; } = null!;
    }

    public record GetFullNameByIdAccountModel
    {
        [Required]
        public string FirstName { get; init; } = null!;

        [Required]
        public string LastName { get; init; } = null!;
    }

    public record UpdateActiveRequestRepoAccountModel
    {
        public bool? IsActive { get; init; }
    }

    public record AddScopeRequestAccountModel
    {
        public int ScopeId { get; init; }
    }

    public record AddScopeRequestRepoAccountModel
    {
        public string Email { get; init; }
        public int ScopeId { get; init; }
    }

    public record RemoveScopeRequestRepoAccountModel
    {
        public string Email { get; init; }
        public int ScopeId { get; init; }
    }

    public record ResetPasswordRequestAccountModel
    {
        [Required]
        public CheckRequestAuthenticationModel Auth { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public string NewPassword { get; init; }
    }

    public record ResetPasswordRequestRepoAccountModel
    {
        public string Password { get; init; }
    }
}