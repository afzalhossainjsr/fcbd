using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.Product;

namespace DAL.Repository.Store.Product
{
    public interface IStoreProductSupplierDAL
    {
        Task<ResultObject> DeleteProductSupplier(string? userName, int? Id);
        Task<List<StoreProductSupplierViewModel>> GetProductSupplier(string? UserName, string? SearchText);
        Task<ResultObject> SaveProductSupplier(string? userName, StoreProductSupplierModel obj);
        Task<ResultObject> UpdateProductSupplier(string? userName, StoreProductSupplierModel obj);
    }
}
