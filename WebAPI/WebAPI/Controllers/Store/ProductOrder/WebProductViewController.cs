using DAL.Repository.Store.ProductOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Store.ProductOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebProductViewController : ControllerBase
    {
        private readonly IWebProductViewDAL _IWebProductViewDAL;

        public WebProductViewController (IWebProductViewDAL iUserRegisterDAL)
        {
            this._IWebProductViewDAL = iUserRegisterDAL;
         
        }
        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProduct(
            int? ProductCategoryId, int? ProductId, string? SearchText, int? ProductBrandId,
            string? ProductColor, string? ProductSize, int? PriceStart, int? PriceEnd, int? WarrantyTypeParameterId,
            int? WarrantyPeriod, int? PriceDisplayOrder, int? OriginCountryId, int? Page = 1, int PageSize = 10) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IWebProductViewDAL.GetProduct(UserName, ProductCategoryId, ProductId, SearchText, ProductBrandId,
           ProductColor, ProductSize, PriceStart, PriceEnd, WarrantyTypeParameterId,
             WarrantyPeriod, PriceDisplayOrder, OriginCountryId, Page, PageSize);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductCategory")] 
        public async Task<IActionResult> GetProductCategory()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IWebProductViewDAL.GetProductCategory(UserName);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetOfferProduct")] 
        public async Task<IActionResult> GetOfferProduct()
        {
            var lst = await _IWebProductViewDAL.GetOfferProduct();
            return new JsonResult(lst); 
        }
    }
}
