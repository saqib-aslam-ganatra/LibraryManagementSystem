using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.Features.Author.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
    }
}
