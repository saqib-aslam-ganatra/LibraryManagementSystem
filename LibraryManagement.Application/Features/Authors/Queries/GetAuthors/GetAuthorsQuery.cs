using LibraryManagement.Application.Features.Author.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Author.Queries.GetAuthors
{
    public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>> { }
}
