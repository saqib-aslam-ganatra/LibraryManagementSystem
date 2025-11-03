using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Application.Features.Books.DTOs;
using LibraryManagement.Application.Features.Author.DTOs;
using LibraryManagement.Application.Features.Members.DTOs;
using LibraryManagement.Application.Features.Loans.DTOs;
using LibraryManagement.Application.Features.Books.Commands.CreateBook;
using LibraryManagement.Application.Features.Books.Commands.UpdateBook;
using LibraryManagement.Application.Features.Author.Commands.CreateAuthor;
using LibraryManagement.Application.Features.Author.Commands.UpdateAuthor;

namespace LibraryManagement.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Book mappings
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();

            // Author mappings
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<CreateAuthorCommand, Author>();
            CreateMap<UpdateAuthorCommand, Author>();

            // Member mappings
            CreateMap<Member, MemberDto>().ReverseMap();

            // Loan mappings
            CreateMap<Loan, LoanDto>().ReverseMap();

            

        }
    }
}
