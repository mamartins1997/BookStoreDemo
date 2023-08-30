using BookStoreApi.Application.Common.DTOs;
using BookStoreApi.Application.Common.DTOs.Book;
using BookStoreApi.Application.Common.DTOs.Book.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Host.Controllers;

[Route("/book-store")]
[AllowAnonymous]
[ApiController]
public class BookStoreController : ApiControllerBase
{
    public BookStoreController(ISender mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<IEnumerable<BookResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetBooks([FromQuery] string category)
    {
        return Ok("OK");
    }
    
    [HttpGet]
    [Route("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<BookResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult GetBook([FromRoute] string id)
    {
        return Ok("OK");
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult CreateBook([FromBody] BookPayload payload)
    {
        return CreatedAtAction(null, "Created");
    }
    
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<BookResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateBook([FromBody] BookPayload payload, [FromRoute] string id)
    {
        return CreatedAtAction(null, "Updated");
    }
    
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApplicationResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult UpdateBook([FromRoute] string id)
    {
        return Ok("Ok");
    }
}