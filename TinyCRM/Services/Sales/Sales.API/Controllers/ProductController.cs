using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.CQRS.Commands.ProductCommands.Requests;
using Sales.Application.CQRS.Queries.ProductQueries.Requests;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Products.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<ProductSummaryDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingProductsDto dto)
    {
        var products = await _mediator.Send(new FilterAndPagingProductsQuery(dto));

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    [Authorize(Policy = TinyCrmPermissions.Products.Read)]
    public async Task<ActionResult<ProductDetailDto>> GetByIdAsync(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Products.Create)]
    public async Task<ActionResult<ProductDetailDto>> CreateAsync([FromBody] CreateOrEditProductDto dto)
    {
        var product = await _mediator.Send(new CreateProductCommand(dto));

        return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Products.Edit)]
    public async Task<ActionResult<ProductDetailDto>> EditAsync([FromBody] CreateOrEditProductDto dto, Guid id)
    {
        var product = await _mediator.Send(new EditProductCommand(id, dto));

        return product;
    }

    [HttpDelete]
    [Authorize(Policy = TinyCrmPermissions.Products.Delete)]
    public async Task<ActionResult> DeleteAsync(DeleteManyProductsDto dto)
    {
        await _mediator.Send(new DeleteManyProductsCommand(dto));

        return NoContent();
    }
}