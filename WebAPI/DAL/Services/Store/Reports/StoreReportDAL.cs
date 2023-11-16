using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Store.Reports;
using Model.Store.Product;

namespace DAL.Services.Store.Reports
{
    public class StoreReportDAL : IStoreReportDAL
    {
        private readonly IDataManager _dataManager;
        public StoreReportDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetProductSalesReport";
        readonly int LoadDayWiseProductOrder  = 1,
	                 LoadDayWiseProductDelivery  = 2,
                     LoadProductWiseOrderQty  = 3, 
                     LoadMonthWiseProductSale  = 4,
			         LoadMonthWiseSpecificProductSale = 5,
                      LoadCurrentMonthSalesSummary = 6;   


        #region Load  Data Date: 04/09/2023 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName,string?FromDate, string?ToDate, int?StoreId ) 
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@FromDate", FromDate));
            parameterList.Add(new SqlParameter("@ToDate", ToDate));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        //public async Task<List<CountryDataModel>> GetCountryInfo()
        //{
        //    var lst = await GetData<CountryDataModel>(LoadDayWiseProductOrder);
        //    return (lst);
        //}

        private async Task<List<Dictionary<string, string>>> GetDynamicReport(int? LoadOption ,string? UserName, string? FromDate, string? ToDate, int? StoreId)
        { 
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", LoadOption)); 
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@FromDate", FromDate));
            parameterList.Add(new SqlParameter("@ToDate", ToDate));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnDictionaryListBySP(getSp, parameters);
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetDayWiseProductOrder( string? UserName, string? FromDate, string? ToDate, int? StoreId)
        {
            var list = await GetDynamicReport(LoadDayWiseProductOrder, UserName, FromDate, ToDate, StoreId); 
            return new(list); 
        }
        public async Task<List<Dictionary<string, string>>> GetDayWiseProductDelivery(string? UserName, string? FromDate, string? ToDate, int? StoreId)
        {
            var list = await GetDynamicReport(LoadDayWiseProductDelivery, UserName, FromDate, ToDate, StoreId); 
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetProductWiseOrderQty(string? UserName, string? FromDate, string? ToDate, int? StoreId)
        {
            var list = await GetDynamicReport(LoadProductWiseOrderQty, UserName, FromDate, ToDate, StoreId); 
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetMonthWiseProductSale(string? UserName)
        {
            var list = await GetDynamicReport(LoadMonthWiseProductSale, UserName, null, null, null);
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetMonthWiseSpecificProductSale(string? UserName) 
        {
            var list = await GetDynamicReport(LoadMonthWiseSpecificProductSale, UserName, null, null, null);
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetCurrentMonthSalesSummary(string? UserName) 
        {
            var list = await GetDynamicReport(LoadCurrentMonthSalesSummary, UserName, null, null, null);
            return new(list);
        }
        //ReturnDataTableBySP
        private async Task<DataTable> GetDataTable (int? LoadOption, string? UserName, string? FromDate, string? ToDate, int? StoreId) 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>(); 
            parameterList.Add(new SqlParameter("@LoadOption", LoadOption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@FromDate", FromDate));
            parameterList.Add(new SqlParameter("@ToDate", ToDate));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnDataTableBySP(getSp, parameters);
            return list;   
        }
        public async Task<DataTable> GetOrderQtyDataTable(string? UserName, string? FromDate, string? ToDate, int? StoreId) 
        {
            var list = await GetDataTable(LoadProductWiseOrderQty, UserName, FromDate, ToDate, StoreId); 
            return list; 
        }

        private async Task<List<Dictionary<string, string>>> GetStockReport(int? LoadOption, string? UserName, int? StoreId)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", LoadOption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnDictionaryListBySP("StoreDB.dbo.spGetStockReport", parameters);
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetProductStockByStore(string? UserName, int? StoreId) 
        {
            var list = await GetStockReport(1, UserName, StoreId);   
            return new(list);
        }

     

        #endregion
    }
}
