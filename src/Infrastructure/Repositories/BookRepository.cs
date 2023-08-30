using BookStoreApi.Application.Common.Interfaces.Contexts;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using BookStoreApi.Domain.Entities.Book;
using MongoDB.Driver;

namespace BookStoreApi.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IBookStoreContext _context;
    public BookRepository(IBookStoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<BookDocument>> GetBooks()
    {
        return await _context
            .Books
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<BookDocument> GetBook(string id)
    {
        return await _context
            .Books
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<BookDocument>> GetBookByName(string title)
    {
        var filter = Builders<BookDocument>.Filter.ElemMatch(p => p.Title, title);

        return await _context
            .Books
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookDocument>> GetBookByCategory(string categoryName)
    {
        var filter = Builders<BookDocument>.Filter.Eq(p => p.Category, categoryName);

        return await _context
            .Books
            .Find(filter)
            .ToListAsync();
    }

    public async Task CreateBook(BookDocument product)
    {
        await _context.Books.InsertOneAsync(product);
    }

    public async Task<bool> UpdateBook(BookDocument product)
    {
        var updateResult = await _context
            .Books
            .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

        return updateResult.IsAcknowledged
               && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteBook(string id)
    {
       var filter = Builders<BookDocument>.Filter.Eq(p => p.Id, id);

        var deleteResult = await _context
            .Books
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
               && deleteResult.DeletedCount > 0;
    }
}