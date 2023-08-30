using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using BookStoreApi.Application.Common.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace BookStoreApi.Application.UseCases.Book.Queries;

public record GetBooksQuery(string? Category) : IRequest<ApplicationResponse<IEnumerable<BookResponse>>>;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, ApplicationResponse<IEnumerable<BookResponse>>>
{
    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    private readonly IBookRepository _bookRepository;
    
    public async Task<ApplicationResponse<IEnumerable<BookResponse>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var result = new ApplicationResponse<IEnumerable<BookResponse>>();

        if (string.IsNullOrEmpty(request.Category))
        {
            var getBooks = await _bookRepository.GetBooks();
            result.SetContentValue(getBooks.Adapt<IEnumerable<BookResponse>>());
        }
        else
        {
            var getBooksByCategory = await _bookRepository.GetBookByCategory(request.Category);
            result.SetContentValue(getBooksByCategory.Adapt<IEnumerable<BookResponse>>());
        }

        return result;
    }
}