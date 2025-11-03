using LibraryManagement.Application.Features.Loans.DTOs;
using LibraryManagement.Domain.Enums;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.UpdateLoan
{
    public class UpdateLoanCommand : IRequest<LoanDto?>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
    }
}
