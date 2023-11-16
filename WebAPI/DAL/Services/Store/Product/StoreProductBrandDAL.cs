using System.Data;
using System.Data.SqlClient;

using DAL.Common;
using DAL.Repository.Store.Product;
using Model.Store.Product;

namespace DAL.Services.Store.Product
{
    public class StoreProductBrandDAL: IStoreProductBrandDAL
    {
        private readonly IDataManager _dataManager;
        public StoreProductBrandDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetProductBrand";
        private readonly string setSp = "StoreDB.dbo.spSetProductBrand";

        readonly int LoadProductBrand = 1;

        #region Load  Data Date: 27/02/2023 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName)
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<StoreProductBrandViewModel>> GetProductBrand(string? UserName)
        {
            var lst = await GetData<StoreProductBrandViewModel>(LoadProductBrand, UserName); 
            return (lst);
        }
        #endregion Load Data  

        #region Save  Date: 27/02/2023 
        private readonly int Save = 1;
        private readonly int Update = 2;
        private readonly int Delete = 3;
        private async Task<ResultObject> SetData(int? SaveOption, string? userName, StoreProductBrandModel obj)
        {

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@BrandName", obj.BrandName));
            parameterList.Add(new SqlParameter("@BrandImage", obj.BrandImage));

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveProductBrand(string? userName, StoreProductBrandModel obj) 
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateProductBrand(string? userName, StoreProductBrandModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> DeleteProductBrand(string? userName, int? Id)
        {
            StoreProductBrandModel obj = new StoreProductBrandModel() { Id = Id };
            var result = await SetData(Delete, userName, obj);
            return result;
        }
        #endregion 
    }
}
