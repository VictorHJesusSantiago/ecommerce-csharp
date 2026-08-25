using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    [HttpGet]
    public IActionResult Search([FromQuery] string q, [FromQuery] string category = null, [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null, [FromQuery] string sort = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new { query = q, category, minPrice, maxPrice, sort, page, pageSize, results = new object[] { } });
    }

    [HttpGet("autocomplete")]
    public IActionResult Autocomplete([FromQuery] string q)
    {
        return Ok(new { suggestions = new string[] { } });
    }

    [HttpGet("suggestions")]
    public IActionResult GetSuggestions([FromQuery] string q)
    {
        return Ok(new { suggestions = new string[] { } });
    }

    [HttpGet("popular")]
    public IActionResult GetPopularSearches()
    {
        return Ok(new { popular = new string[] { } });
    }

    [HttpGet("trending")]
    public IActionResult GetTrendingSearches()
    {
        return Ok(new { trending = new string[] { } });
    }

    [HttpGet("recent")]
    public IActionResult GetRecentSearches()
    {
        return Ok(new { recent = new string[] { } });
    }
}
