using System.Net;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result.HasError<NotFoundError>())
        {
            return Problem(
                detail: result.Errors.First().Message,
                statusCode: StatusCodes.Status404NotFound,
                title: HttpStatusCode.NotFound.ToString());
        }
        return Ok(result.Value);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var result = await _service.GetByNameAsync(name);
        if (result.HasError<NotFoundError>())
        {
            return Problem(
                detail: result.Errors.First().Message,
                statusCode: StatusCodes.Status404NotFound,
                title: HttpStatusCode.NotFound.ToString());
        }
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category request)
    {
        var result = await _service.CreateAsync(request);

        if (result.HasError<ConflictError>())
        {
            return Problem(
                detail: result.Errors.First().Message,
                statusCode: StatusCodes.Status409Conflict,
                title: HttpStatusCode.Conflict.ToString());
        }

        return CreatedAtAction(nameof(GetById), new { Id = result.Value.Id }, result.Value);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> PostBulk([FromBody] IEnumerable<Category> request)
    {
        var results = await _service.CreateBulkAsync(request);
        return Ok(results);
    }
}