using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MarketingController : ControllerBase
{
    [HttpPost("coupons/validate")]
    public IActionResult ValidateCoupon([FromBody] object request)
    {
        return Ok(new { valid = true, discount = 0m });
    }

    [HttpGet("banners")]
    public IActionResult GetBanners([FromQuery] string position = null)
    {
        return Ok(new { banners = new object[] { } });
    }

    [HttpPost("newsletter/subscribe")]
    public IActionResult SubscribeNewsletter([FromBody] object request)
    {
        return Ok(new { subscribed = true });
    }

    [HttpPost("newsletter/unsubscribe")]
    public IActionResult UnsubscribeNewsletter([FromBody] object request)
    {
        return Ok(new { unsubscribed = true });
    }

    [HttpGet("promotions/active")]
    public IActionResult GetActivePromotions()
    {
        return Ok(new { promotions = new object[] { } });
    }
}
