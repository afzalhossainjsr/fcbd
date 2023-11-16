using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.ProductOrder;

namespace DAL.Repository.Store.ProductOrder
{
    public interface IOrderProcessDAL
    {
        Task<ResultObject> CancelSingleProduct(string? userName, OrderProcessStatusChangeModel obj);
        Task<ResultObject> ChangeOrderStatus(string? userName, OrderProcessStatusChangeModel obj);
        Task<List<OrderReturnViewModel>> GetOrderByOrderNumber(string? userName, string? OrderNumber);
        Task<OrderHeadDataModel> GetProductOrder(string? UserName, string? StartDate, string? EndDate, string? OrderNo, string? SearchText, int? OrderStatusId, int? OrderTypeId, int page = 1, int Pagingsize = 100);
        Task<List<OrderDetailsViewModel>> GetProductOrderDetails(string? UserName, int? OrderHeadId);
        Task<ResultObject> RestoreProductFromCancelOrderItem(string? userName, OrderProcessStatusChangeModel obj);
        Task<ResultObject> SaveCancelledOrder(string? userName, int? Id);
        Task<ResultObject> SaveConfirmedOrder(string? userName, OrderProcessConfirmationModel obj);
        Task<ResultObject> SaveOrderReturn(string? userName, ProductReturnModel obj);
    }
}
