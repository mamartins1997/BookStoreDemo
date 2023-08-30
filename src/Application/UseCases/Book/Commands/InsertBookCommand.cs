using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using BookStoreApi.Domain.Entities.Book;
using Mapster;
using MediatR;

namespace BookStoreApi.Application.UseCases.Book.Commands;

public record InsertBookCommand(BookPayload Book) : IRequest<ApplicationResponse<BookResponse>>;

public class InsertBookCommandHandler : IRequestHandler<InsertBookCommand, ApplicationResponse<BookResponse>>
{
    public InsertBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ApplicationResponse<BookResponse>> Handle(InsertBookCommand request, CancellationToken cancellationToken)
    {
        var result = new ApplicationResponse<BookResponse>();
        //TODO Validation

        var newBook = request.Book.Adapt<BookDocument>();
        newBook.Created = DateTime.Now;
        await _bookRepository.CreateBook(newBook);
        
        result.SetContentValue(newBook.Adapt<BookResponse>());
        return result;
    }
    
}