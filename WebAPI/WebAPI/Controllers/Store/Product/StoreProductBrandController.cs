using DAL.Repository.Store.Product;

using Microsoft.AspNetCore.Mvc;
using Model.Store.Product;
using System.Drawing;



namespace WebAPI.Controllers.Store.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductBrandController : ControllerBase
    {
        private readonly IStoreProductBrandDAL _iStoreProductBrandDAL;
        private readonly string rootPath;

        public StoreProductBrandController(IStoreProductBrandDAL iUserRegisterDAL, IWebHostEnvironment
            hostingEnvironment)
        {
            this._iStoreProductBrandDAL = iUserRegisterDAL;
            rootPath = hostingEnvironment.ContentRootPath;

        }
        [HttpGet]
        [Route("GetProductBrand")]
        public async Task<IActionResult> GetProductBrand()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductBrandDAL.GetProductBrand(UserName);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("SaveProductBrand")]
        public async Task<IActionResult> SaveProductBrand(StoreProductBrandModel obj)
        {
            var UserName = User?.Identity?.Name;

            if (obj.BrandImageBase64String != null)
            {
                var imagename = SaveFile(obj.BrandImageBase64String);
                if (imagename != "-1")
                    obj.BrandImage = imagename;

            }
            var lst = await _iStoreProductBrandDAL.SaveProductBrand(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("UpdateProductBrand")]
        public async Task<IActionResult> UpdateProductBrand(StoreProductBrandModel obj)
        {
            var UserName = User?.Identity?.Name;
            var pastImagename = obj.BrandImage;
            if (obj.BrandImageBase64String != null && obj.BrandImageBase64String.Length > 10)
            {
                var imagename = SaveFile(obj.BrandImageBase64String);
                if (imagename != "-1")
                {
                    obj.BrandImage = imagename;
                }
                if (pastImagename != null && imagename != "-1")
                {
                    /*delete  image from folder*/
                    string spath = rootPath + "/Image/store/ProductBrand/";
                    string path400 = spath + pastImagename;
                    string path100 = spath + "small/" + pastImagename;
                    if (System.IO.File.Exists(path400))
                        System.IO.File.Delete(path400);
                    if (System.IO.File.Exists(path100))
                        System.IO.File.Delete(path100);
                }
            }

            var lst = await _iStoreProductBrandDAL.UpdateProductBrand(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpDelete]
        [Route("DeleteProductBrand")]
        public async Task<IActionResult> DeleteProductBrand(int? Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductBrandDAL.DeleteProductBrand(UserName, Id);
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
                string spath = rootPath + "/Image/store/ProductBrand/";
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
