using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Services
{
    public class VersionedService : IVersionedService
    {
        public int GetV1() => 1;

        public string GetV2() => "2";

        public IActionResult GetV3()
        {
            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell("A1").Value = "Hello";
                worksheet.Cell("B1").Value = "World!";

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);

                stream.Position = 0;

                // Return the Excel file as a FileStreamResult
                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "example.xlsx"
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult($"An error occurred: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }
    }
}
