using LibraryManagement.Application.Features.Loans.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoanById
{
    public class GetLoanByIdQuery : IRequest<LoanDto?>
    {
        public int Id { get; set; }
    }
}
