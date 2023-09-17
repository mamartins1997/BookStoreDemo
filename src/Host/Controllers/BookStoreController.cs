using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using BookStoreApi.Application.UseCases.Book.Commands;
using BookStoreApi.Application.UseCases.Book.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Host.Controllers;

[Route("/book-store")]
[AllowAnonymous]
[ApiController]
public class BookStoreController : ApiControllerBase
{
    private readonly ILogger<BookStoreController> _logger;
    public BookStoreController(ISender mediator, ILogger<BookStoreController> logger) : base(mediator)
    {
        this._logger = logger;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<IEnumerable<BookResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBooks([FromQuery] string? category)
    {
        try
        {
            var result = await _mediator.Send(new GetBooksQuery(category));
            if (result.HasError())
                return BadRequest(result);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Get Books Error");
            return BadRequest("Get Books Error");
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<BookResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBook([FromRoute] string id)
    {
        try
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            
            if (result.HasError() && result.Errors!.ContainsKey("NotFound"))
                return NotFound(result);
            
            if (result.HasError())
                return BadRequest(result);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Get Book Error");
            return BadRequest("Get Book Error");
        }
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<BookResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] BookPayload payload)
    {
        try
        {
            var result = await _mediator.Send(new InsertBookCommand(payload));
            if (result.HasError())
                return BadRequest(result);
            return CreatedAtAction(null, result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Create Book Error");
            return BadRequest("Create book error");
        }
    }
    
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<BookResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook([FromBody] BookPayload payload, [FromRoute] string id)
    {
        try
        {
            var result = await _mediator.Send(new UpdateBookCommand(payload,id));
            
            if (result.HasError() && result.Errors!.ContainsKey("NotFound"))
                return NotFound(result);
            
            if (result.HasError())
                return BadRequest(result);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Update Book Error");
            return BadRequest("Update Book Error");
        }
    }
    
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBook([FromRoute] string id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));
            
            if (result.HasError())
                return BadRequest(result);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Delete Book Error");
            return BadRequest("Delete Book Error");
        }
    }
}