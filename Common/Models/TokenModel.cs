
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public record AccessTokenModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.?[A-Za-z0-9-_.+/=]*$")]
        public string Value { get; init; }

        [Required]
        public DateTime Expire { get; init; }
    }

    public record RefreshTokenModel
    {
        [Required]
        public Guid Value { get; init; }
        [Required]
        public DateTime Expire { get; init; }
    }

    public record GetByLoginTokenModel
    {
        [Required]
        public AccessTokenModel Access { get; init; }
        public RefreshTokenModel? Refresh { get; init; }
    }

    public record GetAccessByRefreshRequestTokenModel
    {
        [Required]
        public Guid RefreshToken { get; init; }
    }
}
