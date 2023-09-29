using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Hubs;
using Model.Hubs;

namespace DAL.Services.Hubs
{
    public class ConnectedUserDAL : IConnectedUserDAL
    {
        private readonly IDataManager _dataManager;
        public ConnectedUserDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private string SetSp = "ChatDB.DBO.spSetConnectedUser";
        private string GetSp = "ChatDB.DBO.spGetConnectedUser";

        #region GetData Date: 20/08/2023  
        private int HubConnectionIdByUserName = 1;
        private async Task<List<ConnectedUserViewModel>> GetData(int? LoadOption, string? UserName, string? HubConnectionId)
        {

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", LoadOption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@HubConnectionId", HubConnectionId));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnListBySP2<ConnectedUserViewModel>(GetSp, parameters);
            return new(list);
        }
        public async Task<List<ConnectedUserViewModel>> GetConnectedUserByUserName(string? UserName)
        {
            var result = await GetData(HubConnectionIdByUserName, UserName, null);
            return (result);
        }
        #endregion Load Data 
        #region  Date:28/09/2023
        int SaveConnection = 1,
            DeleteConnection = 2;
        private async Task<ResultObject> SetData(int? SaveOption, ConnectedUserModel obj)
        {

            ResultObject objResult = new ResultObject();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", obj.UserName));
            parameterList.Add(new SqlParameter("@HubConnectionId", obj.HubConnectionId));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            objResult = await _dataManager.SaveDataBySP(SetSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveHubConnection(ConnectedUserModel obj)
        {
            var result = await SetData(SaveConnection, obj);
            return result;
        }
        public async Task<ResultObject> DeleteHubConnection(ConnectedUserModel obj)
        {
            var result = await SetData(DeleteConnection, obj);
            return result;
        }
        #endregion 
    }
}