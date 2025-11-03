using System;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Application.Features.Dashboard.DTOs
{
    public class LoanSnapshotDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public LoanStatus Status { get; set; }
    }
}
