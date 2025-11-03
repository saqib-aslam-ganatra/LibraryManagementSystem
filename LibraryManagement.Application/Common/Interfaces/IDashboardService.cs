using LibraryManagement.Application.Features.Dashboard.DTOs;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default);
    }
}
