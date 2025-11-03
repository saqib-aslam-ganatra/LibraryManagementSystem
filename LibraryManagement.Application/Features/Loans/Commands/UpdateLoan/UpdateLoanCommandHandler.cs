using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Loans.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.UpdateLoan
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, LoanDto?>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public UpdateLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanDto?> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id);
            if (loan is null)
            {
                return null;
            }

            loan.BookId = request.BookId;
            loan.MemberId = request.MemberId;
            loan.LoanDate = request.LoanDate;
            loan.DueDate = request.DueDate;
            loan.ReturnDate = request.ReturnDate;
            loan.Status = request.Status;

            await _loanRepository.UpdateAsync(loan);

            var updatedWithIncludes = await _loanRepository.GetByIdAsync(loan.Id) ?? loan;
            return _mapper.Map<LoanDto>(updatedWithIncludes);
        }
    }
}
