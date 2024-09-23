using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public record CreateRequestRepoRefreshTokenModel
    {
        [Required]
        public Guid RefreshTokenId { get; init; }
        [Required]
        public string AccountId { get; init; } = null!;
        [Required]
        public string Ipaddress { get; init; } = null!;
        [Required]
        public DateTime ExpiredTime { get; init; }
    }

    public record GetByIdRefreshTokenModel
    {
        [Required]
        public string AccountId { get; init; } = null!;
        [Required]
        public DateTime ExpiredTime { get; init; }
    }
}
