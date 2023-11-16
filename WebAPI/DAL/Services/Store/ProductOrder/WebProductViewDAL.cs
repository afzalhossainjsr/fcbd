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
using Model.Store.Product;
using Model.Store.ProductOrder;

namespace DAL.Services.Store.ProductOrder
{
    public class WebProductViewDAL : IWebProductViewDAL
    {
        private readonly IDataManager _dataManager;
        public WebProductViewDAL(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetWebProduct";
        readonly int LoadProductCategory  = 1,
	                 LoadProduct  = 2;

        #region Load  Data Date: 19/03/2023 
        private async Task<DataSet> GetData(int loadoption, string? UserName,
            int? ProductCategoryId, int? ProductId, string? SearchText, int? ProductBrandId,
            string? ProductColor, string? ProductSize, int? PriceStart, int? PriceEnd, int? WarrantyTypeParameterId,
            int? WarrantyPeriod,int? PriceDisplayOrder,int? OriginCountryId, int? page = 1, int? PagingSize = 10)  
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@ProductCategoryId", ProductCategoryId));
            parameterList.Add(new SqlParameter("@ProductId", ProductId));
            parameterList.Add(new SqlParameter("@SearchText", SearchText));
            parameterList.Add(new SqlParameter("@ProductBrandId", ProductBrandId));
      
            parameterList.Add(new SqlParameter("@ProductColor", ProductColor));
            parameterList.Add(new SqlParameter("@ProductSize", ProductSize));
            parameterList.Add(new SqlParameter("@PriceStart", PriceStart));
            parameterList.Add(new SqlParameter("@PriceEnd", PriceEnd));
            parameterList.Add(new SqlParameter("@WarrantyTypeParameterId", WarrantyTypeParameterId));
            parameterList.Add(new SqlParameter("@WarrantyPeriod", WarrantyPeriod));
            parameterList.Add(new SqlParameter("@PriceDisplayOrder", PriceDisplayOrder));
            parameterList.Add(new SqlParameter("@OriginCountryId", OriginCountryId));
            
            parameterList.Add(new SqlParameter("@OffsetValue", (page - 1) * PagingSize));
            parameterList.Add(new SqlParameter("@PagingSize", PagingSize));

            SqlParameter[] parameters = parameterList.ToArray();
            var tablelst = await _dataManager.ReturnDataSet(getSp, parameters);  

            return (tablelst);
        }
        public async Task<StoreProductWebModel> GetProduct(string? UserName,
            int? ProductCategoryId, int? ProductId, string? SearchText, int? ProductBrandId,
            string? ProductColor, string? ProductSize, int? PriceStart, int? PriceEnd, int? WarrantyTypeParameterId,
            int? WarrantyPeriod, int? PriceDisplayOrder,int? OriginCountryId, int? page=1, int PagingSize = 10)
        {
            StoreProductWebModel model = new StoreProductWebModel();
            var ds = await GetData(LoadProduct, UserName,ProductCategoryId,  ProductId,  SearchText,  ProductBrandId,
           ProductColor,  ProductSize,  PriceStart,  PriceEnd,  WarrantyTypeParameterId,
             WarrantyPeriod, PriceDisplayOrder, OriginCountryId, page, PagingSize);
            if (ds != null && ds.Tables.Count>0)
            {
                model.ProductList = await _dataManager.ReturnDataTableToList<WebProductViewModel>(ds.Tables[0]);

                if(model.ProductList.Count>0)
                {
                    var rows = (await _dataManager.ReturnDataTableToList<TotalRowsModel>(ds.Tables[10])).ToList()[0].TotalRows;
                    model.pager = new Pager(rows ?? 0, page, PagingSize); 
                }

                model.ProductBrandList = await _dataManager.ReturnDataTableToList<WebProductBrandViewModel>(ds.Tables[1]);
                model.ProductCategoryList = await _dataManager.ReturnDataTableToList<WebProductCategoryViewModel>(ds.Tables[2]);
                model.MinPriceMaxPriceRange = (await _dataManager.ReturnDataTableToList<WebMinPriceMaxPriceViewModel>(ds.Tables[3])).ToList()[0];
                model.OriginCountryList = await _dataManager.ReturnDataTableToList<WebCountryViewModel>(ds.Tables[4]);
                model.WarrantyTypeList = await _dataManager.ReturnDataTableToList<WebWarrantyTypeViewModel>(ds.Tables[5]);
                model.WarrantyPeriodList = await _dataManager.ReturnDataTableToList<WebWarrantyPeriodViewModel>(ds.Tables[6]);
                model.ColorList = await _dataManager.ReturnDataTableToList<WebColorViewModel>(ds.Tables[7]);
                model.SizeList = await _dataManager.ReturnDataTableToList<WebSizeViewModel>(ds.Tables[8]);
                model.UsageAreaList = await _dataManager.ReturnDataTableToList<WebUsageAreaViewModel>(ds.Tables[9]);
            }
           return (model);   
        }
        public async Task<List<ProductCategoryViewModel>> GetProductCategory(string? UserName)
        {
            var ds = await GetData(LoadProductCategory, UserName, null, null, null, null,  null, null, null, null, null, null, null,null,null);
            var lst = new List<ProductCategoryViewModel>();
            if (ds != null && ds.Tables.Count > 0)
            {
                lst = await _dataManager.ReturnDataTableToList<ProductCategoryViewModel>(ds.Tables[0]);
            }
         
            var featuresCategory = lst.Where(x => x.ParentId == 0).ToList();
            List<ProductCategoryViewModel> TreeList = new List<ProductCategoryViewModel>();
            for (var i = 0; i < featuresCategory.Count; i++)
            {
                ProductCategoryViewModel obj = new ProductCategoryViewModel();
                obj.Id = featuresCategory[i].Id;
                obj.CategoryName = featuresCategory[i].CategoryName;
                obj.CategoryImage = featuresCategory[i].CategoryImage;
                obj.ParentId = featuresCategory[i].ParentId;
                obj.IsAddedProduct = featuresCategory[i].IsAddedProduct;
                obj.TotalProduct = featuresCategory[i].TotalProduct;
                obj.CategoryParentTree = featuresCategory[i].CategoryParentTree;
                obj.CategoryParentTreeBn = featuresCategory[i].CategoryParentTreeBn;
                obj.ProductCategoryList = CategoryList(lst, featuresCategory[i].Id).ToList();
                TreeList.Add(obj);
            }
            return (TreeList);
        }
        private List<ProductCategoryViewModel> CategoryList(List<ProductCategoryViewModel> Listobj, int? ParentId)
        {
            var Lst = Listobj.Where(x => x.ParentId == ParentId)
                .Select(s => new ProductCategoryViewModel
                {
                    Id = s.Id,
                    CategoryName = s.CategoryName,
                    CategoryImage = s.CategoryImage,
                    ParentId = s.ParentId,
                    StageNumber = s.StageNumber,
                    CategoryParentTree = s.CategoryParentTree,
                    IsAddedProduct = s.IsAddedProduct,
                    TotalProduct = s.TotalProduct,
                    CategoryParentTreeBn = s.CategoryParentTreeBn,
                    ProductCategoryList = CategoryList(Listobj, s.Id)
                }).ToList();
            return Lst;
        }
        public async Task<List<WebProductViewModel>> GetOfferProduct() 
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", 4));
            SqlParameter[] parameters = parameterList.ToArray();
            var tablelst = await _dataManager.ReturnListBySP2<WebProductViewModel>(getSp, parameters); 
            return (tablelst);    
        }
        #endregion Load Data  
    }
}
