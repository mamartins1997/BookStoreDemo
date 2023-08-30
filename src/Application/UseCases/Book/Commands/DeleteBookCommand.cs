using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using MediatR;

namespace BookStoreApi.Application.UseCases.Book.Commands;

public record DeleteBookCommand(string Id) : IRequest<ApplicationResponse<bool>>;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ApplicationResponse<bool>>
{
    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ApplicationResponse<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var result = new ApplicationResponse<bool>();

        await _bookRepository.DeleteBook(request.Id);
        
        result.SetContentValue(true);
        return result;
    }
}