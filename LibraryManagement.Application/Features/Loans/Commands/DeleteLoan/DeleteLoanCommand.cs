using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.DeleteLoan
{
    public class DeleteLoanCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
