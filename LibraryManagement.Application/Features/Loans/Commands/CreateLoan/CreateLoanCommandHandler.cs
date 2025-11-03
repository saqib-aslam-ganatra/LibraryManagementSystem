using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Loans.DTOs;
using LibraryManagement.Domain.Entities;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                BorrowedAt = request.BorrowedAt ?? DateTime.UtcNow,
                LoanDate = request.LoanDate ?? DateTime.UtcNow,
                DueDate = request.DueDate,
                Status = request.Status
            };

            var created = await _loanRepository.AddAsync(loan);

            var createdWithIncludes = await _loanRepository.GetByIdAsync(created.Id) ?? created;
            return _mapper.Map<LoanDto>(createdWithIncludes);
        }
    }
}
