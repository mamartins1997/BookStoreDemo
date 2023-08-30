using BookStoreApi.Domain.Entities.Common;

namespace BookStoreApi.Domain.Entities.Book;

public class BookDocument : AuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Author { get; set; }
    public decimal Price { get; set; }
}