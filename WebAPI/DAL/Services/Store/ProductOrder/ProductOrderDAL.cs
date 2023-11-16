using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using DAL.Repository.Store.ProductOrder;
using Model.Store.ProductOrder;

namespace DAL.Services.Store.ProductOrder
{
    public class ProductOrderDAL : IProductOrderDAL 
    {
        private readonly IDataManager _dataManager;
        public ProductOrderDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private readonly string getSp = @"StoreDB.dbo.spGetProductOrder";
        private readonly string setSp = "StoreDB.dbo.spSetProductOrder";

        readonly int LoadProductOrder  = 2,
		            OrderDetails = 3,
                    LoadDefaultAddress = 4;

        #region Load  Data Date: 16/04/2023 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName,int?Id) 
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", Id)); 
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<OrderHeadViewModel>> GetOrderList(string? UserName) 
        {
            var lst = await GetData<OrderHeadViewModel>(LoadProductOrder, UserName,null); 
            return (lst);
        }
        public async Task<List<OrderDetailsViewModel>> GetOrderDetails(string? UserName, int?Id )
        {
            var lst = await GetData<OrderDetailsViewModel>(OrderDetails, UserName, Id);
            return (lst); 
        }
        public async Task<List<CustomerDefaultAddressModel>> GetDefaultAddress(string? UserName ) 
        {
            var lst = await GetData<CustomerDefaultAddressModel>(LoadDefaultAddress, UserName, null);  
            return (lst);
        }
        #endregion Load Data  

        #region Save  Date: 27/03/2023  
        private readonly int Save = 1;
        private readonly int Update = 2;
        private readonly int Delete = 3;

        private async Task<ResultObject> SetData(int? SaveOption, string? userName, OrderHeadModel obj)
        {

            List<TypeOrderDetailsModel>? OrderDetails = new List<TypeOrderDetailsModel>()
            {
                new TypeOrderDetailsModel() { OrderDetailId = -1, OrderQty = 0}
            };
            if(obj.OrderDetails == null)
            {
                obj.OrderDetails = OrderDetails;
            }

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@BuyerUserName", obj.BuyerUserName)); 
            parameterList.Add(new SqlParameter("@OrderPlacedBy", obj.OrderPlacedBy));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@OrderTypeId", obj.OrderTypeId));
            parameterList.Add(new SqlParameter("@OrderStatusId", obj.OrderStatusId));
            parameterList.Add(new SqlParameter("@PaymentMethodId", obj.PaymentMethodId));
            parameterList.Add(new SqlParameter("@DeliveryCharge", obj.DeliveryCharge));
            parameterList.Add(new SqlParameter("@LocationId", obj.LocationId));
            parameterList.Add(new SqlParameter("@Address", obj.Address));
            parameterList.Add(new SqlParameter("@Apt_Suit", obj.Apt_Suit));
            parameterList.Add(new SqlParameter("@District", obj.District));
            parameterList.Add(new SqlParameter("@City", obj.City));
            parameterList.Add(new SqlParameter("@Country", obj.Country));
            parameterList.Add(new SqlParameter("@State", obj.State));
            parameterList.Add(new SqlParameter("@PostCode", obj.PostCode));
            parameterList.Add(new SqlParameter("@Direction", obj.Direction));
            parameterList.Add(new SqlParameter("@Lat", obj.Lat));
            parameterList.Add(new SqlParameter("@Lang", obj.Lang));
            parameterList.Add(new SqlParameter("@PhoneNumber", obj.PhoneNumber));
            parameterList.Add(new SqlParameter("@EmailAddress", obj.EmailAddress)); 
            parameterList.Add(new SqlParameter("@AdditionalPhoneNumber", obj.AdditionalPhoneNumber));

            parameterList.Add(new SqlParameter("@OrderDetails", await _dataManager.ListToDataTable(obj.OrderDetails)));

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveProductOrder(string? userName, OrderHeadModel obj)
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateProductOrder(string? userName, OrderHeadModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> DeleteProductOrder(string? userName, int? Id) 
        {
            OrderHeadModel obj = new OrderHeadModel() { Id = Id };
            var result = await SetData(Delete, userName, obj);
            return result;
        }
        #endregion 
    }
}
