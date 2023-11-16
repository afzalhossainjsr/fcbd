using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Store.Product;

namespace DAL.Repository.Store
{
    public interface IBasicDataDAL
    {
        Task<List<StoreInfoModel>> GetColorInfo();
        Task<List<CountryDataModel>> GetCountryInfo();
        Task<List<MeasurementNameModel>> GetMeasurement();
        Task<List<OrderStatusModel>> GetOrderStatus();
        Task<List<OrderTypeModel>> GetOrderType();
        Task<List<StoreTaxModel>> GetTax();
        Task<List<UsageAreaModel>> GetUsageAreaParameter();
        Task<List<WarrantyPeriodModel>> GetWarrantyPeriod();
        Task<List<WarrantyTypeModel>> GetWarrantyType();
    }
}
