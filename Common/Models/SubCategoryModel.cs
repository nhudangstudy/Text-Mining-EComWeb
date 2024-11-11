using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public record CreateSubCategoryModel
    {
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public string? SubCategoryDescription { get; set; }
    }

    public record UpdateSubCategoryModel
    {
        public string SubCategoryName { get; set; } = null!;
        public string? SubCategoryDescription { get; set; }
    }

    public record SubCategoryResponseModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public string? SubCategoryDescription { get; set; }
        public bool IsArchived { get; set; }
    }

}
