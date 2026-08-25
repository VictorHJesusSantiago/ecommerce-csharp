using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        return Ok(new { totalOrders = 0, totalRevenue = 0m, totalCustomers = 0, totalProducts = 0, recentOrders = new object[] { } });
    }

    [HttpGet("users")]
    public IActionResult GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new { page, pageSize, users = new object[] { } });
    }

    [HttpGet("users/{id:guid}")]
    public IActionResult GetUser(Guid id)
    {
        return Ok(new { id });
    }

    [HttpPut("users/{id:guid}/lock")]
    public IActionResult LockUser(Guid id)
    {
        return Ok(new { id, locked = true });
    }

    [HttpPut("users/{id:guid}/unlock")]
    public IActionResult UnlockUser(Guid id)
    {
        return Ok(new { id, unlocked = true });
    }

    [HttpGet("settings")]
    public IActionResult GetSettings()
    {
        return Ok(new { settings = new object[] { } });
    }

    [HttpPut("settings")]
    public IActionResult UpdateSettings([FromBody] object request)
    {
        return Ok(new { updated = true });
    }
}
