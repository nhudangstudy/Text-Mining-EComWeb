using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public record GetClaimModel
    {
        [Required]
        [EmailAddress]
        public string ClientId { get; init; } = null!;

        public IEnumerable<string>? Scopes { get; init; }
    }
}