using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DownloadExcel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var path = @"C:\Users\HP\OneDrive\Desktop\DownloadExcel\Files\SampleExcel.xlsx";
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(path));
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".xls", "application/vnd.ms-excel" },
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            };
        }
    }
}
