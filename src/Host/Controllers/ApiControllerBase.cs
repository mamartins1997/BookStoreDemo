using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Host.Controllers;

[ApiController]
[Route("api")]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    public ISender _mediator;
    public ApiControllerBase(ISender mediator)
    {
        _mediator = mediator;
    }
    
}