using System.Data;
using Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Auth;
using Newtonsoft.Json.Linq;
using WebAPI.Auth;
using WebAPI.Common;

namespace WebAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {


        [HttpGet("GetData")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult test()
        {
            var UserName = User?.Identity?.Name;
            var username2 = HttpContext.User?.Identity?.Name;
            return new JsonResult("Test: - "+ username2);
        }
        [HttpGet("GetData2")]
       
        public IActionResult test2() 
        {
            var UserName = User?.Identity?.Name;
            var username2 = HttpContext.User?.Identity?.Name;
            return new JsonResult("Test");
        }

        [HttpGet]
        [Route("generatePdfA4")]
        public IActionResult GeneratePdf()
        {
            var UserName = User?.Identity?.Name;
            var username2 = HttpContext.User?.Identity?.Name;
            string htmlContent = @"
<!DOCTYPE html>
<html>
<head>
    <title>Sample Table PDF</title>
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
        }
        th, td {
            border: 1px solid black;
            padding: 8px;
            text-align: center;
        }
        tr.header-row {
            background-color: #f2f2f2;
        }
     
table { 
    page-break-inside: auto;
} 

tr { 
    page-break-inside: avoid; 
    page-break-after: auto;
} 

thead { 
    display: table-header-group;
} 

tfoot { 
    display: table-footer-group;
}
    </style>
</head>
<body>
    <h1>Sample Table</h1>
    <table>
        <thead>
            <tr class='header-row'>
                <th>Column 1</th>
                <th>Column 2</th>
                <th>Column 3</th>
            </tr>
        </thead>
        <tbody>
  
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
<tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
 <tr>
                <td>Row 1, Column 1</td>
                <td>Row 1, Column 2</td>
                <td>Row 1, Column 3</td>
            </tr>
            <!-- Add more rows as needed -->
        </tbody>
    </table>
</body>
</html>";
            var pdfBytes = PdfGenerator.GeneratePdfA4(htmlContent);
            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
        [HttpGet]
        [Route("GeneratePdfcustom")]
        public IActionResult GeneratePdfcustom()
        {
            string htmlContent = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Sample Table PDF</title>
                    <style>
                        table {
                            width: 100%;
                            border-collapse: collapse;
                        }
                        th, td {
                            border: 1px solid black;
                            padding: 8px;
                            text-align: center;
                        }
                        tr.header-row {
                            background-color: #f2f2f2;
                        }
                    </style>
                </head>
                <body>
                    <h1>Sample Table</h1>
                    <table>
                        <thead>
                            <tr class='header-row'>
                                <th>Column 1</th>
                                <th>Column 2</th>
                                <th>Column 3</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Row 1, Column 1</td>
                                <td>Row 1, Column 2</td>
                                <td>Row 1, Column 3</td>
                            </tr>
                            <!-- Add more rows as needed -->
                        </tbody>
                    </table>
                </body>
                </html>";

            var pdfBytes = PdfGenerator.GeneratePdfCustom(htmlContent);
            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
       
        
        [HttpGet("ExportExcel")] 
        public IActionResult ExportExcel()
        {
            var UserName = User?.Identity?.Name;
            DataTable table = new DataTable("SampleTable");
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Rows.Add(1, "John");
            table.Rows.Add(2, "Jane");
            table.Rows.Add(3, "Doe");
            var title = "Daily Product Order";
            var secondTitle = "1 January, 2023";
            return ExcelGenerator.GenerateExcel01(table, title, secondTitle);
        }
      
    }
}
