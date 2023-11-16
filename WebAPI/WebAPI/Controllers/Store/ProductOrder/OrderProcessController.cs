using DAL.Repository.Store.ProductOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.ProductOrder;

namespace WebAPI.Controllers.Store.ProductOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessController : ControllerBase
    {
        private readonly IOrderProcessDAL _IOrderProcessDAL;
        public OrderProcessController(IOrderProcessDAL iUserRegisterDAL)
        {
            this._IOrderProcessDAL = iUserRegisterDAL;
        }
        [HttpGet]
        [Route("GetProductOrder")]
        public async Task<IActionResult> GetProductOrder(string? StartDate, string? EndDate, string? OrderNo,
            string? SearchText, int? OrderStatusId, int? OrderTypeId, int Page = 1, int pagingsize = 100)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.GetProductOrder(UserName, StartDate, EndDate, OrderNo, SearchText, OrderStatusId, OrderTypeId,Page, pagingsize);  
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductOrderDetails")] 
        public async Task<IActionResult> GetProductOrderDetails(int? OrderHeadId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.GetProductOrderDetails(UserName, OrderHeadId);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveConfirmedOrder")]
        public async Task<IActionResult> SaveConfirmedOrder(OrderProcessConfirmationModel obj )
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.SaveConfirmedOrder(UserName,obj );
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveCancelledOrder")]
        public async Task<IActionResult> SaveCancelledOrder(int? Id) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.SaveCancelledOrder(UserName, Id);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("ChangeOrderStatus")] 
        public async Task<IActionResult> ChangeOrderStatus(OrderProcessStatusChangeModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.ChangeOrderStatus(UserName, obj);  
            return new JsonResult(lst); 
        }
        [HttpPost]
        [Route("CancelSingleProduct")]
        public async Task<IActionResult> CancelSingleProduct(OrderProcessStatusChangeModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.CancelSingleProduct(UserName, obj); 
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("RestoreProductFromCancelOrderItem")] 
        public async Task<IActionResult> RestoreProductFromCancelOrderItem(OrderProcessStatusChangeModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.RestoreProductFromCancelOrderItem(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetOrderByOrderNumber")]
        public async Task<IActionResult> GetOrderByOrderNumber( string? OrderNumber)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.GetOrderByOrderNumber(UserName, OrderNumber);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveOrderReturn")]
        public async Task<IActionResult> SaveOrderReturn( ProductReturnModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IOrderProcessDAL.SaveOrderReturn(UserName, obj);
            return new JsonResult(lst);
        }
    }
}
