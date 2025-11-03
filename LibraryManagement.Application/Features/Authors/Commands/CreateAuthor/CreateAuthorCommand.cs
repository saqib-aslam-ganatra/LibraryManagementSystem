using LibraryManagement.Application.Features.Authors.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<AuthorDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
    }
}
