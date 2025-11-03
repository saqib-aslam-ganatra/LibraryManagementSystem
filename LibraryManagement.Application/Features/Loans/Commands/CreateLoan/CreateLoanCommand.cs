using LibraryManagement.Application.Features.Loans.DTOs;
using LibraryManagement.Domain.Enums;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<LoanDto>
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime? BorrowedAt { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Borrowed;
    }
}
