using MediatR;
namespace LibraryManagement.Application.Features.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
