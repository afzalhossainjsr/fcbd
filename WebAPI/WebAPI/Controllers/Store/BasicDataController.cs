using DAL.Repository.Store;
using DAL.Repository.Store.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.Product;

namespace WebAPI.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicDataController : ControllerBase
    {
        private readonly IBasicDataDAL _IBasicDataDAL;
        private readonly IColorInfoDAL _IColorInfoDAL;  
        


        public BasicDataController(IBasicDataDAL iUserRegisterDAL, IColorInfoDAL IColorInfoDAL)
        {
            this._IBasicDataDAL = iUserRegisterDAL;
            this._IColorInfoDAL = IColorInfoDAL; 
        }
        [HttpGet]
        [Route("GetCountryInfo")]
        public async Task<IActionResult> GetCountryInfo()
        {
            var lst = await _IBasicDataDAL.GetCountryInfo();
            return new JsonResult(lst); 
        }
        [HttpGet]
        [Route("GetMeasurement")]
        public async Task<IActionResult> GetMeasurement()
        {
            var lst = await _IBasicDataDAL.GetMeasurement();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetTax")]
        public async Task<IActionResult> GetTax()
        {
            var lst = await _IBasicDataDAL.GetTax();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetColorInfo")]
        public async Task<IActionResult> GetColorInfo()
        {
            var lst = await _IBasicDataDAL.GetColorInfo();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetWarrantyType")]
        public async Task<IActionResult> GetWarrantyType()
        {
            var lst = await _IBasicDataDAL.GetWarrantyType();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetWarrantyPeriod")]
        public async Task<IActionResult> GetWarrantyPeriod()
        {
            var lst = await _IBasicDataDAL.GetWarrantyPeriod();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetUsageAreaParameter")]
        public async Task<IActionResult> GetUsageAreaParameter()
        {
            var lst = await _IBasicDataDAL.GetUsageAreaParameter();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetOrderType")]
        public async Task<IActionResult> GetOrderType()
        {
            var lst = await _IBasicDataDAL.GetOrderType();
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var lst = await _IBasicDataDAL.GetOrderStatus();
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveColorInfo")]
        public async Task<IActionResult> SaveColorInfo(StoreInfoModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IColorInfoDAL.SaveColorInfo(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateColorInfo")]
        public async Task<IActionResult> UpdateColorInfo(StoreInfoModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IColorInfoDAL.UpdateColorInfo(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("DeleteColorInfo")] 
        public async Task<IActionResult> DeleteColorInfo(int? Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IColorInfoDAL.DeleteColorInfo(UserName, Id); 
            return new JsonResult(lst);
        }
    }
}
