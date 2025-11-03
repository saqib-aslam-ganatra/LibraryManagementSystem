using LibraryManagement.Application.Features.Loans.DTOs;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllAsync();
        Task<LoanDto?> GetByIdAsync(int id);
        Task<LoanDto> CreateAsync(LoanDto dto);
        Task<LoanDto> UpdateAsync(LoanDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
