using System;
using System.Collections.Generic;
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
        public async Task<OTPMessageResultModel> SendOTPMessage(string? phonenumber, string message)
        {
            var result = await _dataManager.SendSingleMessage(phonenumber, message);
            return result;
        }
    }
}