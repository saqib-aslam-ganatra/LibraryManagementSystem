using LibraryManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;

        public DateTime JoinedDate { get; set; }

        // Navigation property: a member can borrow many books
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
