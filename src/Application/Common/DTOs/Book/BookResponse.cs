﻿namespace BookStoreApi.Application.Common.DTOs.Book.Response;

public class BookResponse
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Author { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
}