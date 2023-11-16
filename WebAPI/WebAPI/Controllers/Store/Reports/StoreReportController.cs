using System.Data;
using System.Drawing.Imaging;
using System.Net;

using ClosedXML.Excel;
using DAL.Repository.Store.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Jpeg;
using WebAPI.Common;

namespace WebAPI.Controllers.Store.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreReportController : ControllerBase
    {
        private readonly IStoreReportDAL _IStoreReportDAL;
        public StoreReportController(IStoreReportDAL IStoreReportDAL)
        {
            this._IStoreReportDAL = IStoreReportDAL; 
        }
        [HttpGet]
        [Route("GetDayWiseProductOrder")] 
        public async Task<IActionResult> GetDayWiseProductOrder( string? FromDate, string? ToDate, int? StoreId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetDayWiseProductOrder( UserName,  FromDate, ToDate, StoreId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetDayWiseProductDelivery")] 
        public async Task<IActionResult> GetDayWiseProductDelivery(string? FromDate, string? ToDate, int? StoreId) 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetDayWiseProductDelivery(UserName, FromDate, ToDate, StoreId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetProductWiseOrderQty")]
        public async Task<IActionResult> GetProductWiseOrderQty(string? FromDate, string? ToDate, int? StoreId)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetProductWiseOrderQty(UserName, FromDate, ToDate, StoreId);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetMonthWiseProductSale")]
        public async Task<IActionResult> GetMonthWiseProductSale()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetMonthWiseProductSale(UserName);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetMonthWiseSpecificProductSale")] 
        public async Task<IActionResult> GetMonthWiseSpecificProductSale()
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetMonthWiseSpecificProductSale(UserName);
            return new JsonResult(lst);
        }
        [HttpGet]
        [Route("GetCurrentMonthSalesSummary")] 
        public async Task<IActionResult> GetCurrentMonthSalesSummary() 
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetCurrentMonthSalesSummary(UserName); 
            return new JsonResult(lst);
        }

        [HttpGet("ExportDailyProductOrder")]
        public async Task<IActionResult> GetDailyProductOrder(string? FromDate, string? ToDate, int? StoreId)  
        {
            var UserName = User?.Identity?.Name;
            var dt = await  _IStoreReportDAL.GetOrderQtyDataTable(UserName, FromDate, ToDate, StoreId);
            dt.Columns.Remove("Image");
            dt.Columns.Remove("RetailPrice");
            var title = "Daily Product Order";
            var secondTitle = "" + FromDate + " - " + ToDate + "";
            return ExcelGenerator.GenerateExcel01(dt, title, secondTitle); 
        }

        [HttpGet]
        [Route("GetProductStockByStore")]
        public async Task<IActionResult> GetProductStockByStore(int?StoreId = 1)
        {
            var UserName = User?.Identity?.Name;
            var lst = await _IStoreReportDAL.GetProductStockByStore(UserName, StoreId); 
            return new JsonResult(lst);
        }


        //private IActionResult GenerateExcel(DataTable dt,string title, string secondTitle  ) 
        //{
        //    var index = (dt.Columns.Count-1);
        //    Alphabet letter = (Alphabet)index;
        //    var tileMargeRange = "A1:"+ letter + "1";
        //    var SecondtileMargeRange = "A2:" + letter + "2";
        //    if (dt.Rows.Count > 0)
        //    {
        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            var ws = wb.Worksheets.Add(dt, "Worksheet Report");
        //            ws.PageSetup.FitToPages(1, 1);
        //            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
        //            ws.PageSetup.PaperSize = XLPaperSize.A4Paper;

        //            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
        //            ws.Tables.FirstOrDefault().Theme = XLTableTheme.TableStyleLight20; 

        //            ws.Row(1).InsertRowsAbove(1);
        //            ws.Row(1).InsertRowsBelow(1);

        //            ws.Range(tileMargeRange).Row(1).Merge();
        //            ws.Range(SecondtileMargeRange).Row(1).Merge();

        //            ws.Cell(1, 1).Style.Font.FontSize = 16;
        //            ws.Cell(1, 1).Style.Font.FontColor = XLColor.Black;
        //            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //            ws.Cell(1, 1).Style.Font.SetBold();
        //            ws.Cell(1, 1).Value = title;
        //            ws.Cell(2, 1).Style.Font.FontSize = 14;
        //            ws.Cell(2, 1).Style.Font.FontColor = XLColor.Black;
        //            ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //            ws.Cell(2, 1).Style.Font.SetBold();
        //            ws.Cell(2, 1).Value = secondTitle; 

        //            //ws.Range("A3:C3").Row(1).Style.Font.Bold = true;
        //            //ws.Range("A3:C3").Row(1).Style.Fill.BackgroundColor = XLColor.FromHtml("#ccc");

        //            //// Load the image from the URL using SixLabors.ImageSharp
        //            //var imageUrl = "https://partnersapi.looks.com.bd/Image/store/product/small/4dd984be-ba2f-4022-9122-d45e40ef5c9d.jpg";
        //            //var webClient = new WebClient();
        //            //var imageBytes = webClient.DownloadData(imageUrl);

        //            //using (var imageStream = new MemoryStream(imageBytes))
        //            //using (var image = Image.Load(imageStream))
        //            //{

        //            //    image.Mutate(x => x.Resize(new ResizeOptions
        //            //    {
        //            //        Size = new Size(100, 100),
        //            //        Mode = ResizeMode.Max
        //            //    }));
        //            //    var picture = ws.AddPicture(imageStream)
        //            //        .MoveTo(ws.Cell("A11"))  // Set the cell where the image should be placed
        //            //        .WithSize(100, 100);   // Set the size of the image
        //            //}


        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                wb.SaveAs(ms);
        //                ms.Seek(0, SeekOrigin.Begin);
        //                var fileBytes = ms.ToArray();
        //                var fileName = "Worksheet-" + DateTime.Now.ToString("dd-MM-yyyy-HHmmss") + ".xlsx";
        //                // Return the Excel file as a downloadable attachment
        //                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //            }
        //        }
        //    }

        //    return Ok();
        //}

    }
    //public enum Alphabet
    //{
    //    A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
    //    AA, AB,AC, AD, AE, AF, AG, AH, AI, AJ, AK, AL, AM, AN, AO, AP, AQ, AR, AS, AT, AU, AV, AW, AX, AY, AZ,
    //    BA, BB, BC, BD, BE, BF, BG, BH, BI, BJ, BK, BL, BM, BN, BO, BP, BQ, BR, BS, BT, BU, BV, BW, BX, BY, BZ 
    //}
}
