using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class CatalogV1Controller : ControllerBase
{
    [HttpGet("products")]
    public IActionResult GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new { version = "v1", page, pageSize });
    }
}
