using MediatR;
using Microsoft.Extensions.Logging;

namespace BookStoreApi.Application.Common.Behaviours;

public class ExecutionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public ExecutionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    private readonly ILogger<TRequest> _logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("BookStoreApi {Name}: {@Request}", requestName, request);

            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "BookStoreApi {Name} error: Unhandled Exception for Request / {@Request}", requestName, request);

            throw;
        }
    }
}