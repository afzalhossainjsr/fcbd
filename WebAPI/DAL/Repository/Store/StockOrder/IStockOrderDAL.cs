using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.StockOrder;

namespace DAL.Repository.Store.StockOrder
{
    public interface IStockOrderDAL
    {
        Task<ResultObject> AddSingleStock(string? userName, ProductStockModel obj);
        Task<ResultObject> AddSingleStockBalance(string? userName, ProductStockModel obj);
        Task<ResultObject> AllowReAddProduct(string? userName, StockOrderAppovalModel obj);
        Task<ResultObject> ApprovedStockOrder(string? userName, StockOrderAppovalModel obj);
        Task<ResultObject> CancelledStockOrder(string? userName, StockOrderAppovalModel obj);
        Task<ResultObject> DeleteAllRequestProductStock(string? userName, RequestProductStockModel obj);
        Task<ResultObject> DeleteProductSupplyOrder(string? userName, int Id);
        Task<ResultObject> DeleteRequestProductStock(string? userName, RequestProductStockModel obj);
        Task<List<StockProductModel>> GetAllProductForStock(string? UserName, int? ProductCategoryId, int? ProductBrandId, int? OriginCountryId, int? StoreId, int? StockQtyLessThen, int? StockQtygretherThen);
        Task<List<ProductStockOrderViewModel>> GetProductStockOrder(int StatusId, string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId);
        //Task<List<ProductStockOrderViewModel>> GetApprovedStockOrder(string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId);
        //Task<List<ProductStockOrderViewModel>> GetCancelledStockOrder(string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId);     
        //Task<List<ProductStockOrderViewModel>> GetReceivedStockOrder(string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId);

        Task<List<ProductCategoryModel>> GetCurrentProductCategory(string? UserName);
        Task<List<StockLevelModel>> GetCurrentStock(string? UserName, int? ProductCategoryId, int? StockLevel, int? StoreId);
        Task<List<ProductStockOrderViewModel>> GetNewOrderForApproval(string? UserName);
        Task<List<ProductStockViewModel>> GetProductList(string? UserName, List<TypeOrderHeadIdList> HeadIdList);

        Task<List<ProductSupplyDetailsViewModel>> GetProductSupplyDetails(string? UserName, int? OrderHeadId);
        Task<List<ProductSupplyOrderViewModel>> GetProductSupplyOrder(string? UserName, int? OrderNo, string? FromDate, string? ToDate, int? OrderStatusId, int? StoreId, int? SupplierId);
        Task<List<OrderStatusViewModel>> GetProductSupplyOrderStatus(string? UserName);

        Task<List<StockProductModel>> GetRequestedStockQty(string? UserName, int? StoreId);
        Task<ResultObject> ReceiveProductSupplyOrder(string? userName, ProductSupplyOrderModel obj);
        Task<ResultObject> SaveProductSupplyOrder(string? userName, ProductSupplyOrderModel obj);
        Task<ResultObject> SetRequestProductStock(string? userName, RequestProductStockModel obj);
        Task<ResultObject> SetStockLevel(string? userName, QuickStockLevelModel obj);
        Task<ResultObject> UpdateProductSupplyOrder(string? userName, ProductSupplyOrderModel obj);
        Task<ResultObject> SaveDamageProduct(string? userName, ProductDamageOrExpireHistoryModel obj);
        Task<ResultObject> UpdateDamageProduct(string? userName, ProductDamageOrExpireHistoryModel obj);
        Task<ResultObject> DeleteDamageProduct(string? userName, ProductDamageOrExpireHistoryModel obj);
        Task<List<Dictionary<string, string>>> GetDamagedProduct(string? UserName, int? StoreId, int? ProductCategoryId, string? FromDate, string? Todate, int? DamegeTypeId);
        Task<List<Dictionary<string, string>>> GetExpenseReason(string? UserName);
        Task<List<ExpenseHistoryViewModel>> GetExpenseHistoryHead(string? UserName, int? StoreId, int? Id, string? FromDate, string? ToDate, int? OrderNo);
        Task<List<ExpenseHistoryDetailsViewModel>> GetExpenseHistoryDetail(string? UserName, int? Id);
    }
}
