using LibraryManagement.Application.Features.Members.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllAsync();
        Task<MemberDto?> GetByIdAsync(int id);
        Task<MemberDto> CreateAsync(MemberDto dto);
        Task<MemberDto> UpdateAsync(MemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
