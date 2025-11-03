using System;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Application.Features.Loans.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }

        public string? BookTitle { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        public string? MemberName { get; set; }

        public DateTime BorrowedAt { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public LoanStatus Status { get; set; }
    }
}
