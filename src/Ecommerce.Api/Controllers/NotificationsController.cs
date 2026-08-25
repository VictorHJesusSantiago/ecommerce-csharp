using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetNotifications([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new { page, pageSize, notifications = new object[] { } });
    }

    [HttpPost("{id:guid}/read")]
    public IActionResult MarkAsRead(Guid id)
    {
        return Ok(new { id, read = true });
    }

    [HttpPost("read-all")]
    public IActionResult MarkAllAsRead()
    {
        return Ok(new { allRead = true });
    }

    [HttpGet("preferences")]
    public IActionResult GetPreferences()
    {
        return Ok(new { });
    }

    [HttpPut("preferences")]
    public IActionResult UpdatePreferences([FromBody] object request)
    {
        return Ok(new { updated = true });
    }
}
