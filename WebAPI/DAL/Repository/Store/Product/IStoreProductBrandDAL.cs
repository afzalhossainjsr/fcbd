using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.Product;

namespace DAL.Repository.Store.Product
{
    public interface IStoreProductBrandDAL
    {
        Task<ResultObject> DeleteProductBrand(string? userName, int? Id);
        Task<List<StoreProductBrandViewModel>> GetProductBrand(string? UserName);
        Task<ResultObject> SaveProductBrand(string? userName, StoreProductBrandModel obj);
        Task<ResultObject> UpdateProductBrand(string? userName, StoreProductBrandModel obj);
    }
}
