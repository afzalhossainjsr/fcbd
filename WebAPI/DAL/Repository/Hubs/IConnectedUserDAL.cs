using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using Model.Hubs;

namespace DAL.Repository.Hubs
{
    public interface IConnectedUserDAL 
    {

        Task<ResultObject> DeleteHubConnection(ConnectedUserModel obj);
        Task<List<ConnectedUserViewModel>> GetConnectedUserByUserName(string? UserName);
        Task<ResultObject> SaveHubConnection(ConnectedUserModel obj);

    }
}
