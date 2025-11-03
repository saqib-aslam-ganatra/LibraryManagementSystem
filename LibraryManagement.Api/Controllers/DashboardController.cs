using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Dashboard.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryDto>> GetSummaryAsync(CancellationToken cancellationToken)
        {
            var summary = await _dashboardService.GetSummaryAsync(cancellationToken);
            return Ok(summary);
        }
    }
}
