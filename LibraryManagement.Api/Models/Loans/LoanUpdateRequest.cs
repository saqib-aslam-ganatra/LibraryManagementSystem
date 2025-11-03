using System;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Api.Models.Loans
{
    public class LoanUpdateRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; }
    }
}
