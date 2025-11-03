using LibraryManagement.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{entity}")]
        public async Task<IActionResult> DownloadAsync(string entity, [FromQuery] string format = "csv", CancellationToken cancellationToken = default)
        {
            var report = await _reportService.GenerateAsync(entity, format, cancellationToken);
            return File(report.Content, report.ContentType, report.FileName);
        }
    }
}
