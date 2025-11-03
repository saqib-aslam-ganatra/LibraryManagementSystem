using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Features.Members.Queries.GetMembers
{
    public class GetMembersQuery : IRequest<IEnumerable<MemberDto>> { }
}
