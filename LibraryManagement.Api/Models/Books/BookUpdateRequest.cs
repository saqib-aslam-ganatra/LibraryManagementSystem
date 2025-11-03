using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Models.Books
{
    public class BookUpdateRequest : BookCreateRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
