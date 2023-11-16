
using System.Data;
using System.Data.SqlClient;

using DAL.Common;
using DAL.Repository.Store.StockOrder;
using Model.Store.StockOrder;

namespace DAL.Services.Store.StockOrder
{
    public class StockOrderDAL : IStockOrderDAL     
    {
        private readonly IDataManager _dataManager;
        public StockOrderDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetProductSupplyOrder";
        private readonly string setSp = @"StoreDB.dbo.spSetProductSupplyOrder";    

        #region Load  Data Date: 04/04/2023 
        private readonly int LoadProductStockByOrderId  = 1,
	                           LoadProductSupplyOrderStatus  = 2,
                               LoadProductSupplyOrder  = 3,
	                           LoadProductSupplyDetails = 4; 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName,  List<TypeOrderHeadIdList>? HeadIdList, int?OrderHeadId,
            int? OrderNo,string? FromDate, string? ToDate, int? OrderStatusId,int? StoreId,int? SupplierId)   
            where T : class, new()
        {
            List<TypeOrderHeadIdList> typeOrderHeadIdLists = new List<TypeOrderHeadIdList>() { new TypeOrderHeadIdList() { OrderHeadId = -1 } };
            if (HeadIdList == null)
            {
                HeadIdList = typeOrderHeadIdLists; 
            }

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@OrderHeadId", OrderHeadId));
            parameterList.Add(new SqlParameter("@OrderNo", OrderNo));
            parameterList.Add(new SqlParameter("@FromDate", FromDate));
            parameterList.Add(new SqlParameter("@ToDate", ToDate));
            parameterList.Add(new SqlParameter("@OrderStatusId", OrderStatusId));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            parameterList.Add(new SqlParameter("@SupplierId", SupplierId));
            parameterList.Add(new SqlParameter("@OrderIdList", await _dataManager.ListToDataTable(HeadIdList)));  

            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);
            return new(lst);
        }
        public async Task<List<ProductStockViewModel>> GetProductList(string? UserName, List<TypeOrderHeadIdList>? HeadIdList)
        {
            var lst = await GetData<ProductStockViewModel>(LoadProductStockByOrderId, UserName, HeadIdList,  null, null, null, null, null, null, null);  
            return (lst);
        }
        public async Task<List<OrderStatusViewModel>> GetProductSupplyOrderStatus(string? UserName)
        {
            var lst = await GetData<OrderStatusViewModel>(LoadProductSupplyOrderStatus, UserName, null, null, null, null, null, null, null, null);  
            return (lst);
        }
        public async Task<List<ProductSupplyOrderViewModel>> GetProductSupplyOrder(string? UserName, int? OrderNo, string? FromDate, string? ToDate, int? OrderStatusId, int? StoreId, int? SupplierId)
        {
            var lst = await GetData<ProductSupplyOrderViewModel>(LoadProductSupplyOrder, UserName, null, null, OrderNo, FromDate, ToDate, OrderStatusId, StoreId, SupplierId);
            return (lst); 
        }
        public async Task<List<ProductSupplyDetailsViewModel>> GetProductSupplyDetails(string? UserName, int? OrderHeadId)
        {
            var lst = await GetData<ProductSupplyDetailsViewModel>(LoadProductSupplyDetails, UserName, null, OrderHeadId, null, null, null, null, null, null);  
            return (lst);
        }
        #endregion 

        #region Save  Date:09/04/2022 
        private readonly int Save = 1;
        private readonly int Update = 2;
        private readonly int Delete = 3;
        private readonly int Receive = 4;
        private readonly int AddStockToSingleProduct = 5;
        private readonly int AddStockBalanceToSingleProduct = 6;  

        private async Task<ResultObject> SetData(int? SaveOption, string? userName, ProductSupplyOrderModel obj)
        {
            List<TypeProductSupplyDetails>? ProductSupplyDetails = new List<TypeProductSupplyDetails>() { new TypeProductSupplyDetails() { Id = 0 } };
            if (obj.ProductSupplyDetails == null) { obj.ProductSupplyDetails = ProductSupplyDetails; }
            List<TypeExpenseHistoryModel>? ExpenceList = new List<TypeExpenseHistoryModel>() { new TypeExpenseHistoryModel() { ExpenseReasonId = 0 } };
            if (obj.ExpenceList == null) { obj.ExpenceList = ExpenceList; } 


            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@SupplierId", obj.SupplierId));
            parameterList.Add(new SqlParameter("@DeliveryDate", obj.DeliveryDate));
            parameterList.Add(new SqlParameter("@SupplyNote", obj.SupplyNote));
            parameterList.Add(new SqlParameter("@Quantity", obj.Quantity)); 
            
            parameterList.Add(new SqlParameter("@TypeProductSupplyDetails", await _dataManager.ListToDataTable(obj.ProductSupplyDetails)));
            parameterList.Add(new SqlParameter("@TypeExpenceHistory", await _dataManager.ListToDataTable(obj.ExpenceList))); 


            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveProductSupplyOrder(string? userName, ProductSupplyOrderModel obj)
        {
            var result = await SetData(Save, userName, obj); 
            return result;
        }
        public async Task<ResultObject> UpdateProductSupplyOrder(string? userName, ProductSupplyOrderModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> ReceiveProductSupplyOrder(string? userName, ProductSupplyOrderModel obj) 
        {
            var result = await SetData(Receive, userName, obj); 
            return result;
        }
        public async Task<ResultObject> DeleteProductSupplyOrder(string? userName, int Id)
        {
            ProductSupplyOrderModel model = new ProductSupplyOrderModel();
            model.Id = Id; 
            var result = await SetData(Delete, userName, model); 
            return result;
        } 
        public async Task<ResultObject> AddSingleStock(string? userName, ProductStockModel obj)    
        {  
                ProductSupplyOrderModel model = new ProductSupplyOrderModel() 
                {
                    Id = obj.Id, 
                    Quantity = obj.StockQty
                }; 
            var result = await SetData(AddStockToSingleProduct , userName, model); 
            return result; 
        }
        public async Task<ResultObject> AddSingleStockBalance(string? userName, ProductStockModel obj) 
        {
            ProductSupplyOrderModel model = new ProductSupplyOrderModel()
            {
                Id = obj.Id,
                Quantity = obj.StockQty
            };
            var result = await SetData(AddStockBalanceToSingleProduct, userName, model); 
            return result;
        }

        #endregion
        #region Product Stock order Date::28/08/2023
        private int 
               LoadProductStockOrder = 1,
               LoadApprovedStockOrder = 2,
               LoadReceivedStockOrder = 4,
               LoadCancelledStockOrder = 5,
               LoadNewOrderForApproval = 6 ;
        private async Task<List<T>> GetStockData<T>(int loadoption,string? UserName,int? OrderNumber,string? FromDate,string? ToDate,int? SupplierId)where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@OrderNumber", OrderNumber));
            parameterList.Add(new SqlParameter("@OrderStartDate", FromDate));
            parameterList.Add(new SqlParameter("@OrderEndDate", ToDate));
            parameterList.Add(new SqlParameter("@SupplierId", SupplierId));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>("StoreDB.dbo.spGetProductStockManagement", parameters);
            return new(lst); 
        }

        public async Task<List<ProductStockOrderViewModel>> GetProductStockOrder(int StatusId,string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        {
            var lst = await GetStockData<ProductStockOrderViewModel>(StatusId,  UserName,  OrderNumber,  FromDate,  ToDate, SupplierId);
            return (lst);
        }
       
        //public async Task<List<ProductStockOrderViewModel>> GetApprovedStockOrder(string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        //{
        //    var lst = await GetStockData<ProductStockOrderViewModel>(LoadApprovedStockOrder, UserName, OrderNumber, FromDate, ToDate, SupplierId);
        //    return (lst);
        //}
        //public async Task<List<ProductStockOrderViewModel>> GetReceivedStockOrder(string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        //{
        //    var lst = await GetStockData<ProductStockOrderViewModel>(LoadReceivedStockOrder, UserName, OrderNumber, FromDate, ToDate, SupplierId);
        //    return (lst);
        //}
        //public async Task<List<ProductStockOrderViewModel>> GetCancelledStockOrder(string? UserName, int? OrderNumber, string? FromDate, string? ToDate, int? SupplierId)
        //{
        //    var lst = await GetStockData<ProductStockOrderViewModel>(LoadCancelledStockOrder, UserName, OrderNumber, FromDate, ToDate, SupplierId);
        //    return (lst); 
        //}
        public async Task<List<ProductStockOrderViewModel>> GetNewOrderForApproval(string? UserName) 
        {
            var lst = await GetStockData<ProductStockOrderViewModel>(LoadNewOrderForApproval, UserName, null, null, null, null);   
            return (lst);
        }
        private async Task<ResultObject> SetApprovalData(int? SaveOption, string? userName, StockOrderAppovalModel obj)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@CancelledReason", obj.Reason));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetProductStockManagement", parameters);
            return (objResult);
        }
        private int Approved = 1,
            Cancelled = 2,
            AllowAddProduct = 3;

        public async Task<ResultObject> ApprovedStockOrder(string? userName, StockOrderAppovalModel obj)
        {
            var result = await SetApprovalData(Approved, userName, obj);  
            return result;
        }
        public async Task<ResultObject> CancelledStockOrder(string? userName, StockOrderAppovalModel obj)
        {
            var result = await SetApprovalData(Cancelled, userName, obj); 
            return result;
        }
        public async Task<ResultObject> AllowReAddProduct(string? userName, StockOrderAppovalModel obj) 
        {
            var result = await SetApprovalData(AllowAddProduct, userName, obj);
            return result;
        }
        private async Task<List<T>> GetStockProductData<T>( int loadoption, string? UserName, int? ProductCategoryId,
            int? ProductBrandId,int? OriginCountryId, int?StoreId,  int? StockQtyLessThen, int? StockQtygretherThen ) where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@StoreId", StoreId)); 
            parameterList.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            parameterList.Add(new SqlParameter("@ProductBrandId", ProductBrandId));
            parameterList.Add(new SqlParameter("@OriginCountryId", OriginCountryId));
            parameterList.Add(new SqlParameter("@StockQtyLessThen", StockQtyLessThen));
            parameterList.Add(new SqlParameter("@StockQtygretherThen", StockQtygretherThen));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>("StoreDB.dbo.spGetProductForStockOrder", parameters);
            return new(lst);
        }
        private int LoadAllProductForStock = 1, LoadRequestedStockQty = 2;  
        public async Task<List<StockProductModel>> GetAllProductForStock( string? UserName, int? ProductCategoryId, int? ProductBrandId, int? OriginCountryId,int?StoreId, int? StockQtyLessThen, int? StockQtygretherThen)
        {
            var lst = await GetStockProductData<StockProductModel>(LoadAllProductForStock, UserName,  ProductCategoryId, ProductBrandId,  OriginCountryId, StoreId, StockQtyLessThen,  StockQtygretherThen);
            return (lst);  
        }
        public async Task<List<StockProductModel>> GetRequestedStockQty(string? UserName, int? StoreId) 
        {
            var lst = await GetStockProductData<StockProductModel>(LoadRequestedStockQty, UserName, null, null, null, null, null, null);
            return (lst);
        }
        #endregion
        private async Task<List<T>> GetStockLevelData<T>(int loadoption, string? UserName, int? ProductCategoryId, int? StockLevel,int? StoreId) 
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>(); 
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            parameterList.Add(new SqlParameter("@StockLevel", StockLevel));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>("StoreDB.dbo.spGetQuickStockData", parameters);
            return new(lst);
        }
        private int LoadCurrentStock = 1, LoadProductCategory = 2;
        public async Task<List<StockLevelModel>> GetCurrentStock(string? UserName, int? ProductCategoryId, int? StockLevel, int? StoreId)
        { 
            var lst = await GetStockLevelData<StockLevelModel>(LoadCurrentStock, UserName, ProductCategoryId, StockLevel, StoreId);
            return (lst); 
        }
        public async Task<List<ProductCategoryModel>> GetCurrentProductCategory(string? UserName)
        {
            var lst = await GetStockLevelData<ProductCategoryModel>(LoadProductCategory, UserName, null, null, null);    
            return (lst); 
        }
        public async Task<ResultObject> SetStockLevel( string? userName, QuickStockLevelModel obj)    
        {
            List<SqlParameter> parameterList = new List<SqlParameter>(); 
            parameterList.Add(new SqlParameter("@SaveOption", 1));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@StoreId", obj.StoreId));
            parameterList.Add(new SqlParameter("@ProductId", obj.ProductId));
            parameterList.Add(new SqlParameter("@CurrentStockQty", obj.CurrentStockQty)); 
            parameterList.Add(new SqlParameter("@LowStockLevelQty", obj.LowStockLevelQty)); 
            parameterList.Add(new SqlParameter("@ReOrderQty", obj.ReOrderQty));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetQuickStockData", parameters);
            return (objResult);
        }
        public async Task<ResultObject> SetRequestProductStock(string? userName, RequestProductStockModel obj) 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", 1));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@StoreId", obj.StoreId));
            parameterList.Add(new SqlParameter("@ProductId", obj.ProductId));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetRequestProductStock", parameters);
            return (objResult);
        }
        public async Task<ResultObject> DeleteRequestProductStock(string? userName, RequestProductStockModel obj) 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>(); 
            parameterList.Add(new SqlParameter("@SaveOption", 2));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@StoreId", obj.StoreId));
            parameterList.Add(new SqlParameter("@ProductId", obj.ProductId));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetRequestProductStock", parameters); 
            return (objResult);
        }
        public async Task<ResultObject> DeleteAllRequestProductStock(string? userName, RequestProductStockModel obj)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>(); 
            parameterList.Add(new SqlParameter("@SaveOption", 3));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@StoreId", obj.StoreId));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetRequestProductStock", parameters);
            return (objResult);
        }

        private async Task<ResultObject> SetDamageProduct(int? SaveOption,string? userName, ProductDamageOrExpireHistoryModel obj)  
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@ProductId", obj.ProductId));
            parameterList.Add(new SqlParameter("@StoreId", obj.StoreId));
            parameterList.Add(new SqlParameter("@Quantity", obj.Quantity));
            parameterList.Add(new SqlParameter("@DamageTypeId", obj.DamageTypeId));
            parameterList.Add(new SqlParameter("@Remarks", obj.Remarks));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.dbo.spSetProductDamageOrExpireHistory", parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveDamageProduct(string? userName, ProductDamageOrExpireHistoryModel obj) 
        {
            var result = await SetDamageProduct(1, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateDamageProduct(string? userName, ProductDamageOrExpireHistoryModel obj)
        {
            var result = await SetDamageProduct(2, userName, obj); 
            return result;
        }
        public async Task<ResultObject> DeleteDamageProduct(string? userName, ProductDamageOrExpireHistoryModel obj)
        {
            var result = await SetDamageProduct(3, userName, obj);  
            return result;
        }
     
        private async Task<List<Dictionary<string, string>>> GetDamageProduct(int loadoption, string? UserName, int? StoreId,
           int? ProductCategoryId, string? FromDate, string? Todate,int? DamegeTypeId)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            parameterList.Add(new SqlParameter("@CategoryId", ProductCategoryId));
            parameterList.Add(new SqlParameter("@FromDate", FromDate));
            parameterList.Add(new SqlParameter("@Todate", Todate));
            parameterList.Add(new SqlParameter("@DamegeTypeId", DamegeTypeId));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnDictionaryListBySP("StoreDB.dbo.spGetProductDamageOrExpireHistory", parameters);
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetDamagedProduct(string? UserName, int? StoreId, 
           int? ProductCategoryId, string? FromDate, string? Todate, int? DamegeTypeId)
        {
            var list = await GetDamageProduct(1, UserName,StoreId, ProductCategoryId,FromDate,Todate,DamegeTypeId);
            return new(list);
        }

        private async Task<List<Dictionary<string, string>>> GetExpenseData(int loadoption, string? UserName, int? Id)
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", Id));
            SqlParameter[] parameters = parameterList.ToArray();
            var list = await _dataManager.ReturnDictionaryListBySP("StoreDB.dbo.spGetExpenseHistory", parameters);
            return new(list);
        }
        public async Task<List<Dictionary<string, string>>> GetExpenseReason(string? UserName)
        {
            var list = await GetExpenseData(1, UserName, null);  
            return new(list);
        }
        private async Task<List<T>> LoadExpenseHistory<T>(int loadoption, string? UserName, int? StoreId,  int? Id, 
            string? FromDate, string? ToDate, int?OrderNo)
           where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", Id));
            parameterList.Add(new SqlParameter("@StoreId", StoreId));
            parameterList.Add(new SqlParameter("@FromDate", FromDate));
            parameterList.Add(new SqlParameter("@ToDate", ToDate));
            parameterList.Add(new SqlParameter("@OrderNo", OrderNo));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>("StoreDB.dbo.spGetExpenseHistory", parameters);
            return new(lst);
        }
        public async Task<List<ExpenseHistoryViewModel>> GetExpenseHistoryHead(string?UserName,int?StoreId,int? Id,
            string? FromDate,string? ToDate, int? OrderNo)
        {
            var lst = await LoadExpenseHistory<ExpenseHistoryViewModel>(2,UserName,StoreId,Id,FromDate,ToDate,OrderNo);
            return (lst);
        }
        public async Task<List<ExpenseHistoryDetailsViewModel>> GetExpenseHistoryDetail(string? UserName, int? Id)
        {
            var lst = await LoadExpenseHistory<ExpenseHistoryDetailsViewModel>(3, UserName, null, Id, null, null, null); 
            return (lst);
        }

    }
}
