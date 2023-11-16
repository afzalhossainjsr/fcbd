using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using DAL.Repository.Store.Product;
using Model.Store.Product;

namespace DAL.Services.Store.Product
{
    public class StoreProductDAL: IStoreProductDAL 
    {
        private readonly IDataManager _dataManager;
        public StoreProductDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetProduct";
        private readonly string setSp = "StoreDB.dbo.spSetProduct";

        #region Load  Data Date: 13/03/2023 
        readonly int LoadProduct = 1,
                    LoadSKU = 2,
                    LoadProductColor = 3,
                    LoadSize = 4,
                    LoadProductFeaturesList = 5,
                    LoadProductOfferList = 6,
                    LoadProductDescription = 7,
                    LoadProductImage = 8,
                    LoadStoreWiseProductLowStockAndReOrdering = 9,
                    LoadProductUsageArea = 10;

        private async Task<List<T>> GetData<T>(int loadoption, string? UserName, int? Id, string? SearchText,
            int? ProductCategoryId, int? ProductBrandId, int? ProductSupplierId)
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            parameterList.Add(new SqlParameter("@Id", Id));
            parameterList.Add(new SqlParameter("@SearchText", SearchText));
            parameterList.Add(new SqlParameter("@CategoryId", ProductCategoryId));
            parameterList.Add(new SqlParameter("@BrandId", ProductBrandId));
            parameterList.Add(new SqlParameter("@SupplierId", ProductSupplierId));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<StoreProductViewModel>> GetProduct(string? UserName, int? Id, string? SearchText,
            int? ProductCategoryId, int? ProductBrandId, int? ProductSupplierId)
        {
            var result = await  GetData <StoreProductViewModel>(LoadProduct, UserName,  Id,  SearchText,  ProductCategoryId,  ProductBrandId, ProductSupplierId);
            return result;
        }
        public async Task<StoreProductInitialViewModel> GetProductInitialData(string? UserName, int? Id)
        {
            StoreProductInitialViewModel model = new StoreProductInitialViewModel();
            model.ProductSKU = await GetData<ProductSKUViewModel>(LoadSKU, UserName, Id, null, null, null, null);
            model.ProductColor = await GetData<ProductColorViewModel>(LoadProductColor, UserName, Id, null, null, null, null);
            model.ProductSize = await GetData<ProductSizeViewModel>(LoadSize, UserName, Id, null, null, null, null);
            model.ProductImage = await GetData<ProductImageViewModel>(LoadProductImage, UserName, Id, null, null, null, null);
            model.ProductUsageArea = await GetData<ProductUsageAreaViewModel>(LoadProductUsageArea, UserName, Id, null, null, null, null);
            model.ProductFeaturesList = await GetData<ProductFeaturesListViewModel>(LoadProductFeaturesList, UserName, Id, null, null, null, null);
            model.ProductOfferList = await GetData<ProductOfferListViewModel>(LoadProductOfferList, UserName, Id, null, null, null, null);
            model.ProductDescription = await GetData<ProductDescriptionViewModel>(LoadProductDescription, UserName, Id, null, null, null, null);
            model.StoreWiseProductLowStockAndReOrdering = await GetData<StoreWiseProductLowStockAndReOrderingViewModel>(LoadStoreWiseProductLowStockAndReOrdering, UserName, Id, null, null, null, null);
            return (model);
        }
        #endregion
        #region Save Product Date:23/04/2022 
        private readonly int Save = 1;
        private int Update = 2;
        private int Delete = 3;

        private async Task<ResultObject> SetData(int? SaveOption, string? userName, StoreProductModel obj)
        {
            List<TypeProductSKUModel>? ProductSKU = new List<TypeProductSKUModel>() { new TypeProductSKUModel() { Id = 0, SKU = "Dummy" } };
            if (obj.ProductSKU == null) { obj.ProductSKU = ProductSKU; }

            List<TypeStoreWiseProductStockModel>? LocationWiseProductStock = new List<TypeStoreWiseProductStockModel>() { new TypeStoreWiseProductStockModel() { Id = 0, StoreId = 0, Quantity = 0 } };
            if (obj.StoreWiseProductStock == null) { obj.StoreWiseProductStock = LocationWiseProductStock; }

            List<TypeProductLowStockAndReOrderingModel>? ProductLowStockAndReOrdering = new List<TypeProductLowStockAndReOrderingModel>() { new TypeProductLowStockAndReOrderingModel() { Id = 0, StoreId = 0, LowStockLevelQuantity = 0 } };
            if (obj.ProductLowStockAndReOrdering == null) { obj.ProductLowStockAndReOrdering = ProductLowStockAndReOrdering; }

            List<TypeProductImageModel>? ProductImage = new List<TypeProductImageModel>() { new TypeProductImageModel() { Id = 0, ImageName = "", Base64String = ""} };
            if (obj.ProductImage == null) { obj.ProductImage = ProductImage; }

     
            List<TypeProductColor>? ProductColor = new List<TypeProductColor>() { new TypeProductColor() { Id = 0, ColorInfoId=0} };
            if (obj.ProductColor == null) { obj.ProductColor = ProductColor; }

            List<TypeProductSize>? ProductSize = new List<TypeProductSize>() { new TypeProductSize() { SizeName = "", Size = 0 } };
            if (obj.ProductSize == null) { obj.ProductSize = ProductSize; }

            List<TypeProductUsageArea>? ProductUsageArea = new List<TypeProductUsageArea>() { new TypeProductUsageArea() {  UsageAreaParameterId =0 } };
            if (obj.ProductUsageArea == null) { obj.ProductUsageArea = ProductUsageArea; }

            List<TypeProductFeatures>? ProductFeatures = new List<TypeProductFeatures>() { new TypeProductFeatures() {   Headings ="", Description="" } };
            if (obj.ProductFeatures == null) { obj.ProductFeatures = ProductFeatures; }

            List<TypeProductFeatures>? ProductOffer = new List<TypeProductFeatures>() { new TypeProductFeatures() { Headings = "", Description = "" } };
            if (obj.ProductOffer == null) { obj.ProductOffer = ProductOffer; } 

            List<TypeProductFeatures>? ProductDescriptionList = new List<TypeProductFeatures>() { new TypeProductFeatures() { Headings = "", Description = "" } };
            if (obj.ProductDescriptionList == null) { obj.ProductDescriptionList = ProductDescriptionList; }

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@ProductName", obj.ProductName));
            parameterList.Add(new SqlParameter("@ProductNameBangla", obj.ProductNameBangla));
            parameterList.Add(new SqlParameter("@Barcode", obj.Barcode));
            parameterList.Add(new SqlParameter("@ProductBrandId", obj.ProductBrandId));
            parameterList.Add(new SqlParameter("@IsAddedVolume", obj.IsAddedVolume));
            parameterList.Add(new SqlParameter("@volumeMeasurementId", obj.VolumeMeasurementId));
            parameterList.Add(new SqlParameter("@volumeAmount", obj.VolumeAmount));
            parameterList.Add(new SqlParameter("@MeasurementId", obj.MeasurementId));
            parameterList.Add(new SqlParameter("@Amount", obj.Amount));
            parameterList.Add(new SqlParameter("@ShortDescription", obj.ShortDescription));
            parameterList.Add(new SqlParameter("@ProductDescription", obj.ProductDescription));
            parameterList.Add(new SqlParameter("@ProductCategoryId", obj.ProductCategoryId));
            parameterList.Add(new SqlParameter("@SupplyPrice", obj.SupplyPrice));
            parameterList.Add(new SqlParameter("@VendorPrice", obj.VendorPrice));
            parameterList.Add(new SqlParameter("@VendorSpecialPrice", obj.VendorSpecialPrice));
            parameterList.Add(new SqlParameter("@RetailPrice", obj.RetailPrice));
            parameterList.Add(new SqlParameter("@SpecialPrice", obj.SpecialPrice));
            parameterList.Add(new SqlParameter("@MarkUpPercentage", obj.MarkUpPercentage));
            parameterList.Add(new SqlParameter("@ProductSupplierId", obj.ProductSupplierId));
            parameterList.Add(new SqlParameter("@TrackStockQuantity", obj.TrackStockQuantity));
            parameterList.Add(new SqlParameter("@IsAvailableColor", obj.IsAvailableColor));
            parameterList.Add(new SqlParameter("@AllowColorWhenChoose", obj.AllowColorWhenChoose));
            parameterList.Add(new SqlParameter("@AllowImageWhenChoose", obj.AllowImageWhenChoose));
            parameterList.Add(new SqlParameter("@AllowSizeWhenChoose", obj.AllowSizeWhenChoose));
            parameterList.Add(new SqlParameter("@originCountryId", obj.OriginCountryId));
            parameterList.Add(new SqlParameter("@IsAddedWarranty", obj.IsAddedWarranty));
            parameterList.Add(new SqlParameter("@WarrantyTypeParameterId", obj.WarrantyTypeParameterId));
            parameterList.Add(new SqlParameter("@WarrantyPeriod", obj.WarrantyPeriod));
            parameterList.Add(new SqlParameter("@WarrantyPeriodParameterId", obj.WarrantyPeriodParameterId));
            parameterList.Add(new SqlParameter("@IsAddedUsageArea", obj.IsAddedUsageArea));
            parameterList.Add(new SqlParameter("@IsAddedPackSize", obj.IsAddedPackSize));
            parameterList.Add(new SqlParameter("@PackSizeQty", obj.PackSizeQty));
            parameterList.Add(new SqlParameter("@IsAddedWeight", obj.IsAddedWeight));
            parameterList.Add(new SqlParameter("@Weight", obj.Weight));
            parameterList.Add(new SqlParameter("@WeightMeasurementId", obj.WeightMeasurementId));
            parameterList.Add(new SqlParameter("@TaxId", obj.TaxId));
            parameterList.Add(new SqlParameter("@SearchKeyword", obj.SearchKeyword));
            parameterList.Add(new SqlParameter("@TypeProductSKU", await _dataManager.ListToDataTable(obj.ProductSKU)));
            parameterList.Add(new SqlParameter("@TypeStoreWiseProduct", await _dataManager.ListToDataTable(obj.StoreWiseProductStock)));
            parameterList.Add(new SqlParameter("@TypeProductLowStockAndReOrdering", await _dataManager.ListToDataTable(obj.ProductLowStockAndReOrdering)));
            parameterList.Add(new SqlParameter("@TypeProductImage", await _dataManager.ListToDataTable(obj.ProductImage)));
            parameterList.Add(new SqlParameter("@TypeProductColor", await _dataManager.ListToDataTable(obj.ProductColor)));
            parameterList.Add(new SqlParameter("@TypeProductSize", await _dataManager.ListToDataTable(obj.ProductSize)));
            parameterList.Add(new SqlParameter("@TypeProductUsageArea", await _dataManager.ListToDataTable(obj.ProductUsageArea)));
            parameterList.Add(new SqlParameter("@TypeProductFeatures", await _dataManager.ListToDataTable(obj.ProductFeatures)));
            parameterList.Add(new SqlParameter("@TypeProductOffer", await _dataManager.ListToDataTable(obj.ProductOffer)));
            parameterList.Add(new SqlParameter("@TypeProductDescription", await _dataManager.ListToDataTable(obj.ProductDescriptionList)));

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveProduct(string? userName, StoreProductModel obj)
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateProduct(string? userName, StoreProductModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> DeleteProduct(string? userName, int? Id)
        {
            StoreProductModel obj = new StoreProductModel()
            {
                Id = Id,
                ProductName = "Dummy"
            };
            var result = await SetData(Delete, userName, obj);
            return result;
        }
        public async Task<ResultObject> SaveProductOffer(string  userName, PrductOfferModel obj)  
        { 
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", 1));
            parameterList.Add(new SqlParameter("@UserName", userName));  
            parameterList.Add(new SqlParameter("@ProductId", obj.ProductId));
            parameterList.Add(new SqlParameter("@OfferPrice", obj.OfferPrice));
            parameterList.Add(new SqlParameter("@OfferStartDate", obj.OfferStartDate));
            parameterList.Add(new SqlParameter("@OfferEndDate", obj.OfferEndDate));   

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP("StoreDB.DBO.spSetProductOffer", parameters);
            return (objResult);
        }
        private async Task<ResultObject> SetProductInactivation(int? SaveOption, string? userName, ProductInactiveModel obj)  
        {
            ResultObject objResult = new ResultObject();
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.ProductId));
            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            objResult = await _dataManager.SaveDataBySP("StoreDB.DBO.spSetProductInActive", parameters);
            return (objResult);
        }
        private int setInactiveProduct = 1, 
                     setActiveProduct = 2;  
        public async Task<ResultObject> InActiveProduct(string? userName, ProductInactiveModel obj)
        {
            var result = await SetProductInactivation(setInactiveProduct, userName, obj);
            return result;
        }
        public async Task<ResultObject> ActiveProduct(string? userName, ProductInactiveModel obj) 
        {
            var result = await SetProductInactivation(setActiveProduct, userName, obj);  
            return result;
        }
        #endregion
        #region Product Management Date:27/08/2023
        private int LoadLowStock = 2,
                   LoadOutofStock = 3;
        private async Task<List<T>> GetManagementData<T>(int loadoption, string? UserName, int? ProductCategoryId, string? SearchText) where T : class, new()
                {

                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@LoadOption", loadoption));
                    parameterList.Add(new SqlParameter("@UserName", UserName)); 
                    parameterList.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
                    parameterList.Add(new SqlParameter("@SearchKey", SearchText));
                  
                    SqlParameter[] parameters = parameterList.ToArray();
                    var lst = await _dataManager.ReturnListBySP2<T>("StoreDB.dbo.spGetProductManagement", parameters);

                    return new(lst);
                }
        public async Task<List<StoreProductViewModel>> GetLowStock(string? UserName, int? ProductCategoryId, string? SearchText)
        {
            var result = await GetManagementData<StoreProductViewModel>(LoadLowStock, UserName, ProductCategoryId, SearchText);
            return result;
        }
        public async Task<List<StoreProductViewModel>> GetOutofStock(string? UserName, int? ProductCategoryId, string? SearchText)
        {
            var result = await GetManagementData<StoreProductViewModel>(LoadOutofStock, UserName, ProductCategoryId, SearchText);  
            return result;
        }
        #endregion
    }
}
