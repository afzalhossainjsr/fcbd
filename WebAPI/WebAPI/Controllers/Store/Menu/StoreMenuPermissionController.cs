using DAL.Repository.Store.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.Menu;

namespace WebAPI.Controllers.Store.Menu
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreMenuPermissionController : ControllerBase
    {
        private readonly IMenuPermissionDAL _iMenuPermissionDAL;
        public StoreMenuPermissionController(IMenuPermissionDAL iUserRegisterDAL)
        {
            this._iMenuPermissionDAL = iUserRegisterDAL;
        }
        [HttpGet]
        [Route("GetPermissionMenu")] 
        public async Task<IActionResult> GetPermissionMenu(string AdminUserName) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iMenuPermissionDAL.GetPermissionMenu(UserName, AdminUserName); 
            return new JsonResult(lst);
        }
        [HttpPost] 
        [Route("SaveMenuPermission")] 
        public async Task<IActionResult> SaveMenuPermission(List<TypeMenuPermissionModel> obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iMenuPermissionDAL.SaveMenuPermission(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetUserWiseMenu")]
        public async Task<IActionResult> GetUserWiseMenu() 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iMenuPermissionDAL.GetUserWiseMenu(UserName);
            return new JsonResult(lst);
        }
    }
}
