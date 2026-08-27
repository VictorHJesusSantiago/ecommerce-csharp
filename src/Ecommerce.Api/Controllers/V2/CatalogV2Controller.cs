using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class CatalogV2Controller : ControllerBase
{
    [HttpGet("products")]
    public IActionResult GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new { version = "v2", page, pageSize, includes = "thumbnails" });
    }
}
