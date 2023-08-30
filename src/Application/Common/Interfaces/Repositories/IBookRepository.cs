using BookStoreApi.Domain.Entities.Book;

namespace BookStoreApi.Application.Common.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<BookDocument>> GetBooks();
    Task<BookDocument> GetBook(string id);
    Task<IEnumerable<BookDocument>> GetBookByName(string title);
    Task<IEnumerable<BookDocument>> GetBookByCategory(string categoryName);

    Task CreateBook(BookDocument product);
    Task<bool> UpdateBook(BookDocument product);
    Task<bool> DeleteBook(string id);
}