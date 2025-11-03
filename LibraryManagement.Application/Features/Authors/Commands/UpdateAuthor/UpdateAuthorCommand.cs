using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
    }
}
