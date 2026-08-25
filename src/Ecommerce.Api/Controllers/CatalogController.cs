using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    [HttpGet("products")]
    public IActionResult GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new { page, pageSize, products = new object[] { } });
    }

    [HttpGet("products/{id:guid}")]
    public IActionResult GetProduct(Guid id)
    {
        return Ok(new { id });
    }

    [HttpGet("categories")]
    public IActionResult GetCategories()
    {
        return Ok(new { categories = new object[] { } });
    }

    [HttpGet("categories/{id:guid}")]
    public IActionResult GetCategory(Guid id)
    {
        return Ok(new { id });
    }

    [HttpGet("brands")]
    public IActionResult GetBrands()
    {
        return Ok(new { brands = new object[] { } });
    }

    [HttpGet("brands/{id:guid}")]
    public IActionResult GetBrand(Guid id)
    {
        return Ok(new { id });
    }
}
