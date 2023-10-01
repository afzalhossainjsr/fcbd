using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;

namespace DAL.Repository.Auth
{
    public interface IUserRegisterDAL
    {
        Task<AspNetUsersSocialUserReferenceViewModel> GetSocialReferenceStatus(AspNetUsersSocialUserReferenceSearchModel obj);
        Task<UserRegistrationInfoModel> GetUserRegistrationInfo(string? UserName);
        Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message);
    }
}
