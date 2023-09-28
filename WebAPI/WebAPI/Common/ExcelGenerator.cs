using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Common
{
    public static class ExcelGenerator 
    {
        public static IActionResult GenerateExcel01(DataTable dt, string title, string secondTitle)
        {
            if (dt.Rows.Count > 0)
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "Worksheet Report");
                    ws.PageSetup.FitToPages(1, 1);
                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    ws.PageSetup.PaperSize = XLPaperSize.A4Paper;

                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    ws.Tables.FirstOrDefault().Theme = XLTableTheme.TableStyleLight20;

                    ws.Row(1).InsertRowsAbove(1);
                    ws.Row(1).InsertRowsBelow(1);

                    var lastColumnIndex = dt.Columns.Count - 1;
                    var lastColumnName = GetExcelColumnName(lastColumnIndex);

                    var tileMargeRange = $"A1:{lastColumnName}1";
                    var SecondtileMargeRange = $"A2:{lastColumnName}2";

                    ws.Range(tileMargeRange).Row(1).Merge();
                    ws.Range(SecondtileMargeRange).Row(1).Merge();

                    ws.Cell(1, 1).Style.Font.FontSize = 16;
                    ws.Cell(1, 1).Style.Font.FontColor = XLColor.Black;
                    ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(1, 1).Style.Font.SetBold();
                    ws.Cell(1, 1).Value = title;
                    ws.Cell(2, 1).Style.Font.FontSize = 14;
                    ws.Cell(2, 1).Style.Font.FontColor = XLColor.Black;
                    ws.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(2, 1).Style.Font.SetBold();
                    ws.Cell(2, 1).Value = secondTitle;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var fileBytes = ms.ToArray();
                        var fileName = $"Worksheet-{DateTime.Now.ToString("dd-MM-yyyy-HHmmss")}.xlsx";

                        // Return the Excel file as a downloadable attachment
                        return new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            FileDownloadName = fileName
                        };
                    }
                }
            }
            else
            {
                return new ContentResult
                {
                    StatusCode = StatusCodes.Status204NoContent, // Use an appropriate status code
                    Content = "No data to export."
                };
            }
        }
        /*
        [HttpGet("ExportDailyProductOrder")]
        public async Task<IActionResult> GetDailyProductOrder(string? FromDate, string? ToDate, int? StoreId)  
        {
            var UserName = User?.Identity?.Name;
            var dt = await  _IStoreReportDAL.GetOrderQtyDataTable(UserName, FromDate, ToDate, StoreId);
            dt.Columns.Remove("Image");
            dt.Columns.Remove("RetailPrice");
            var title = "Daily Product Order";
            var secondTitle = "" + FromDate + " - " + ToDate + "";
            //return GenerateExcel(dt, title, secondTitle);  
            return ExcelGenerator.GenerateExcel01(dt, title, secondTitle);
        }
         
         */

        private static string GetExcelColumnName(int columnIndex)
        {
            int dividend = columnIndex + 1;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                int remainder = (dividend - 1) % 26;
                columnName = (char)(65 + remainder) + columnName;
                dividend = (dividend - remainder) / 26;
            }

            return columnName;
        }
    }
}