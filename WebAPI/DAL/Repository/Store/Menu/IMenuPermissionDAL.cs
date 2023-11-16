using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using Model.Store.Menu;

namespace DAL.Repository.Store.Menu
{
    public interface IMenuPermissionDAL
    {
        Task<List<MenuPermissionViewModel>> GetPermissionMenu(string? UserName, string AdminUserName);
        Task<List<MenuPermissionViewModel>> GetUserWiseMenu(string? userName);
        Task<ResultObject> SaveMenuPermission(string? userName, List<TypeMenuPermissionModel> obj);
    }
}
