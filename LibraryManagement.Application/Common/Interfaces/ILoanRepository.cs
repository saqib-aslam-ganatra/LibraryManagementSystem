using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        // Add loan-specific queries or methods if needed later
    }
}
