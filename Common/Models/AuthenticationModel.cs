using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public record SendRequestRepoAuthenticationModel
    {
        [Required]
        public string Email { get; init; }
        [Required]
        public string Code { get; init; }
        [Required]
        public DateTime Expire { get; init; }
    }

    public record SendRequestAuthenticationModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }
    }

    public record CheckRequestAuthenticationModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [Required]
        [RegularExpression("[0-9]{6}")]
        public string Code { get; init; }
    }    
}
