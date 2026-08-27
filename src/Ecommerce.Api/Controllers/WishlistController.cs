using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase
{
    [HttpGet]
    public IActionResult GetWishlist()
    {
        return Ok(new { items = new object[] { } });
    }

    [HttpPost("items")]
    public IActionResult AddToWishlist([FromBody] object request)
    {
        return Ok(new { added = true });
    }

    [HttpDelete("items/{productId:guid}")]
    public IActionResult RemoveFromWishlist(Guid productId)
    {
        return Ok(new { productId, removed = true });
    }
}
