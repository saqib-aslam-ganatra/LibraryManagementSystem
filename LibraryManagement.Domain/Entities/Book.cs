using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string ISBN { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public string? Description { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public decimal ReplacementCost { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation property: A book can have multiple loans
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
