using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    [HttpGet("products/{productId:guid}/reviews")]
    public IActionResult GetProductReviews(Guid productId, [FromQuery] int page = 1)
    {
        return Ok(new { productId, page, reviews = new object[] { } });
    }

    [HttpPost("products/{productId:guid}/reviews")]
    public IActionResult CreateReview(Guid productId, [FromBody] object request)
    {
        return Ok(new { reviewId = Guid.NewGuid() });
    }

    [HttpPut("reviews/{id:guid}")]
    public IActionResult UpdateReview(Guid id, [FromBody] object request)
    {
        return Ok(new { id, updated = true });
    }

    [HttpDelete("reviews/{id:guid}")]
    public IActionResult DeleteReview(Guid id)
    {
        return Ok(new { id, deleted = true });
    }

    [HttpPost("reviews/{id:guid}/helpful")]
    public IActionResult VoteHelpful(Guid id, [FromBody] object request)
    {
        return Ok(new { id, voted = true });
    }
}
