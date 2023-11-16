
using DAL.Repository.Store.ProductOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using Model.Store.ProductOrder;

namespace WebAPI.Controllers.Store.ProductOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrderController : ControllerBase
    {
        private readonly IProductOrderDAL _IProductOrderDAL;
        //private readonly IUserRegisterDAL _iUserDAL;  

        //private readonly IHubContext<NotificationHubs> _hubContext; 
        public ProductOrderController(IProductOrderDAL iUserRegisterDAL
            //IHubContext<NotificationHubs> hubContext,
            //IUserRegisterDAL iUserDAL
            )  
        {
            this._IProductOrderDAL = iUserRegisterDAL;
            //_hubContext = hubContext;
            //this._iUserDAL = iUserDAL;
        }
        [HttpGet]
        [Route("GetOrderList")]
        public async Task<IActionResult> GetOrderList()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.GetOrderList(UserName);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails(int?Id) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.GetOrderDetails(UserName,Id );
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveProductOrder")]
        public async Task<IActionResult> SaveProductOrder(OrderHeadModel obj) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.SaveProductOrder(UserName, obj);
            //if (int.Parse(lst.ResultID) > 0)
            //{
            //    await _hubContext.Clients.All.SendAsync("NewOrder", lst);
            //}

            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateProductOrder")]
        public async Task<IActionResult> UpdateProductOrder(OrderHeadModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.UpdateProductOrder(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("CancelProductOrder")]
        public async Task<IActionResult> CancelProductOrder(int Id) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.DeleteProductOrder(UserName, Id);
            //if (int.Parse(lst.ResultID) > 0)
            //{
            //    await _hubContext.Clients.All.SendAsync("NewProductOrder", lst);
            //}
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetDefaultAddress")] 
        public async Task<IActionResult> GetDefaultAddress() 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.GetDefaultAddress(UserName);
            return new JsonResult(lst);
        }
        //[HttpGet]
        //[Route("GetCustomerInformationByMobileNumber")]
        //public async Task<IActionResult> GetCustomerInformationByMobileNumber(string MobileNumber) 
        //{
        //    var UserName = User?.Identity?.Name;
        //    var lst = await _IProductOrderDAL.GetDefaultAddress(MobileNumber);
        //    var userinfo = await _iUserDAL.GetUserInformationbyUserName(MobileNumber);
        //    CustomerInfoModel obj = new CustomerInfoModel();
        //    obj.addresslist = lst;
        //    obj.userinfo = userinfo;
        //    return new JsonResult(obj);
        //}
        [HttpPost]
        [Route("SaveProductOrderByAdmin")] 
        public async Task<IActionResult> SaveProductOrderByAdmin(OrderHeadModel obj) 
        {
            obj.OrderPlacedBy = "Admin"; 
            var UserName = User?.Identity?.Name;
            var lst = await _IProductOrderDAL.SaveProductOrder(UserName, obj);
            return new JsonResult(lst);
        }
    }
    //public class CustomerInfoModel
    //{
    //    public ApplicationUserViewModel? userinfo { get; set; } 
    //    public List<CustomerDefaultAddressModel>? addresslist { get; set; } 
 
    //}
}
