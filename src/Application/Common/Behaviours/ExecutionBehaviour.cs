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

            var result = await next();

            var executionResult = new { UserInfo = new { UserName = "marcelo", UserId = Guid.NewGuid() }, Request = request, Response = result };
            _logger.LogInformation("{Name}: {@Request}", requestName, executionResult);

            return result;
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            var teste = new { UserInfo = new { UserName = "marcelo", UserId = Guid.NewGuid() }, PayloadInfo = request };

            _logger.LogError(ex, "{Name}: {@Request}", requestName, teste);

            throw;
        }
    }
}