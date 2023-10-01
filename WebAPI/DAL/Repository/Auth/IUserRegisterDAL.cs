using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using Model.Auth;

namespace DAL.Repository.Auth
{
    public interface IUserRegisterDAL
    {
        Task<ForgotPasswordToken> GetForgotPasswordToken(ResetPasswordModel model);
        Task<AspNetUsersSocialUserReferenceViewModel> GetSocialReferenceStatus(AspNetUsersSocialUserReferenceSearchModel obj);
        Task<UserRegistrationInfoModel> GetUserRegistrationInfo(string? UserName);
        Task<ResultObject> SaveForgotPasswordToken(ForgotPasswordToken obj);
        Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message);
    }
}
