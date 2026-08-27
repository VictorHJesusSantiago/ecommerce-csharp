using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProductListDto>>), 200)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductSearchRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.SearchProductsAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.GetByIdAsync(id, cancellationToken);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpGet("slug/{slug}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductBySlug(string slug, CancellationToken cancellationToken)
    {
        var result = await _productService.GetBySlugAsync(slug, cancellationToken);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateProductAsync(request, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return CreatedAtAction(nameof(GetProduct), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateProductAsync(id, request, cancellationToken);
        if (!result.Success)
        {
            if (result.StatusCode == 404) return NotFound(result);
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteProductAsync(id, cancellationToken);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost("{id:guid}/publish")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ApiResponse), 200)]
    public async Task<IActionResult> PublishProduct(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.PublishProductAsync(id, cancellationToken);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("featured")]
    [ProducesResponseType(typeof(ApiResponse<List<ProductDto>>), 200)]
    public async Task<IActionResult> GetFeaturedProducts([FromQuery] int count = 10, CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetFeaturedProductsAsync(count, cancellationToken);
        return Ok(result);
    }

    [HttpGet("new-arrivals")]
    [ProducesResponseType(typeof(ApiResponse<List<ProductDto>>), 200)]
    public async Task<IActionResult> GetNewArrivals([FromQuery] int count = 10, CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetNewArrivalsAsync(count, cancellationToken);
        return Ok(result);
    }

    [HttpGet("best-sellers")]
    [ProducesResponseType(typeof(ApiResponse<List<ProductDto>>), 200)]
    public async Task<IActionResult> GetBestSellers([FromQuery] int count = 10, CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetBestSellersAsync(count, cancellationToken);
        return Ok(result);
    }
}
