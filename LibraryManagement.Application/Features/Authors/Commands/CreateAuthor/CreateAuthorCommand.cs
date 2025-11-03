using LibraryManagement.Application.Features.Author.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Author.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<AuthorDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
    }
}
