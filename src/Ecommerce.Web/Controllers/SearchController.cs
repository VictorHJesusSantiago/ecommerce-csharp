using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Route("[controller]")]
public class SearchController : Controller
{
    public IActionResult Index([FromQuery] string q, [FromQuery] string category = null, [FromQuery] string brand = null, [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null, [FromQuery] string sort = null, [FromQuery] int page = 1, [FromQuery] bool inStock = false, [FromQuery] bool onSale = false, [FromQuery] double? minRating = null)
    {
        ViewBag.Query = q;
        ViewBag.Category = category;
        ViewBag.Brand = brand;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.Sort = sort;
        ViewBag.Page = page;
        ViewBag.InStock = inStock;
        ViewBag.OnSale = onSale;
        ViewBag.MinRating = minRating;
        return View();
    }

    public IActionResult Autocomplete([FromQuery] string q)
    {
        return Json(new { suggestions = new string[] { } });
    }

    public IActionResult Suggestions([FromQuery] string q)
    {
        return Json(new { suggestions = new string[] { } });
    }

    public IActionResult Advanced() => View();
}
