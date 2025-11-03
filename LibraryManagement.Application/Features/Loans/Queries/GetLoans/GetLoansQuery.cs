using LibraryManagement.Application.Features.Loans.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoans
{
    public class GetLoansQuery : IRequest<IEnumerable<LoanDto>>
    {
    }
}
