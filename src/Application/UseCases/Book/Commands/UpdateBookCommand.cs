using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace BookStoreApi.Application.UseCases.Book.Commands;

public record UpdateBookCommand(BookPayload Payload, string Id) : IRequest<ApplicationResponse<BookResponse>>;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ApplicationResponse<BookResponse>>
{
    public UpdateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ApplicationResponse<BookResponse>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var result = new ApplicationResponse<BookResponse>();
        
        var getBook = await _bookRepository.GetBook(request.Id);
        if (getBook is null)
        {
            result.AddError("NotFound", $"Book id {request.Id} was not found!");
            return result;
        }

        var bookUpdateData = request.Payload;
        getBook.Title = bookUpdateData.Title ?? getBook.Title;
        getBook.Description = bookUpdateData.Description ?? getBook.Description;
        getBook.Category = bookUpdateData.Category ?? getBook.Category;
        getBook.Author = bookUpdateData.Author ?? getBook.Author;
        getBook.Price = bookUpdateData.Price ?? getBook.Price;
        getBook.SetLastModified();
        
        await _bookRepository.UpdateBook(getBook);
        
        result.SetContentValue(getBook.Adapt<BookResponse>());
        return result;
    }
}