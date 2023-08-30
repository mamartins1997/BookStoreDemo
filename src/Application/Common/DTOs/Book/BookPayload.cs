namespace BookStoreApi.Application.Common.DTOs.Book;

public class BookPayload
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Author { get; set; }
    public decimal Price { get; set; }
}