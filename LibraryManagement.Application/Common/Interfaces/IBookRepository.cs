using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        // You can add custom book-specific methods here later if needed
    }
}
