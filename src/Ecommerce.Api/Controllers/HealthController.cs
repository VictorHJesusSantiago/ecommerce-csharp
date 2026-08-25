using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow, version = "1.0.0" });
    }

    [HttpGet("readiness")]
    public IActionResult Readiness()
    {
        return Ok(new { status = "ready" });
    }

    [HttpGet("liveness")]
    public IActionResult Liveness()
    {
        return Ok(new { status = "alive" });
    }
}
