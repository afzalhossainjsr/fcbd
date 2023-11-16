using DAL.Repository.Store.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.Product;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace WebAPI.Controllers.Store.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductCategoryController : ControllerBase
    {
        private readonly IStoreProductCategoryDAL _iStoreProductCategoryDAL;
        private readonly string rootPath;

        public StoreProductCategoryController(IStoreProductCategoryDAL iUserRegisterDAL, IWebHostEnvironment
            hostingEnvironment)  
        {
            this._iStoreProductCategoryDAL = iUserRegisterDAL;
            rootPath = hostingEnvironment.ContentRootPath;

        }
        [HttpGet]
        [Route("GetProductCategory")] 
        public async Task<IActionResult> GetProductCategory()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductCategoryDAL.GetProductCategory(UserName);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveProductCategory")] 
        public async Task<IActionResult> SaveProductCategory(StoreProductCategoryModel obj)
        {
            var UserName = User?.Identity?.Name;

            if (obj.CategoryImageBase64String != null)
            {
                var imagename = SaveFile(obj.CategoryImageBase64String);
                if (imagename != "-1")
                    obj.CategoryImage = imagename;

            }
           var lst = await _iStoreProductCategoryDAL.SaveProductCategory(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateProductCategory")]
        public async Task<IActionResult> UpdateProductCategory(StoreProductCategoryModel obj)
        {
            var UserName = User?.Identity?.Name;
            var pastImagename = obj.CategoryImage;
            if (obj.CategoryImageBase64String != null && obj.CategoryImageBase64String.Length > 10)
            {
                    var imagename = SaveFile(obj.CategoryImageBase64String);
                    if (imagename != "-1")
                    {
                        obj.CategoryImage = imagename;
                    }
                    if (pastImagename != null && imagename != "-1")
                    {
                        /*delete  image from folder*/
                        string spath = rootPath + "/Image/store/productCategory/";
                        string path400 = spath + pastImagename;
                        string path100 = spath + "small/" + pastImagename;
                        if (System.IO.File.Exists(path400))
                            System.IO.File.Delete(path400);
                        if (System.IO.File.Exists(path100))
                            System.IO.File.Delete(path100);
                    }
            }

            var lst = await _iStoreProductCategoryDAL.UpdateProductCategory(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("DeleteProductCategory")]
        public async Task<IActionResult> DeleteProductCategory(int? Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductCategoryDAL.DeleteProductCategory(UserName, Id);
            return new JsonResult(lst);
        }
        private string SaveFile(string Base64String)
        {
            try
            {
                var filename = Guid.NewGuid().ToString();
                var imagename = filename + ".jpg";

                // Convert base 64 string to byte[] 
                byte[] imageBytes = Convert.FromBase64String(Base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                //geting server folder path 
                string spath = rootPath + "/Image/store/productCategory/";
                string path = spath + imagename;
                //Save Image to server folder

                image.Save(path);

                var img = SixLabors.ImageSharp.Image.Load(path);
                img.Mutate(x => x.Resize(400, 400));
                img.Save(path);

                string smallImgPath = spath + "small/" + imagename;

                System.IO.File.Copy(path, smallImgPath);

                var smallImg = SixLabors.ImageSharp.Image.Load(smallImgPath);
                smallImg.Mutate(x => x.Resize(100, 100));
                smallImg.Save(smallImgPath);

                return (imagename);
            }
            catch (Exception)
            {
                return ("-1");
            }
        }
    }
}
