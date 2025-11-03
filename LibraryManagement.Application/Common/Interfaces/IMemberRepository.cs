using LibraryManagement.Domain.Entities;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member> AddAsync(Member entity);        
    Task<Member> UpdateAsync(Member entity);     
    Task<bool> DeleteAsync(int id);             
}
