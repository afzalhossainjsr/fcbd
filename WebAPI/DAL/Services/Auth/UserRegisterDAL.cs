using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Auth;

namespace DAL.Services.Auth
{
    public class UserRegisterDAL : IUserRegisterDAL
    {

        private readonly IDataManager _dataManager;
        public UserRegisterDAL(IDataManager dataManager)
        {

            _dataManager = dataManager;
        }
        private string getSP = @"spGetUserProfileSettings";
        private string SetSp = @"spSetUserProfileSettings";

        public int LoadAddressType = 1,
                   LoadGender = 2,
                   LoadCountry = 3,
                   LoadUserRegistrationInfo = 4,
                   LoadUserAddress = 5,
                   LoadUserPaymentCardInfo = 6,
                   LoadDeleteReasonParameter = 7;
        public async Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message)
        {
            var result = await _dataManager.SendSingleMessage(phonenumber, message);
            return result;
        }
        public async Task<UserRegistrationInfoModel> GetUserRegistrationInfo(string? UserName) 
        {
            UserRegistrationInfoModel Info = new UserRegistrationInfoModel();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", LoadUserRegistrationInfo));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnListBySP2<UserRegistrationInfoModel>(getSP, parameters);
            if (list.Count > 0)
            {
                Info = list[0];
            }
            return (Info);
        }
        public async Task<AspNetUsersSocialUserReferenceViewModel> GetSocialReferenceStatus(AspNetUsersSocialUserReferenceSearchModel obj)
        {

            AspNetUsersSocialUserReferenceViewModel Info = new AspNetUsersSocialUserReferenceViewModel();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SocialUserId", obj.SocialUserId));
            parameterList.Add(new SqlParameter("@Provider", obj.Provider));
            parameterList.Add(new SqlParameter("@Email", obj.Email));
            parameterList.Add(new SqlParameter("@MobileNumber", obj.MobileNumber));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnListBySP2<AspNetUsersSocialUserReferenceViewModel>("EasyesseUserDB.dbo.spGetAspNetUsersSocialUserReference", parameters);
            if (list.Count > 0)
            {
                Info = list[0];
            }
            return (Info);
        }
    }
}