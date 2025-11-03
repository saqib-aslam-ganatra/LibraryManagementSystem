using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Dashboard.DTOs;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryManagement.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
        {
            var totalBooks = await _context.Books.CountAsync(cancellationToken);
            var availableBooks = await _context.Books.CountAsync(book => book.IsAvailable, cancellationToken);
            var totalAuthors = await _context.Authors.CountAsync(cancellationToken);
            var totalMembers = await _context.Members.CountAsync(cancellationToken);
            var activeLoans = await _context.Loans.CountAsync(loan => loan.Status == LoanStatus.Borrowed, cancellationToken);
            var overdueLoans = await _context.Loans.CountAsync(loan => loan.Status == LoanStatus.Overdue ||
                                                                      (loan.Status == LoanStatus.Borrowed && loan.DueDate < DateTime.UtcNow), cancellationToken);

            var recentLoans = await _context.Loans
                .AsNoTracking()
                .Include(l => l.Book)
                .Include(l => l.Member)
                .OrderByDescending(l => l.LoanDate)
                .Take(5)
                .Select(l => new LoanSnapshotDto
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    MemberName = l.Member.FullName,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    Status = l.Status
                })
                .ToListAsync(cancellationToken);

            return new DashboardSummaryDto
            {
                TotalBooks = totalBooks,
                AvailableBooks = availableBooks,
                TotalAuthors = totalAuthors,
                TotalMembers = totalMembers,
                ActiveLoans = activeLoans,
                OverdueLoans = overdueLoans,
                RecentLoans = recentLoans
            };
        }
    }
}
