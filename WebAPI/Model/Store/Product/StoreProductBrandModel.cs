using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.Product
{
    public class StoreProductBrandModel
    {
        public int?Id { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImage { get; set; }
        public string? BrandImageBase64String { get; set; }  

    }
    public class StoreProductBrandViewModel : Column1To7 
    {
        public int? Id { get; set; }
        public string? BrandName { get; set; }
        public string? BrandImage { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; } 
        public string? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeletedAt { get; set; }

    }
}
