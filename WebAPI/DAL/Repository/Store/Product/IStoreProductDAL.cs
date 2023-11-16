using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.Product;

namespace DAL.Repository.Store.Product
{
    public interface IStoreProductDAL
    {
        Task<ResultObject> ActiveProduct(string? userName, ProductInactiveModel obj);
        Task<ResultObject> DeleteProduct(string? userName, int? Id);
        Task<List<StoreProductViewModel>> GetLowStock(string? UserName, int? ProductCategoryId, string? SearchText);
        Task<List<StoreProductViewModel>> GetOutofStock(string? UserName, int? ProductCategoryId, string? SearchText);
        Task<List<StoreProductViewModel>> GetProduct(string? UserName, int? Id, string? SearchText, int? ProductCategoryId, int? ProductBrandId, int? ProductSupplierId);
        Task<StoreProductInitialViewModel> GetProductInitialData(string? UserName, int? Id);
        Task<ResultObject> InActiveProduct(string? userName, ProductInactiveModel obj);
        Task<ResultObject> SaveProduct(string? userName, StoreProductModel obj);
        Task<ResultObject> SaveProductOffer(string? userName, PrductOfferModel obj);
        Task<ResultObject> UpdateProduct(string? userName, StoreProductModel obj);
    }
}
