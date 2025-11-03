using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthorsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book?> GetByIdWithAuthorAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
