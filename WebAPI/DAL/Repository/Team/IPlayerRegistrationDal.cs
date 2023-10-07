using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using Model.Team;

namespace DAL.Repository.Team
{
    public interface IPlayerRegistrationDal
    {
        Task<ResultObject> InActivePlayer(string? userName, PlayerRegistrationModel obj);
        Task<ResultObject> SavePlayer(string? userName, PlayerRegistrationModel obj);
        Task<ResultObject> UpdatePlayer(string? userName, PlayerRegistrationModel obj);
    }
}
