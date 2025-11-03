using LibraryManagement.Application.Features.Reports.DTOs;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IReportService
    {
        Task<ReportFileDto> GenerateAsync(string entity, string format, CancellationToken cancellationToken = default);
    }
}
