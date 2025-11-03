using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Models.Books
{
    public class BookCreateRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ReplacementCost { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
