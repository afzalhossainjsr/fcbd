
using DAL.Repository.Store.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Store.Product;


namespace WebAPI.Controllers.Store.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreProductDAL _iStoreProductDAL;
        private readonly string rootPath;

        public StoreProductController(IStoreProductDAL iUserRegisterDAL, IWebHostEnvironment
            hostingEnvironment) 
        {
            this._iStoreProductDAL = iUserRegisterDAL;
            rootPath = hostingEnvironment.ContentRootPath;

        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProduct(int? Id, string? SearchText, int? ProductCategoryId, int? ProductBrandId, int? ProductSupplierId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.GetProduct(UserName, Id, SearchText, ProductCategoryId, ProductBrandId, ProductSupplierId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductInitialData")] 
        public async Task<IActionResult> GetProductInitialData(int? Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.GetProductInitialData(UserName, Id);
            return new JsonResult(lst);
        }

        [HttpPost]
        [Route("SaveProduct")] 
        public async Task<IActionResult> SaveProduct(StoreProductModel obj)
        {
            var UserName = User?.Identity?.Name;
            /*Save Image First*/
            if (obj.ProductImage?.Count > 0) 
            {
                for (int i = 0; i < obj.ProductImage.Count; i++)
                {
                    var img = obj.ProductImage[i];
                    if (img.Base64String?.Length > 105)
                    {
                        var imageResult = SaveFile(img.Base64String);
                        if (imageResult != "-1")
                            obj.ProductImage[i].ImageName = imageResult;
                        else
                            obj.ProductImage[i].ImageName = null;
                    }
                }
            }
            /*then saved */
            var lst = await _iStoreProductDAL.SaveProduct(UserName, obj);
            if ((int.Parse(lst.ResultID ?? "0") < 0))
            {
                /*delete saved image from folder*/
                string spath = rootPath + "/Image/store/Product/";
                if (obj.ProductImage?.Count > 0)
                {
                    for (int i = 0; i < obj.ProductImage.Count; i++)
                    {
                        string path200 = spath + obj.ProductImage[i].ImageName;
                        string path100 = spath + "small/" + obj.ProductImage[i].ImageName;
                        if (System.IO.File.Exists(path200))
                            System.IO.File.Delete(path200);
                        if (System.IO.File.Exists(path100))
                            System.IO.File.Delete(path100);
                    }
                }
            }
            return new JsonResult(lst);
        }



        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(StoreProductModel obj)
        {
            var UserName = User?.Identity?.Name;
            /*Save Image First*/
            if (obj.ProductImage?.Count > 0)
            {
                for (int i = 0; i < obj.ProductImage.Count; i++)
                {
                    var img = obj.ProductImage[i];
                    if (img.Base64String?.Length >105) 
                    {
                        var imageResult = SaveFile(img.Base64String);
                        if (imageResult != "-1")
                            obj.ProductImage[i].ImageName = imageResult;
                        else
                            obj.ProductImage[i].ImageName = null;
                    }
                }
            }
            /*then saved */
              string spath = rootPath + "/Image/store/product/";
            var lst = await _iStoreProductDAL.UpdateProduct(UserName, obj);
            if ((int.Parse(lst.ResultID ?? "0") < 0))
            {
                /*delete saved image from folder*/

                if (obj.ProductImage?.Count > 0)
                {
                    for (int i = 0; i < obj.ProductImage.Count; i++)
                    {
                      
                       if (obj.ProductImage[i].Base64String?.Length > 105)
                        {
                            string path200 = spath + obj.ProductImage[i].ImageName;
                            string path100 = spath + "small/" + obj.ProductImage[i].ImageName;
                            if (System.IO.File.Exists(path200))
                                System.IO.File.Delete(path200);
                            if (System.IO.File.Exists(path100))
                                System.IO.File.Delete(path100);
                        }
                    }
                }
            }

            if (obj.DeletedProductList?.Count > 0) 
            {
                for (int i = 0; i < obj.DeletedProductList.Count; i++)
                {
                    string path200 = spath + obj.DeletedProductList[i].ImageName;
                    string path100 = spath + "small/" + obj.DeletedProductList[i].ImageName;
                    if (System.IO.File.Exists(path200))
                        System.IO.File.Delete(path200);
                    if (System.IO.File.Exists(path100))
                        System.IO.File.Delete(path100);
                }
            }


            return new JsonResult(lst);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.DeleteProduct(UserName, Id);
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
                string spath = rootPath + "/Image/store/product/";
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
        [HttpPost]
        [Route("SaveProductOffer")]
        public async Task<IActionResult> SaveProductOffer(PrductOfferModel obj) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.SaveProductOffer(UserName, obj); 
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("InActiveProduct")] 
        public async Task<IActionResult> InActiveProduct(ProductInactiveModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.InActiveProduct(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpPost]
        [Route("ActiveProduct")] 
        public async Task<IActionResult> ActiveProduct(ProductInactiveModel obj)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.ActiveProduct(UserName, obj);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetLowStock")]
        public async Task<IActionResult> GetLowStock(int? ProductCategoryId, string? SearchText)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.GetLowStock(UserName, ProductCategoryId, SearchText);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetOutofStock")] 
        public async Task<IActionResult> GetOutofStock(int? ProductCategoryId, string? SearchText)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _iStoreProductDAL.GetOutofStock(UserName, ProductCategoryId, SearchText); 
            return new JsonResult(lst);
        }
        //[HttpGet]
        //[Route("GetProductCatalog")] 
        //public async Task<IActionResult> GetProductCatalog(int? ProductCategoryId)  
        //{
        //    var UserName = User?.Identity?.Name;
        //    var lst = await _iStoreProductDAL.GetProduct(UserName, null, null, ProductCategoryId, null, null);
        //    string htmlContent = GetHtmlCode(lst); 
        //    var pdfBytes = PdfGenerator2.GeneratePdfA4(htmlContent); 
        //    return File(pdfBytes, "application/pdf", "ProductCatalog.pdf");
        //}

//        private string GetHtmlCode(List<StoreProductViewModel> lst) 
//        {
//            lst = lst.Where(x=>x.IsActive==1).OrderBy(obj => obj.ProductCategoryId).ToList();
//            string table = @"<!DOCTYPE html>
//<html lang=""en"">
//<head>
//    <meta charset=""UTF-8"">
//    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">

//   <link href='https://fonts.maateen.me/kalpurush/font.css' rel='stylesheet'>
//<style>  
//body {
//    font-family: '', Arial, sans-serif !important;
//}
//                table {
//                    page-break-inside: auto;
//                width:100%;
//                 font-family: '', sans-serif;
//                } 

//                tr {
//                    page-break-inside: avoid; 
//                    page-break-after: auto;
//                } 

//                thead {
//                    display: table-header-group;
//                } 

//                tfoot {
//                    display: table-footer-group;
//                }

//                .product-container {
//                        width: 33.33%;
//                        float: left;
//                        padding: 1px 1px 1px 1px;
//                        box-sizing: border-box;
//                    }

//                    .product {
//                        /*border: 1px solid #ccc;*/
//                        text-align: center;
//                        padding: 3px;
//                    }

//                        .product img {
//                            max-width: 100%;
//                            height:100%;
//                        }

//                    .product-name {
//                        margin-top: 3px;
//                    }

//                    .price {
//                        margin-top: 3px;
//                    }

//                    .special-price {
//                        color: red;
//                    }

//                    .flex-container {
//                        /*display: flex;*/
//                       // border:1px solid#ccc;
//                        overflow:hidden;
//                       // border-radius:5px;
//                    }

//                    .image-container {
//                        width:30%; /* Take up 33% of the container width */
//                        padding: 3px; /* Optional: Add padding for spacing */
//                        float:left;
//                    }

//                    .info-container {
//                        width: 64%; /* Take up 67% of the container width */
//                        padding: 3px; /* Optional: Add padding for spacing */
//                        float:left;
//                float:left;
//                    }

//                    /* Optional styling for the image */
//                    img {
//                        width: 100%;
//                        height: auto;
//                    }
//                    </style></head>
//<body>";

//            table += @"<table>
//                <thead>
//                    <tr>
//                        <td>
//                        <div style=""border-bottom:1px solid #2a2828;text-align:center;margin:8px;"">
//                            <span style= ""font-size:25px;"" >
//                               <b> Looks Bangladesh.</b>
//                             </span><br/>
//                            <span>
//                                <b> Address:</b> 63 / C Asad Avenue, Mohammadpur, Dhaka -1207.<br/>
//                                <b>Contact:  </b> 01329-673161  <br/>
//                            </span>
//                          </div>
//                       </td>
//                   </tr>
//                </thead>
//                <tbody>";
//                    var counter = 1;
//                    foreach (var product in lst)
//                    {

//                        if (counter % 3 == 1 )
//                        {
//                            table += @"<tr><td>";
//                        }

//                        table += $@"<div class=""product-container""> 
//                        <div class=""flex-container"">
//                            <div class=""image-container"">
//                                <img src=""https://partnersapi.looks.com.bd/Image/Store/product/small/{product.ImageList?.Split(',')[0]}"" alt=""{product.ProductName}"">
//                            </div>
//                            <div class=""info-container"">
//                                <span style=""font-size: 14px;font-weight:600;color: #25211e; font-family: 'Kalpurush'"">{product.ProductNameBangla}; {product.BrandName}
//                                </span><br>
                            
//                                <b style=""font-size: 12px; font-weight: bold;""> Code : {product.Id} </b>;  
//                                <span style=""font-size: 18px; font-weight: bold; color: #d78905;font-family: 'Kalpurush'"">৳{ CommonMethods.ConvertToBanglaNumber(product.VendorPrice.ToString()) }</span> 
                             
//                            </div>
//                        </div>
//                    </div>";

//                        if (counter % 3 == 0 || lst.Count == counter)
//                        {
//                            table += @"</td></tr>";
//                        }
//                        counter++;
//                    }
//                    table += @"</tbody>
//                  <tfoot>
//                <tr>
//                    <td style=""text-align:right;font-size:16px;padding-right:8px;"">for more details visit: <u>  looks.com.bd </u></td>
//                </tr>
//                </tfoot>
//                </table>
//</body>
//</html>";

//            return table;
//        }
    }
}
