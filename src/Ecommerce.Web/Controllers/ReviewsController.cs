using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Route("[controller]")]
public class ReviewsController : Controller
{
    public IActionResult ProductReviews(Guid productId, [FromQuery] int page = 1, [FromQuery] string sort = "newest")
    {
        ViewBag.ProductId = productId;
        ViewBag.Page = page;
        ViewBag.Sort = sort;
        return View();
    }

    public IActionResult WriteReview(Guid productId)
    {
        ViewBag.ProductId = productId;
        return View();
    }

    public IActionResult EditReview(Guid reviewId)
    {
        ViewBag.ReviewId = reviewId;
        return View();
    }

    public IActionResult MyReviews() => View();
}
