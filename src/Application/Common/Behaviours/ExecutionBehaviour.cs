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
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "BookStoreApi Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}