using BookStoreApi.Application.Common.Interfaces.Contexts;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using BookStoreApi.Domain.Entities.Book;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BookStoreApi.Infrastructure.Contexts;

public class BookStoreContext : IBookStoreContext
{
    public BookStoreContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
        var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);

        Books = database.GetCollection<BookDocument>(configuration.GetSection("DatabaseSettings:BookCollectionName").Value);
    }
    
    public IMongoCollection<BookDocument> Books { get; }
}