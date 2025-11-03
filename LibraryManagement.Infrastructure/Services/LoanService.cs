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
            return loan is null ? null : _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> CreateAsync(LoanDto dto)
        {
            var entity = _mapper.Map<Loan>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<LoanDto>(created);
        }

        public async Task<bool> UpdateAsync(LoanDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity is null)
            {
                return false;
            }

            entity.BookId = dto.BookId;
            entity.MemberId = dto.MemberId;
            entity.DueDate = dto.DueDate;
            entity.ReturnDate = dto.ReturnDate;
            entity.Status = dto.Status;

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
