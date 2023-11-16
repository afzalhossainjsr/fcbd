using DAL.Repository.Store.StockOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.StockOrder;

namespace WebAPI.Controllers.Store.StockOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockOrderController : ControllerBase
    {
        private readonly IStockOrderDAL _IStockOrderDAL;
        public StockOrderController(IStockOrderDAL iUserRegisterDAL) 
        {
            this._IStockOrderDAL = iUserRegisterDAL;
        }
        [HttpPost]
        [Route("GetProductList")] 
        public async Task<IActionResult> GetProductList(List<TypeOrderHeadIdList> HeadIdList) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetProductList(UserName, HeadIdList);  
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductSupplyOrderStatus")]
        public async Task<IActionResult> GetProductSupplyOrderStatus()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetProductSupplyOrderStatus(UserName);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductSupplyOrder")]
        public async Task<IActionResult> GetProductSupplyOrder(int? OrderNo, string? FromDate, string? ToDate, int? OrderStatusId, int? StoreId, int? SupplierId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetProductSupplyOrder(UserName,  OrderNo, FromDate,  ToDate, OrderStatusId,  StoreId,SupplierId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductSupplyDetails")]
        public async Task<IActionResult> GetProductSupplyDetails(int? OrderHeadId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetProductSupplyDetails(UserName, OrderHeadId); 
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveProductSupplyOrder")]
        public async Task<IActionResult> SaveProductSupplyOrder(ProductSupplyOrderModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.SaveProductSupplyOrder(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateProductSupplyOrder")]
        public async Task<IActionResult> UpdateProductSupplyOrder(ProductSupplyOrderModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.UpdateProductSupplyOrder(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("ReceiveProductSupplyOrder")] 
        public async Task<IActionResult> ReceiveProductSupplyOrder(ProductSupplyOrderModel obj) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.ReceiveProductSupplyOrder(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("DeleteProductSupplyOrder")]
        public async Task<IActionResult> DeleteProductSupplyOrder(int Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.DeleteProductSupplyOrder(UserName, Id); 
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("AddSingleStock")]
        public async Task<IActionResult> AddSingleStock(ProductStockModel obj) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.AddSingleStock(UserName, obj); 
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("AddSingleStockBalance")] 
        public async Task<IActionResult> AddSingleStockBalance(ProductStockModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.AddSingleStockBalance(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductStockOrder")]
        public async Task<IActionResult> GetProductStockOrder(int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId, int StatusId = 1)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetProductStockOrder(StatusId, UserName, OrderNumber, FromDate, ToDate, SupplierId); 
            return new JsonResult(lst);
        }
        //[HttpGet]
        //[Route("GetApprovedStockOrder")]
        //public async Task<IActionResult> GetApprovedStockOrder(int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        //{
        //    var UserName = User?.Identity?.Name;
        //    var lst = await _IStockOrderDAL.GetApprovedStockOrder(UserName, OrderNumber, FromDate, ToDate, SupplierId);
        //    return new JsonResult(lst);
        //}
        //[HttpGet]
        //[Route("GetReceivedStockOrder")]
        //public async Task<IActionResult> GetReceivedStockOrder(int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        //{
        //    var UserName = User?.Identity?.Name;
        //    var lst = await _IStockOrderDAL.GetReceivedStockOrder(UserName, OrderNumber, FromDate, ToDate, SupplierId);
        //    return new JsonResult(lst);
        //}
        //[HttpGet]
        //[Route("GetCancelledStockOrder")] 
        //public async Task<IActionResult> GetCancelledStockOrder(int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        //{
        //    var UserName = User?.Identity?.Name;
        //    var lst = await _IStockOrderDAL.GetCancelledStockOrder(UserName, OrderNumber, FromDate, ToDate, SupplierId);
        //    return new JsonResult(lst);
        //}
        [HttpGet]
        [Route("GetNewOrderForApproval")] 
        public async Task<IActionResult> GetNewOrderForApproval()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetNewOrderForApproval(UserName);   
            return new JsonResult(lst); 
        }
        [HttpPost]
        [Route("ApprovedStockOrder")]
        public async Task<IActionResult> ApprovedStockOrder(StockOrderAppovalModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.ApprovedStockOrder(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("CancelledStockOrder")]
        public async Task<IActionResult> CancelledStockOrder(StockOrderAppovalModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.CancelledStockOrder(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("AllowReAddProduct")]
        public async Task<IActionResult> AllowReAddProduct(StockOrderAppovalModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.AllowReAddProduct(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetAllProductForStock")] 
        public async Task<IActionResult> GetAllProductForStock(int? ProductCategoryId, int? ProductBrandId, int? OriginCountryId,int?StoreId, int? StockQtyLessThen, int? StockQtygretherThenOrEqual)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetAllProductForStock(UserName, ProductCategoryId, ProductBrandId, OriginCountryId, StoreId, StockQtyLessThen, StockQtygretherThenOrEqual);
            return new JsonResult(lst); 
        }
        [HttpGet]
        [Route("GetRequestedStockQty")]
        public async Task<IActionResult> GetRequestedStockQty(int? StoreId) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetRequestedStockQty(UserName, StoreId); 
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetCurrentStock")]
        public async Task<IActionResult> GetCurrentStock(int? ProductCategoryId, int? StockLevel, int? StoreId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetCurrentStock(UserName,  ProductCategoryId,  StockLevel,  StoreId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetCurrentProductCategory")]
        public async Task<IActionResult> GetCurrentProductCategory()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetCurrentProductCategory(UserName);  
            return new JsonResult(lst);
        }
       
        [HttpPost]
        [Route("SetStockLevel")]
        public async Task<IActionResult> SetStockLevel(QuickStockLevelModel obj) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.SetStockLevel(UserName, obj); 
            return new JsonResult(lst);
        }

        [HttpPost]
        [Route("SaveRequestProductStock")]
        public async Task<IActionResult> SaveRequestProductStock(RequestProductStockModel obj)  
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.SetRequestProductStock(UserName, obj);
            return new JsonResult(lst); 
        }
        [HttpPost]
        [Route("DeleteRequestProductStock")] 
        public async Task<IActionResult> DeleteRequestProductStock(RequestProductStockModel obj)
        {
            var UserName = User?.Identity?.Name;  
            var lst = await _IStockOrderDAL.DeleteRequestProductStock(UserName, obj); 
            return new JsonResult(lst); 
        }
        [HttpPost]
        [Route("DeleteAllRequestProductStock")]
        public async Task<IActionResult> DeleteAllRequestProductStock(RequestProductStockModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.DeleteAllRequestProductStock(UserName, obj);
            return new JsonResult(lst); 
        }
        [HttpPost]
        [Route("SaveDamageProduct")]
        public async Task<IActionResult> SaveDamageProduct(ProductDamageOrExpireHistoryModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.SaveDamageProduct(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateDamageProduct")]
        public async Task<IActionResult> UpdateDamageProduct(ProductDamageOrExpireHistoryModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.UpdateDamageProduct(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("DeleteDamageProduct")]
        public async Task<IActionResult> DeleteDamageProduct(int Id)
        {
            var UserName = User?.Identity?.Name;
            ProductDamageOrExpireHistoryModel obj = new ProductDamageOrExpireHistoryModel()
            {
                Id = Id
            };
            var lst = await _IStockOrderDAL.DeleteDamageProduct(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetDamagedProduct")]
        public async Task<IActionResult> GetDamagedProduct(int? StoreId,
           int? ProductCategoryId, string? FromDate, string? Todate, int? DamegeTypeId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetDamagedProduct(UserName, StoreId, ProductCategoryId, FromDate, Todate, DamegeTypeId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetExpenseReason")]
        public async Task<IActionResult> GetExpenseReason()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetExpenseReason(UserName);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetExpenseHistoryHead")]
        public async Task<IActionResult> GetExpenseHistoryHead( int? StoreId,int? Id,string? FromDate,string? ToDate,int? OrderNo)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetExpenseHistoryHead(UserName,StoreId,Id,FromDate,ToDate,OrderNo);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetExpenseHistoryDetail")] 
        public async Task<IActionResult> GetExpenseHistoryDetail(int? Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStockOrderDAL.GetExpenseHistoryDetail(UserName,  Id);
            return new JsonResult(lst);
        }
    }
}
