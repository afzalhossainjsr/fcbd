using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Store.Reports
{
    public interface IStoreReportDAL
    {
        Task<List<Dictionary<string, string>>> GetCurrentMonthSalesSummary(string? UserName);
        Task<List<Dictionary<string, string>>> GetDayWiseProductDelivery(string? UserName, string? FromDate, string? ToDate, int? StoreId);
        Task<List<Dictionary<string, string>>> GetDayWiseProductOrder(string? UserName, string? FromDate, string? ToDate, int? StoreId);
        Task<List<Dictionary<string, string>>> GetMonthWiseProductSale(string? UserName);
        Task<List<Dictionary<string, string>>> GetMonthWiseSpecificProductSale(string? UserName);
        Task<DataTable> GetOrderQtyDataTable(string? UserName, string? FromDate, string? ToDate, int? StoreId);
        Task<List<Dictionary<string, string>>> GetProductStockByStore(string? UserName, int? StoreId);
        Task<List<Dictionary<string, string>>> GetProductWiseOrderQty(string? UserName, string? FromDate, string? ToDate, int? StoreId);
        
    }
}
