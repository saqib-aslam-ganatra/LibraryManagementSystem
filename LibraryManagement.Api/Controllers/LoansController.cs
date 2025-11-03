using LibraryManagement.Api.Models.Loans;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Loans.DTOs;
using LibraryManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllAsync()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<LoanDto>> GetByIdAsync(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan is null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<LoanDto>> CreateAsync([FromBody] LoanCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var dto = new LoanDto
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                BorrowedAt = DateTime.UtcNow,
                LoanDate = DateTime.UtcNow,
                DueDate = request.DueDate,
                Status = LoanStatus.Borrowed
            };

            var created = await _loanService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] LoanUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest(new { Message = "Route id and payload id do not match." });
            }

            var dto = new LoanDto
            {
                Id = request.Id,
                BookId = request.BookId,
                MemberId = request.MemberId,
                DueDate = request.DueDate,
                ReturnDate = request.ReturnDate,
                Status = request.Status
            };

            var updated = await _loanService.UpdateAsync(dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _loanService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
