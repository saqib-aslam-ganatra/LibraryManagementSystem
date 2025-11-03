using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Loans.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Infrastructure.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> GetAllAsync()
        {
            var loans = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<LoanDto?> GetByIdAsync(int id)
        {
            var loan = await _repository.GetByIdAsync(id);
            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> CreateAsync(LoanDto dto)
        {
            var entity = _mapper.Map<Loan>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<LoanDto>(created);
        }

        public async Task<LoanDto> UpdateAsync(LoanDto dto)
        {
            var entity = _mapper.Map<Loan>(dto);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<LoanDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
