using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderingController : ControllerBase
{
    [HttpPost("orders")]
    public IActionResult CreateOrder([FromBody] object request)
    {
        return Ok(new { orderId = Guid.NewGuid() });
    }

    [HttpGet("orders/{id:guid}")]
    public IActionResult GetOrder(Guid id)
    {
        return Ok(new { id });
    }

    [HttpPut("orders/{id:guid}/status")]
    public IActionResult UpdateOrderStatus(Guid id, [FromBody] object request)
    {
        return Ok(new { id, updated = true });
    }

    [HttpPost("orders/{id:guid}/cancel")]
    public IActionResult CancelOrder(Guid id)
    {
        return Ok(new { id, cancelled = true });
    }
}
