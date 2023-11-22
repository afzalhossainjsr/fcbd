using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Basic;

namespace DAL.Services.Basic
{
    public class LocationDAL: ILocationDAL
    {
        private readonly IDataManager _dataManager;
        public LocationDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"UserDB.dbo.spGetLocations";
        readonly int LoadDivisionToThana = 1;



        #region Load  Data Date: 22/11/2023 
        private async Task<List<Dictionary<string, string>>> GetData(int? LoadOption, int? Id) 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", LoadOption));
            parameterList.Add(new SqlParameter("@Id", Id));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnDictionaryListBySP(getSp, parameters); 
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetDivisionToThana() 
        {
            var list = await GetData(LoadDivisionToThana, null);
            return new(list);
        }
        #endregion
    }
}
