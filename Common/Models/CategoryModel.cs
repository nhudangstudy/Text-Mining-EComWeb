using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public record CreateCategoryModel
    {
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
    }

    public record UpdateCategoryModel
    {
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
    }

    public record CategoryResponseModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public bool IsArchived { get; set; }
    }

}
