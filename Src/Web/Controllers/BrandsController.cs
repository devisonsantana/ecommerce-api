using System.Net;
using Ecommerce.Application.Common.Queries;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Errors;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;


[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _service;

    public BrandsController(IBrandService service)
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

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var result = await _service.GetBySlugAsync(slug);
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
    public async Task<IActionResult> GetAll([FromQuery] BrandQuery query)
    {
        var brands = await _service.GetAllAsync(query);
        return Ok(brands);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BrandCreateRequest request)
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
    public async Task<IActionResult> PostBulk(
        [FromBody] IEnumerable<BrandCreateRequest> request)
    {
        var results = await _service.CreateBulkAsync(request);
        return Ok(results);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(
        [FromRoute] long id, [FromBody] BrandUpdateRequest brandRequest)
    {
        var result = await _service.UpdateAsync(id, brandRequest);

        if (result.IsFailed)
        {
            if (result.HasError<NotFoundError>())
            {
                return Problem(
                    detail: result.Errors.First().Message,
                    statusCode: StatusCodes.Status404NotFound,
                    title: HttpStatusCode.NotFound.ToString());
            }

            if (result.HasError<ConflictError>())
            {
                return Problem(
                    detail: result.Errors.First().Message,
                    statusCode: StatusCodes.Status409Conflict,
                    title: HttpStatusCode.Conflict.ToString());
            }
        }

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var result = await _service.DeleteAsync(id);

        if (result.HasError<NotFoundError>())
        {
            return Problem(
                detail: result.Errors.First().Message,
                statusCode: StatusCodes.Status404NotFound,
                title: HttpStatusCode.NotFound.ToString());
        }

        return NoContent();
    }
}