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
        Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message);
    }
}
