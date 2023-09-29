using System.Data;
using Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult test()
        {
            return  new JsonResult("Test");
        }
        [HttpGet]
        [Route("generatePdfA4")]
        public IActionResult GeneratePdf()
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
        [HttpGet("ExportExcel")] 
        public IActionResult ExportExcel()
        {
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
        private FacebookUserInfo GetFaceBookUser(string _accessToken) 
        {
            FacebookUserInfo userinfo = new FacebookUserInfo();
            var client = new FacebookClient(_accessToken);
            try
            {
                var result = client.Get("/me?fields=id,email");
                var responseObj = JObject.Parse(result?.ToString());
                userinfo = new FacebookUserInfo()
                {
                    Id = responseObj["id"]?.ToString(),
                    Email = responseObj["email"]?.ToString()
                };
            }
            catch (Exception ex)
            {
                userinfo.Email = ex.Message.ToString();
                return userinfo;
            }

            return userinfo;
        }
    }
}
