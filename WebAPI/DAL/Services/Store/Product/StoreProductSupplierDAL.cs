using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using DAL.Repository.Store.Product;
using Model.Store.Product;

namespace DAL.Services.Store.Product
{
    public class StoreProductSupplierDAL: IStoreProductSupplierDAL 
    {
        private readonly IDataManager _dataManager;
        public StoreProductSupplierDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetProductSupplier";
        private readonly string setSp = "StoreDB.dbo.spSetProductSupplier";

        readonly int LoadProductSupplier = 1; 

        #region Load  Data Date: 05/03/2023 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName, string? SearchText)
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@SearchText", SearchText)); 
            
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<StoreProductSupplierViewModel>> GetProductSupplier(string? UserName, string? SearchText)
        {
            var lst = await GetData<StoreProductSupplierViewModel>(LoadProductSupplier, UserName, SearchText);
            return (lst);
        }
        #endregion Load Data  
        #region Save ProductSupplier Date:05/03/2023
        int Save = 1,
            Update = 2,
            Delete = 3;
        private async Task<ResultObject> SetData(int? SaveOption, string? userName, StoreProductSupplierModel obj)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            /*Supplier Details*/
            parameterList.Add(new SqlParameter("@SupplierName", obj.SupplierName));
            parameterList.Add(new SqlParameter("@SupplierDescription", obj.SupplierDescription));
            /*Supplier Contact info*/
            parameterList.Add(new SqlParameter("@FirstName", obj.FirstName));
            parameterList.Add(new SqlParameter("@LastName", obj.LastName));
            parameterList.Add(new SqlParameter("@PhoneCodeCountryId", obj.PhoneCodeCountryId));
            parameterList.Add(new SqlParameter("@PhoneNumber", obj.PhoneNumber));
            parameterList.Add(new SqlParameter("@TelephoneCodeCountryId", obj.TelephoneCodeCountryId));
            parameterList.Add(new SqlParameter("@TelephoneNumber", obj.TelephoneNumber));
            parameterList.Add(new SqlParameter("@Email", obj.Email));
            parameterList.Add(new SqlParameter("@Website", obj.Website));
            /*Supplier Physical Address*/
            parameterList.Add(new SqlParameter("@Street", obj.Street));
            parameterList.Add(new SqlParameter("@Suburb", obj.Suburb_Word_Union_Village));
            parameterList.Add(new SqlParameter("@City_Thana", obj.City_Thana));
            parameterList.Add(new SqlParameter("@State_District", obj.State_District));
            parameterList.Add(new SqlParameter("@Zip_PostalCode", obj.Zip_PostalCode));
            parameterList.Add(new SqlParameter("@CountryId", obj.CountryId));
            parameterList.Add(new SqlParameter("@SameAsPostalAddress", obj.SameAsPostalAddress));
            /*Supplier Postal Address*/
            parameterList.Add(new SqlParameter("@PStreet", obj.PostalStreet));
            parameterList.Add(new SqlParameter("@PSuburb", obj.PostalSuburb));
            parameterList.Add(new SqlParameter("@PCity_Thana", obj.PostalCity_Thana));
            parameterList.Add(new SqlParameter("@PState_District", obj.PostalState_District));
            parameterList.Add(new SqlParameter("@PZip_PostalCode", obj.PostalZip_PostalCode));
            parameterList.Add(new SqlParameter("@PCountryId", obj.PostalCountryId));


            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveProductSupplier(string? userName, StoreProductSupplierModel obj)
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateProductSupplier(string? userName, StoreProductSupplierModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> DeleteProductSupplier(string? userName, int? Id)
        {
            StoreProductSupplierModel obj = new StoreProductSupplierModel()
            {
                Id = Id,
                SupplierName = "Dummy"
            };
            var result = await SetData(Delete, userName, obj);
            return result;
        }

        #endregion
    }
}
