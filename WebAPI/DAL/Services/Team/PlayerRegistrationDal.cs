using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Team;
using Model.Team;

namespace DAL.Services.Team
{
    public class PlayerRegistrationDal : IPlayerRegistrationDal
    {
        private readonly IDataManager _dataManager;
        public PlayerRegistrationDal(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"sp_get_player_registration"; 
        private readonly string setSp = @"sp_set_player_registration"; 

        #region Get data Date 05/10/2023
        private readonly int  LoadPlayerList = 1,
                              LoadPlayerDetails = 2,
                              LoadUserPlayerProfile = 3;    
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName , int?Id )   where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", Id));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);
            return new(lst);
        }
        public async Task<List<PlayerRegistrationViewModel>> GetPlayerList(string? UserName) 
        {
            var lst = await GetData<PlayerRegistrationViewModel>(LoadPlayerList, UserName, null);     
            return (lst);
        }
        public async Task<List<PlayerRegistrationModel>> GetPlayerDetails(string? UserName, int? Id) 
        {
            var lst = await GetData<PlayerRegistrationModel>(LoadPlayerDetails, UserName, Id); 
            return (lst);
        }
        public async Task<List<PlayerRegistrationModel>> GetUserPlayerProfile(string? UserName, int? Id)
        {
            var lst = await GetData<PlayerRegistrationModel>(LoadUserPlayerProfile, UserName, Id);
            return (lst);
        }
        #endregion 
        #region SetData Date 05/10/2023
        private readonly int Save = 1,
                             Update = 2,
                             InActive = 3; 
        private async Task<ResultObject> SetData(int? SaveOption, string? userName, PlayerRegistrationModel obj)
        {
               
           
                 

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@save_option", SaveOption));
            parameterList.Add(new SqlParameter("@user_name", userName));
            parameterList.Add(new SqlParameter("@first_name_bn", obj.first_name_bn));
            parameterList.Add(new SqlParameter("@last_name_bn", obj.last_name_bn));
            parameterList.Add(new SqlParameter("@first_name_en", obj.first_name_en));
            parameterList.Add(new SqlParameter("@last_name_en", obj.last_name_en));
            parameterList.Add(new SqlParameter("@nick_name_bn", obj.nick_name_bn));
            parameterList.Add(new SqlParameter("@nick_name_en", obj.nick_name_en));
            parameterList.Add(new SqlParameter("@date_of_birth", obj.date_of_birth));
            parameterList.Add(new SqlParameter("@mobile_number", obj.mobile_number));
            parameterList.Add(new SqlParameter("@email", obj.email));
            parameterList.Add(new SqlParameter("@district_id", obj.district_id));
            parameterList.Add(new SqlParameter("@thana_id", obj.thana_id));
            parameterList.Add(new SqlParameter("@player_position_id", obj.player_position_id));
            parameterList.Add(new SqlParameter("@jersey_number", obj.jersey_number));
            parameterList.Add(new SqlParameter("@profile_image", obj.profile_image));
            parameterList.Add(new SqlParameter("@gender_id", obj.gender_id));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SavePlayer(string? userName, PlayerRegistrationModel obj) 
        {
            var result = await SetData(Save, userName, obj);  
            return result;
        }
        public async Task<ResultObject> UpdatePlayer(string? userName, PlayerRegistrationModel obj)
        {
            var result = await SetData(Update, userName, obj); 
            return result;
        }
        public async Task<ResultObject> InActivePlayer(string? userName, PlayerRegistrationModel obj) 
        {
            var result = await SetData(InActive, userName, obj); 
            return result;
        }
        #endregion 
    }
}
