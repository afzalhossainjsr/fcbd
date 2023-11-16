using DAL.Repository.Store.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.Product;

namespace WebAPI.Controllers.Store.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductSupplierController : ControllerBase
    {
        private readonly IStoreProductSupplierDAL _iStoreProductSupplierDAL;
       

        public StoreProductSupplierController(IStoreProductSupplierDAL iUserRegisterDAL)
        {
            this._iStoreProductSupplierDAL = iUserRegisterDAL;

        }
        [HttpGet]
        [Route("GetProductSupplier")] 
        public async Task<IActionResult> GetProductSupplier(string?SearchText)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductSupplierDAL.GetProductSupplier(UserName, SearchText); 
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveProductSupplier")]
        public async Task<IActionResult> SaveProductSupplier(StoreProductSupplierModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductSupplierDAL.SaveProductSupplier(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateProductSupplier")]
        public async Task<IActionResult> UpdateProductSupplier(StoreProductSupplierModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductSupplierDAL.UpdateProductSupplier(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("DeleteProductSupplier")]
        public async Task<IActionResult> DeleteProductSupplier(int? Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductSupplierDAL.DeleteProductSupplier(UserName, Id);
            return new JsonResult(lst);
        }
    }
}
