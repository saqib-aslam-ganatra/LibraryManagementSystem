using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Loans.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoans
{
    public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, IEnumerable<LoanDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }
    }
}
