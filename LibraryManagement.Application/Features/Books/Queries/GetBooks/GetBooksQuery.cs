using LibraryManagement.Application.Features.Books.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IEnumerable<BookDto>> { }
}
