using System;

namespace LibraryManagement.Application.Features.Dashboard.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalMembers { get; set; }
        public int ActiveLoans { get; set; }
        public int OverdueLoans { get; set; }
        public IReadOnlyCollection<LoanSnapshotDto> RecentLoans { get; set; } = Array.Empty<LoanSnapshotDto>();
    }
}
