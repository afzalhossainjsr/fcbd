using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Store.Product;
using Model.Store.ProductOrder;

namespace DAL.Repository.Store.ProductOrder
{
    public interface IWebProductViewDAL
    {
        Task<List<WebProductViewModel>> GetOfferProduct();
        Task<StoreProductWebModel> GetProduct(string? UserName,
            int? ProductCategoryId, int? ProductId, string? SearchText, int? ProductBrandId,
            string? ProductColor, string? ProductSize, int? PriceStart, int? PriceEnd, int? WarrantyTypeParameterId,
            int? WarrantyPeriod, int? PriceDisplayOrder,int? OriginCountryId, int? page, int PagingSize);
        Task<List<ProductCategoryViewModel>> GetProductCategory(string? UserName);
    }
}
