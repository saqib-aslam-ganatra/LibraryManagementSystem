using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Models.Loans
{
    public class LoanCreateRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
