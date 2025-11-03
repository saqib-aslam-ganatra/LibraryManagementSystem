using LibraryManagement.Application.Features.Authors.DTOs;
using MediatR;
namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthorById
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
