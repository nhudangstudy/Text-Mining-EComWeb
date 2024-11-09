using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public record CreateBrandModel
    {
        public string BrandName { get; set; } = null!;
    }

    public record UpdateBrandModel
    {
        public string BrandName { get; set; } = null!;
    }

    public record BrandResponseModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
        public bool IsArchived { get; set; }
    }

}
