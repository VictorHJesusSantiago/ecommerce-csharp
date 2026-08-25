using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingController : ControllerBase
{
    [HttpPost("rates")]
    public IActionResult GetShippingRates([FromBody] object request)
    {
        return Ok(new { rates = new object[] { } });
    }

    [HttpGet("track/{trackingNumber}")]
    public IActionResult TrackShipment(string trackingNumber)
    {
        return Ok(new { trackingNumber, status = "in-transit", events = new object[] { } });
    }

    [HttpPost("estimate")]
    public IActionResult EstimateShipping([FromBody] object request)
    {
        return Ok(new { estimatedCost = 0m, estimatedDays = 3 });
    }
}
