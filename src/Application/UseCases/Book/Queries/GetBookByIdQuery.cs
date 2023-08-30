using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace BookStoreApi.Application.UseCases.Book.Queries;

public record GetBookByIdQuery(string Id) : IRequest<ApplicationResponse<BookResponse>>;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ApplicationResponse<BookResponse>>
{
    public GetBookByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ApplicationResponse<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new ApplicationResponse<BookResponse>();
        var getBook = await _bookRepository.GetBook(request.Id);

        if (getBook == null)
            result.AddError("NotFound", $"Book id {request.Id} was not found!");
        else
            result.SetContentValue(getBook.Adapt<BookResponse>());
        
        return result;
    }
}