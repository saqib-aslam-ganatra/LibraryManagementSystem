using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<Loan> Loans { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
