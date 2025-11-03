using LibraryManagement.Application.Features.Author.DTOs;
using MediatR;
namespace LibraryManagement.Application.Features.Author.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<AuthorDto?>
    {
        public int Id { get; set; }
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
