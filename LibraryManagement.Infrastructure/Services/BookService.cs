using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Books.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _repository.GetAllWithAuthorsAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdWithAuthorAsync(id);
            return book is null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateAsync(BookDto dto)
        {
            var entity = _mapper.Map<Book>(dto);
            if (entity.AvailableCopies == 0 && entity.TotalCopies > 0)
            {
                entity.AvailableCopies = entity.TotalCopies;
            }
            entity.IsAvailable = dto.IsAvailable && entity.AvailableCopies > 0;

            var created = await _repository.AddAsync(entity);
            return _mapper.Map<BookDto>(created);
        }

        public async Task<bool> UpdateAsync(BookDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing is null)
            {
                return false;
            }

            existing.Title = dto.Title;
            existing.ISBN = dto.ISBN;
            existing.AuthorId = dto.AuthorId;
            existing.Description = dto.Description;
            existing.TotalCopies = dto.TotalCopies;

            var availableCopies = dto.AvailableCopies;
            if (dto.TotalCopies > 0 && (availableCopies <= 0 || availableCopies > dto.TotalCopies))
            {
                availableCopies = dto.TotalCopies;
            }

            existing.AvailableCopies = availableCopies;
            existing.ReplacementCost = dto.ReplacementCost;
            existing.IsAvailable = dto.IsAvailable && availableCopies > 0;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
