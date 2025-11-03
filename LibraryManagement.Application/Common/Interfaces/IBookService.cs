using LibraryManagement.Application.Features.Books.DTOs;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(BookDto dto);
        Task<bool> UpdateAsync(BookDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
