namespace LibraryManagement.Application.Features.Books.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
