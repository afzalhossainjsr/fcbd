using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.Product;

namespace DAL.Repository.Store.Product
{
    public interface IStoreProductCategoryDAL
    {
        Task<ResultObject> DeleteProductCategory(string? userName, int? Id);
        Task<List<ProductCategoryViewModel>> GetProductCategory(string? UserName);
        Task<ResultObject> SaveProductCategory(string? userName, StoreProductCategoryModel obj);
        Task<ResultObject> UpdateProductCategory(string? userName, StoreProductCategoryModel obj);
    }
}
