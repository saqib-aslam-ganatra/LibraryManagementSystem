using LibraryManagement.Api.Models.Books;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Books.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllAsync()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookDto>> GetByIdAsync(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateAsync([FromBody] BookCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var dto = MapToDto(request);
            var created = await _bookService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] BookUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest(new { Message = "Route id and payload id do not match." });
            }

            var dto = MapToDto(request);
            dto.Id = request.Id;

            var updated = await _bookService.UpdateAsync(dto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static BookDto MapToDto(BookCreateRequest request)
        {
            var availableCopies = request.AvailableCopies;
            if (request.TotalCopies > 0 && (availableCopies <= 0 || availableCopies > request.TotalCopies))
            {
                availableCopies = request.TotalCopies;
            }

            return new BookDto
            {
                Title = request.Title.Trim(),
                ISBN = request.ISBN.Trim(),
                AuthorId = request.AuthorId,
                Description = request.Description,
                TotalCopies = request.TotalCopies,
                AvailableCopies = availableCopies,
                ReplacementCost = request.ReplacementCost,
                IsAvailable = request.IsAvailable
            };
        }
    }
}
