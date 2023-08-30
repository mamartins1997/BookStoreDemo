using BookStoreApi.Domain.Entities.Book;
using MongoDB.Driver;

namespace BookStoreApi.Application.Common.Interfaces.Contexts;

public interface IBookStoreContext
{
    IMongoCollection<BookDocument> Books { get; }
}