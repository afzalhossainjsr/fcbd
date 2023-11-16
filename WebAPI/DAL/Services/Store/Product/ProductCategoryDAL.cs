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
    public class StoreProductCategoryDAL : IStoreProductCategoryDAL
    {
        private readonly IDataManager _dataManager;
        public StoreProductCategoryDAL(IDataManager dataManager) 
        {
            _dataManager = dataManager;
        }
        private readonly string getSp = @"StoreDB.dbo.spGetProductCategory";
        private readonly string setSp = "StoreDB.dbo.spSetProductCategory"; 

        readonly int LoadProductCategory = 1; 

        #region Load  Data Date: 26/02/2023 
        private async Task<List<T>> GetData<T>(int loadoption, string? UserName)
            where T : class, new()
        {
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@LoadOption", loadoption));
            parameterList.Add(new SqlParameter("@UserName", UserName));
            SqlParameter[] parameters = parameterList.ToArray();
            var lst = await _dataManager.ReturnListBySP2<T>(getSp, parameters);

            return new(lst);
        }
        public async Task<List<ProductCategoryViewModel>> GetProductCategory(string? UserName)
        {
            var lst = await GetData<ProductCategoryViewModel>(LoadProductCategory, UserName);
            var featuresCategory = lst.Where(x => x.ParentId == 0).ToList();
            List<ProductCategoryViewModel> TreeList = new List<ProductCategoryViewModel>(); 
            for (var i =0; i < featuresCategory.Count; i++)
            {
                ProductCategoryViewModel obj = new ProductCategoryViewModel();
                obj.Id = featuresCategory[i].Id;
                obj.CategoryName = featuresCategory[i].CategoryName;
                obj.CategoryImage = featuresCategory[i].CategoryImage;
                obj.StageNumber  = featuresCategory[i].StageNumber;
                obj.ParentId = featuresCategory[i].ParentId;
                obj.IsAddedProduct = featuresCategory[i].IsAddedProduct;
                obj.TotalProduct = featuresCategory[i].TotalProduct;    
                obj.CategoryParentTree = featuresCategory[i].CategoryParentTree;
                obj.ProductCategoryList = CategoryList(lst, featuresCategory[i].Id).ToList();
                TreeList.Add(obj);
            }
            return (TreeList); 
        }

        private List<ProductCategoryViewModel>  CategoryList(List<ProductCategoryViewModel> Listobj, int? ParentId ) 
        {
            var Lst = Listobj.Where(x => x.ParentId== ParentId)
                .Select(s => new ProductCategoryViewModel 
                { 
                    Id = s.Id,
                    CategoryName= s.CategoryName,
                    CategoryImage= s.CategoryImage,
                    ParentId = s.ParentId,
                    StageNumber = s.StageNumber,
                    CategoryParentTree =   s.CategoryParentTree,
                    IsAddedProduct = s.IsAddedProduct,
                    TotalProduct = s.TotalProduct,
                    ProductCategoryList = CategoryList( Listobj, s.Id )
                }).ToList(); 
            return Lst; 
        }

        #endregion Load Data 

        #region Save  Date: 26/02/2023 
        private readonly int Save = 1;
        private readonly int Update = 2;
        private readonly int Delete = 3;

        private async Task<ResultObject> SetData(int? SaveOption, string? userName, StoreProductCategoryModel obj)
        {
           
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            parameterList.Add(new SqlParameter("@UserName", userName));
            parameterList.Add(new SqlParameter("@Id", obj.Id));
            parameterList.Add(new SqlParameter("@CategoryName", obj.CategoryName));
            parameterList.Add(new SqlParameter("@ParentId", obj.ParentId));
            parameterList.Add(new SqlParameter("@CategoryImage", obj.CategoryImage));

            parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            SqlParameter[] parameters = parameterList.ToArray();
            var objResult = await _dataManager.SaveDataBySP(setSp, parameters);
            return (objResult);
        }
        public async Task<ResultObject> SaveProductCategory(string? userName, StoreProductCategoryModel obj)
        {
            var result = await SetData(Save, userName, obj);
            return result;
        }
        public async Task<ResultObject> UpdateProductCategory(string? userName, StoreProductCategoryModel obj)
        {
            var result = await SetData(Update, userName, obj);
            return result;
        }
        public async Task<ResultObject> DeleteProductCategory(string? userName, int? Id)
        {
            StoreProductCategoryModel obj = new StoreProductCategoryModel() { Id = Id };
            var result = await SetData(Delete, userName, obj);
            return result;
        }
        #endregion 
    }
}
