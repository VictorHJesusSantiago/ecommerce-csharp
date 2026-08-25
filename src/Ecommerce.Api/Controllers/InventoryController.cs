using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    [HttpGet("stock/{productId:guid}")]
    public IActionResult GetStock(Guid productId)
    {
        return Ok(new { productId, quantity = 0 });
    }

    [HttpPost("stock/check")]
    public IActionResult CheckStock([FromBody] object request)
    {
        return Ok(new { available = true });
    }

    [HttpGet("warehouses")]
    public IActionResult GetWarehouses()
    {
        return Ok(new { warehouses = new object[] { } });
    }

    [HttpGet("warehouses/{id:guid}")]
    public IActionResult GetWarehouse(Guid id)
    {
        return Ok(new { id });
    }

    [HttpPost("stock/adjust")]
    public IActionResult AdjustStock([FromBody] object request)
    {
        return Ok(new { adjusted = true });
    }

    [HttpPost("stock/transfer")]
    public IActionResult TransferStock([FromBody] object request)
    {
        return Ok(new { transferred = true });
    }
}
