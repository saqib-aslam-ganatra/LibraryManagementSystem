using LibraryManagement.Application.Features.Authors.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>> { }
}
