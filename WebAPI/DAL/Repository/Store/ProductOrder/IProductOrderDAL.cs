using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.ProductOrder;

namespace DAL.Repository.Store.ProductOrder
{
    public interface IProductOrderDAL
    {
        Task<ResultObject> DeleteProductOrder(string? userName, int? Id);
        Task<List<CustomerDefaultAddressModel>> GetDefaultAddress(string? UserName);
        Task<List<OrderDetailsViewModel>> GetOrderDetails(string? UserName, int? Id);
        Task<List<OrderHeadViewModel>> GetOrderList(string? UserName);
        Task<ResultObject> SaveProductOrder(string? userName, OrderHeadModel obj);
        Task<ResultObject> UpdateProductOrder(string? userName, OrderHeadModel obj);
    }
}
