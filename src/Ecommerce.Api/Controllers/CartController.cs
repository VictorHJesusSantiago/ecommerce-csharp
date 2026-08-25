using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCart()
    {
        return Ok(new { items = new object[] { } });
    }

    [HttpPost("items")]
    public IActionResult AddToCart([FromBody] object request)
    {
        return Ok(new { added = true });
    }

    [HttpPut("items/{itemId:guid}")]
    public IActionResult UpdateCartItem(Guid itemId, [FromBody] object request)
    {
        return Ok(new { itemId, updated = true });
    }

    [HttpDelete("items/{itemId:guid}")]
    public IActionResult RemoveFromCart(Guid itemId)
    {
        return Ok(new { itemId, removed = true });
    }

    [HttpPost("clear")]
    public IActionResult ClearCart()
    {
        return Ok(new { cleared = true });
    }
}
