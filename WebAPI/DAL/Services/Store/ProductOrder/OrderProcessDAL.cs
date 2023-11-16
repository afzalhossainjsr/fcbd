using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using DAL.Repository.Store.ProductOrder;
using Model.Basic;
using Model.Store.ProductOrder;

namespace DAL.Services.Store.ProductOrder
{
    public class OrderProcessDAL: IOrderProcessDAL 
    {
        private readonly IDataManager _dataManager;
        public OrderProcessDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetOrderProcess";
        private readonly string setSp = @"StoreDB.dbo.spSetOrderProcess";  

        #region Load  Data Date: 02/04/2023 
        private readonly int LoadProductOrder = 1;
        private readonly int LoadOrderDetails = 2;
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName, int? Id , string? StartDate,
            string? EndDate, string? OrderNo, string? SearchText, int? OrderStatusId, int? OrderTypeId, 
            int? Page = 1, int? PagingSize = 100) where T : class, new() 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", Id));
            parameterList.Add(new SqlParameter("@StartDate", StartDate));
            parameterList.Add(new SqlParameter("@EndDate", EndDate));
            parameterList.Add(new SqlParameter("@OrderNo", OrderNo));
            parameterList.Add(new SqlParameter("@SearchText", SearchText));
            parameterList.Add(new SqlParameter("@OrderStatusId", OrderStatusId));
            parameterList.Add(new SqlParameter("@OrderTypeId", OrderTypeId));
            parameterList.Add(new SqlParameter("@OffsetValue", (Page - 1) * PagingSize));
            parameterList.Add(new SqlParameter("@PagingSize", PagingSize));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);
            return new(lst); 
        }
        public async Task<OrderHeadDataModel> GetProductOrder(string? UserName,  string? StartDate, string? EndDate, string? OrderNo, string? SearchText, int? OrderStatusId, int? OrderTypeId, int page = 1, int Pagingsize = 100)  
        { 
            var lst = await GetData<OrderHeadViewModel>(LoadProductOrder, UserName, null, StartDate, EndDate,  OrderNo,  SearchText,  OrderStatusId,  OrderTypeId, page, Pagingsize);
            OrderHeadDataModel model = new OrderHeadDataModel()
            {
                OrderHeadList = lst .ToList()
            };

            if (model.OrderHeadList.Count > 0)
            {
                model.pager = new Pager(model.OrderHeadList[0].TotalRows ?? 0, page, Pagingsize);   
            }
            else
            {
                model.pager = new Pager(0, page, Pagingsize);  
            }

            return (model);
        }
        public async Task<List<OrderDetailsViewModel>> GetProductOrderDetails(string? UserName,int?OrderHeadId)   
        {
            var lst = await GetData<OrderDetailsViewModel>(LoadOrderDetails, UserName, OrderHeadId, null, null, null, null, null, null);
            return (lst);
        }
        #endregion Load Data  

        #region OrderConfirmation Date:03/04/2023
        private int ConfirmedOrder = 1,
            CancelledOrder = 2,
            ViewOrder = 3,
            StatusChange =4,
            CancelSingleProductFromOrder  = 5,
			RestoreSingleProductFromCancelOrderItem  = 6;
        private async Task<ResultObject> SetData(int? SaveOption, string? userName, OrderProcessConfirmationModel obj) 
        {
            List<TypeOrderDetailsConfirmation>? TypeOrderDetailsConfirmation = new List<TypeOrderDetailsConfirmation>() { new TypeOrderDetailsConfirmation() {  OrderDetailId = -1 } };
            if (obj.TypeOrderDetailsConfirmation == null) { obj.TypeOrderDetailsConfirmation = TypeOrderDetailsConfirmation; }

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@OrderStatusId", obj.OrderStatusId)); 
            parameterList.Add(new SqlParameter("@CancellationReason", obj.CancellationReason));

            parameterList.Add(new SqlParameter("@TypeOrderDetailsConfirmation", await _dataManager.ListToDataTable(obj.TypeOrderDetailsConfirmation)));

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveConfirmedOrder(string? userName, OrderProcessConfirmationModel obj) 
        {
            var result = await SetData(ConfirmedOrder, userName, obj);  
            return result;
        }
        public async Task<ResultObject> SaveCancelledOrder(string? userName, int ?Id )
        {
            OrderProcessConfirmationModel model = new OrderProcessConfirmationModel();
            model.Id = Id; 
            var result = await SetData(CancelledOrder, userName, model); 
            return result;
        }
        public async Task<ResultObject> ChangeOrderStatus(string? userName, OrderProcessStatusChangeModel obj)  
        {
            OrderProcessConfirmationModel model = new OrderProcessConfirmationModel()
            {
                CancellationReason = obj.Reason,
                Id = obj.Id,
                OrderStatusId = obj.OrderStatusId 

            };
            var result = await SetData(StatusChange, userName, model); 
            return result;
        }
        public async Task<ResultObject> CancelSingleProduct(string? userName, OrderProcessStatusChangeModel obj) 
        {
            OrderProcessConfirmationModel model = new OrderProcessConfirmationModel()
            {
                CancellationReason = obj.Reason,
                Id = obj.Id
            };
            var result = await SetData(CancelSingleProductFromOrder, userName, model); 
            return result; 
        }
        public async Task<ResultObject> RestoreProductFromCancelOrderItem(string? userName, 
            OrderProcessStatusChangeModel obj)
        {
            OrderProcessConfirmationModel model = new OrderProcessConfirmationModel()
            {
                Id = obj.Id
            };
            var result = await SetData(RestoreSingleProductFromCancelOrderItem, userName, model);   
            return result;
        }
        #endregion
        private async Task<ResultObject> SetOrderReturn(int? SaveOption, string? userName, ProductReturnModel obj)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>(); 
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@OrderDetailId", obj.OrderDetailId));
            parameterList.Add(new SqlParameter("@ReturnQty", obj.ReturnQty));
            parameterList.Add(new SqlParameter("@Reason", obj.Reason));
            parameterList.Add(new SqlParameter("@ReturnDate", obj.ReturnDate)); 

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetOrderReturn", parameters);
            return (objResult);
        }

        public async Task<ResultObject> SaveOrderReturn(string? userName,ProductReturnModel obj)
        {
            var result = await SetOrderReturn(1, userName, obj); 
            return result;
        }

        private async Task<List<T>> GetOrderReturn<T>(int loadoption, string? UserName,  string? OrderNumber) where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@OrderNumber", OrderNumber));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>("StoreDB.dbo.spGetOrderReturn", parameters); 
            return new(lst); 
        }
      
        public async Task<List<OrderReturnViewModel>> GetOrderByOrderNumber(string? userName, string? OrderNumber)
        {
            var lst = await GetOrderReturn<OrderReturnViewModel>(1, userName, OrderNumber);   
            return lst;
        }
       
    }
}
