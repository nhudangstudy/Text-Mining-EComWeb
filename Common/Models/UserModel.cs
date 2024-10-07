using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public record CreateUpdateUserRequestModel
    {
        [Required]
        public required string FirstName { get; init; }
        [Required]
        public required string LastName { get; init; }
        [Required]
        public DateTime DateOfBirth { get; init; }
    }

    public record CreateUpdateUserRequestRepoModel
    {
        [Required]
        public required string FirstName { get; init; }
        [Required]
        public required string LastName { get; init; }
        [Required]
        public DateTime DateOfBirth { get; init; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }

    public record CreateUpdateUserResponeModel
    {
        [Required]
        public required string Message { get; set; }
    }

    public record GetByIdUserModel
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsArchived { get; set; }
        public int Id { get; set; }
    }

}
