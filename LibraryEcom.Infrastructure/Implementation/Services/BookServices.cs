using LibraryEcom.Application.DTOs.Author;
using LibraryEcom.Application.DTOs.Book;
using LibraryEcom.Application.Interfaces.Repositories.Base;
using LibraryEcom.Application.Interfaces.Services;
using LibraryEcom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryEcom.Infrastructure.Implementation.Services;

public class BookServices(IGenericRepository genericRepository) : IBookService
{
    public List<BookDto> GetAll(int pageNumber, int pageSize, out int rowCount, string? search = null)
    {
        var books = genericRepository.GetPagedResult<Book>(pageNumber, pageSize, out rowCount,
                x => string.IsNullOrEmpty(search) || 
                     x.Title.ToLower().Contains(search.ToLower()) || 
                     x.Description.ToLower().Contains(search.ToLower()))
            .ToList();
    
        var bookDtos = new List<BookDto>();
        var bookIds = books.Select(b => b.Id).ToList();
        
        var bookAuthors = genericRepository.Get<BookAuthor>(ba => bookIds.Contains(ba.BookId))
            .Include(ba => ba.Author) 
            .ToList();

        var authorIds = genericRepository.GetById<Author>(bookAuthors.Select(ba => ba.AuthorId).ToList());

        foreach (var book in books)
        {
            var authorsForBook = bookAuthors
                .Where(ba => ba.BookId == book.Id)
                .Select(ba => ba.Author)
                .ToList();

            var authorDtos = authorsForBook.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                BirthDate = a.BirthDate
            }).ToList();

            var bookDto = new BookDto
            {
                Id = book.Id,
                PublisherId = book.PublisherId,
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                BookFormat = book.BookFormat,
                PublicationDate = book.PublicationDate,
                Genre = book.Genre,
                BasePrice = book.BasePrice,
                PageCount = book.PageCount,
                Language = book.Language,
                IsAvailable = book.IsAvailable,
                Discount = book.Discount,
                Authors = authorDtos
            };

            bookDtos.Add(bookDto);
        }

        return bookDtos;
    }

    public List<BookDto> GetAll(string? search = null)
    {
        throw new NotImplementedException();
    }

    public BookDto? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Create(CreateBookDto dto)
    {
        throw new NotImplementedException();
    }

    public void Update(Guid id, UpdateBookDto dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}