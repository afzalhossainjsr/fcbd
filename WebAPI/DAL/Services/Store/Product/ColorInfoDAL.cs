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
    public class ColorInfoDAL: IColorInfoDAL 
    {
        private readonly IDataManager _dataManager;
        public ColorInfoDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string setSp = @"StoreDB.dbo.spSetColorInfo";
        #region Save  Date: 27/02/2023 
        private readonly int Save = 1;
        private readonly int Update = 2;
        private readonly int Delete = 3;
        private async Task<ResultObject> SetData(int? SaveOption, string? userName, StoreInfoModel obj)
        {

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@ColorName", obj.ColorName));
            parameterList.Add(new SqlParameter("@ColorCode", obj.ColorCode)); 

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveColorInfo(string? userName, StoreInfoModel obj)
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateColorInfo(string? userName, StoreInfoModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> DeleteColorInfo(string? userName, int? Id)
        {
            StoreInfoModel obj = new StoreInfoModel() { Id = Id };
            var result = await SetData(Delete, userName, obj);
            return result; 
        }
        #endregion 
    }
}
