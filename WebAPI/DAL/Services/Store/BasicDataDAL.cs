using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Store;
using Model.Store.Product;

namespace DAL.Services.Store
{
    public class BasicDataDAL : IBasicDataDAL 
    {
        private readonly IDataManager _dataManager;
        public BasicDataDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetBasicData";
        readonly int LoadCountryInfo = 1,
                    LoadMeasurement  = 2,
                    LoadTax  = 3,
                    LoadColorInfo  = 4,
                    LoadWarrantyType  = 5,
                    LoadWarrantyPeriod  = 6,
                    LoadUsageAreaParameter  = 7,
                    LoadOrderType  = 8,
                    LoadOrderStatus  = 9; 


        #region Load  Data Date: 04/03/2023 
        private async Task<List<T>> GetData<T>(int loadoption)
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<CountryDataModel>> GetCountryInfo()
        {
            var lst = await GetData<CountryDataModel>(LoadCountryInfo);  
            return (lst);
        }
        public async Task<List<MeasurementNameModel>> GetMeasurement() 
        {
            var lst = await GetData<MeasurementNameModel>(LoadMeasurement);
            return (lst);
        }
        public async Task<List<StoreTaxModel>> GetTax()
        {
            var lst = await GetData<StoreTaxModel>(LoadTax);
            return (lst);
        }
        public async Task<List<StoreInfoModel>> GetColorInfo()
        {
            var lst = await GetData<StoreInfoModel>(LoadColorInfo);
            return (lst);
        }
        public async Task<List<WarrantyTypeModel>> GetWarrantyType()
        {
            var lst = await GetData<WarrantyTypeModel>(LoadWarrantyType);
            return (lst);
        }
        public async Task<List<WarrantyPeriodModel>> GetWarrantyPeriod() 
        {
            var lst = await GetData<WarrantyPeriodModel>(LoadWarrantyPeriod);
            return (lst);
        }
        public async Task<List<UsageAreaModel>> GetUsageAreaParameter()
        {
            var lst = await GetData<UsageAreaModel>(LoadUsageAreaParameter); 
            return (lst);
        }
        public async Task<List<OrderTypeModel>> GetOrderType()
        {
            var lst = await GetData<OrderTypeModel>(LoadOrderType);
            return (lst);
        }
        public async Task<List<OrderStatusModel>> GetOrderStatus() 
        {
            var lst = await GetData<OrderStatusModel>(LoadOrderStatus);  
            return (lst);
        }
        #endregion 
    }
}
