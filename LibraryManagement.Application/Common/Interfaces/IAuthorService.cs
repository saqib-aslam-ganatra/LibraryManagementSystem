using LibraryManagement.Application.Features.Author.DTOs;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
        Task<bool> UpdateAuthorAsync(AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
