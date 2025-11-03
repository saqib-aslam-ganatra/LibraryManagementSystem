using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> AddAsync(Member entity);
        Task<Member> UpdateAsync(Member entity);
        Task<bool> DeleteAsync(int id);
    }
}
