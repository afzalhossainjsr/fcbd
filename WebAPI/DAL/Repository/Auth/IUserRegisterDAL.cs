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
        Task<AspNetSocialUserVerificationToken> GetSMSToken(SMSTokenModel obj);
        Task<AspNetUsersSocialUserReferenceViewModel> GetSocialReferenceStatus(AspNetUsersSocialUserReferenceSearchModel obj);
        Task<List<Dictionary<string, string>>> GetUserAddress(string? UserName);
        Task<VerifyTokenModel> GetVerifyToken(ResetPasswordModel model);
        Task<ResultObject> SaveForgotPasswordToken(ForgotPasswordToken obj);
        Task<ResultObject> SaveUserAddress(string? UserName, UserAddressModel obj);
        Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message);
        Task<ResultObject> UpdateUserAddress(string? UserName,UserAddressModel obj);
    }
}
