using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using DAL.Repository.Store.Menu;

using Model.Store.Menu;

namespace DAL.Services.Store.Menu
{
    public class MenuPermissionDAL : IMenuPermissionDAL 
    {
        private readonly IDataManager _dataManager;
        public MenuPermissionDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetMenuPermission";
        private readonly string setSp = "StoreDB.dbo.spSetMenuPermission"; 

        readonly int LoadPermissionMenu = 1, LoadUserWisePermissionedMenu = 2;

        #region Load  Data Date: 26/09/2023 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName, string AdminUserName)
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@AdminUserName", AdminUserName));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<MenuPermissionViewModel>> GetPermissionMenu(string? UserName, string AdminUserName)  
        {
            var lst = await GetData<MenuPermissionViewModel>(LoadPermissionMenu, UserName, AdminUserName);
            var featureMenu = lst.Where(x => x.ParentId == 0).ToList();
            List<MenuPermissionViewModel> TreeList = new List<MenuPermissionViewModel>();
            foreach (var feature in featureMenu)
            {
                feature.MenuList = GetMenu(lst, feature.MenuId).ToList();
                TreeList.Add(feature); 
            }
            return (TreeList);
        }

        private List<MenuPermissionViewModel> GetMenu(List<MenuPermissionViewModel> Listobj, int? ParentId)
        {
            var Lst = Listobj.Where(x => x.ParentId == ParentId)
                .Select(s => new MenuPermissionViewModel
                {
                    MenuId = s.MenuId,
                    MenuName = s.MenuName,
                    ParentId = s.ParentId,
                    MenuPermissionId = s.MenuPermissionId,
                    IsPermission = s.IsPermission,
                    IsInsert = s.IsInsert,
                    IsUpdate = s.IsUpdate,
                    IsDelete = s.IsDelete,
                    IconName = s.IconName,
                    ClassName = s.ClassName, 
                    SLNo = s.SLNo,
                    MenuURL = s.MenuURL,
                    MenuList = GetMenu(Listobj, s.MenuId)
                }).ToList();
            return Lst;
        }
        public async Task<List<MenuPermissionViewModel>> GetUserWiseMenu(string? userName)
        {
            {
                var lst = await GetData<MenuPermissionViewModel>(LoadUserWisePermissionedMenu, userName, null);
                var featureMenu = lst.Where(x => x.ParentId == 0).ToList();
                List<MenuPermissionViewModel> TreeList = new List<MenuPermissionViewModel>();
                foreach (var feature in featureMenu)
                {
                    feature.MenuList = GetMenu(lst, feature.MenuId).ToList();
                    TreeList.Add(feature);
                }
                return (TreeList);
            }
        }
        #endregion
            #region Set data Date:27/09/2023  
        private readonly int Save = 1;
       

        private async Task<ResultObject> SetData(int? SaveOption, string? userName, List<TypeMenuPermissionModel> obj)
        {
           
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@TypeMenuPermission", await _dataManager.ListToDataTable(obj)));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveMenuPermission(string? userName, List<TypeMenuPermissionModel> obj) 
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
       
        #endregion 
    }
}
