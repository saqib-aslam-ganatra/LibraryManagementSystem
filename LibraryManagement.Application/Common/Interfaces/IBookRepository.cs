using LibraryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllWithAuthorsAsync();
        Task<Book?> GetByIdWithAuthorAsync(int id);
    }
}
