using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
            => await _context.Members.AsNoTracking().ToListAsync();

        public async Task<Member?> GetByIdAsync(int id)
            => await _context.Members.FindAsync(id);

        public async Task<Member> AddAsync(Member entity)
        {
            _context.Members.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Member> UpdateAsync(Member entity)
        {
            _context.Members.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Members.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Members.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
