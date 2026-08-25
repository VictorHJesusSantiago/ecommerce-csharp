using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CmsController : ControllerBase
{
    [HttpGet("pages/{slug}")]
    public IActionResult GetPage(string slug)
    {
        return Ok(new { slug, title = "Page", content = "" });
    }

    [HttpGet("pages")]
    public IActionResult GetPages()
    {
        return Ok(new { pages = new object[] { } });
    }

    [HttpGet("navigation")]
    public IActionResult GetNavigation()
    {
        return Ok(new { menus = new object[] { } });
    }

    [HttpGet("settings")]
    public IActionResult GetSettings()
    {
        return Ok(new { settings = new object[] { } });
    }

    [HttpGet("media/{id:guid}")]
    public IActionResult GetMedia(Guid id)
    {
        return Ok(new { id, url = "", type = "" });
    }
}
