using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Basic;

namespace Model.Store.Product
{
    public class StoreProductCategoryModel
    {
        public int? Id { get; set; }
        public string? CategoryName { get; set; }
        public int? ParentId { get; set; }
        public string? CategoryImage { get; set; }
        public string? CategoryImageBase64String { get; set; } 
    }
    public class ProductCategoryViewModel 
    {
        public int? Id { get; set; }
        public string? CategoryName { get; set; }
        public int? ParentId { get; set; }
        public string? CategoryParentTree { get; set; }
        public string? CategoryImage { get; set; }
        public bool? IsAddedProduct { get; set; }
        public int? TotalProduct { get; set; }
        public int? StageNumber { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedAt { get; set; }
        public List<ProductCategoryViewModel>? ProductCategoryList { get; set; }
        public string? CategoryParentTreeBn { get; set; } 
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
        public string? Column4 { get; set; }
        public string? Column5 { get; set; }
        public string? Column6 { get; set; }
        public string? Column7 { get; set; }
    }
}
