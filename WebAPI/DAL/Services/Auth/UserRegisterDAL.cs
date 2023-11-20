using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Repository.Auth;
using Model.Auth;

namespace DAL.Services.Auth
{
    public class UserRegisterDAL : IUserRegisterDAL
    {

        private readonly IDataManager _dataManager;
        public UserRegisterDAL(IDataManager dataManager)
        {

            _dataManager = dataManager;
        }
        public async Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message)
        {
            var result = await _dataManager.SendSingleMessage(phonenumber, message);
            return result;
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
            var list = await _dataManager.ReturnListBySP2<AspNetUsersSocialUserReferenceViewModel>("UserDB.dbo.spGetAspNetUsersSocialUserReference", parameters);
            if (list.Count > 0)
            {
                Info = list[0];
            }
            return (Info);
        }
        public async Task<ResultObject> SaveForgotPasswordToken(ForgotPasswordToken obj)
        {
            ResultObject objResult = new ResultObject();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@PhoneNumber", obj.PhoneNumber));
            parameterList.Add(new SqlParameter("@PasswordToken", obj.PasswordToken));
            parameterList.Add(new SqlParameter("@SMSToken", obj.SMSToken));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();

            objResult = await _dataManager.SaveDataBySP(@"UserDB.dbo.spSetAspNetUserResetPasswordToken", parameters);
            return (objResult);
        }
        public async Task<ForgotPasswordToken> GetForgotPasswordToken(ResetPasswordModel model)
        {
            ForgotPasswordToken obj = new ForgotPasswordToken();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", 2));
            parameterList.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
            parameterList.Add(new SqlParameter("@Token", model.Token));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnListBySP<ForgotPasswordToken>("UserDB.dbo.spGetAspNetUserResetPasswordToken", parameters);
            if (list.Count > 0)
            {
                obj = list[0];
               
            }
             return obj; 
        }
        public async Task<VerifyTokenModel> GetVerifyToken(ResetPasswordModel model)  
        {
            VerifyTokenModel obj = new VerifyTokenModel();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", 3));
            parameterList.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
            parameterList.Add(new SqlParameter("@Token", model.Token));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnListBySP<VerifyTokenModel>("UserDB.dbo.spGetAspNetUserResetPasswordToken", parameters);
            if (list.Count > 0)
            {
                obj = list[0];
            }
            return obj;
        }
        public async Task<AspNetSocialUserVerificationToken> GetSMSToken(SMSTokenModel obj)
        {

            AspNetSocialUserVerificationToken Info = new AspNetSocialUserVerificationToken();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@MobileNumber", obj.MobileNumber));
            parameterList.Add(new SqlParameter("@SMSToken", obj.SMSToken));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnListBySP2<AspNetSocialUserVerificationToken>("UserDB.dbo.spSetAspNetUsersSocialUserMobileVerificationToken", parameters); 
            if (list.Count > 0)
            {
                Info = list[0];
            }
            return (Info);
        }

        private async Task<ResultObject> SetUserAddress(int? SaveOption, string? UserName, UserAddressModel obj) 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@DivisionId", obj.DivisionId));
            parameterList.Add(new SqlParameter("@DistrictId", obj.DistrictId));
            parameterList.Add(new SqlParameter("@ThanaId", obj.ThanaId));
            parameterList.Add(new SqlParameter("@Address", obj.Address));
            parameterList.Add(new SqlParameter("@Direction", obj.Direction));
            parameterList.Add(new SqlParameter("@Lat", obj.Lat));
            parameterList.Add(new SqlParameter("@Lang", obj.Lang));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();

            var  objResult = await _dataManager.SaveDataBySP(@"UserDB.dbo.spSetUserAddress", parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveUserAddress(string? UserName, UserAddressModel obj)
        {
            var result = await SetUserAddress(1, UserName, obj); 
            return (result); 
        }
        public async Task<ResultObject> UpdateUserAddress(string? UserName, UserAddressModel obj)
        {
            var result = await SetUserAddress(2, UserName, obj); 
            return (result);
        }
    }
}