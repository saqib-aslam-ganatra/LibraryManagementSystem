using LibraryManagement.Api.Models.Members;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Members.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllAsync()
        {
            var members = await _memberService.GetAllAsync();
            return Ok(members);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MemberDto>> GetByIdAsync(int id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member is null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> CreateAsync([FromBody] MemberCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var dto = new MemberDto
            {
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim(),
                PhoneNumber = request.PhoneNumber?.Trim() ?? string.Empty,
                Address = request.Address?.Trim() ?? string.Empty,
                MembershipDate = DateTime.UtcNow,
                JoinedDate = DateTime.UtcNow
            };

            var created = await _memberService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] MemberUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest(new { Message = "Route id and payload id do not match." });
            }

            var dto = new MemberDto
            {
                Id = request.Id,
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim(),
                PhoneNumber = request.PhoneNumber?.Trim() ?? string.Empty,
                Address = request.Address?.Trim() ?? string.Empty
            };

            var updated = await _memberService.UpdateAsync(dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _memberService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
