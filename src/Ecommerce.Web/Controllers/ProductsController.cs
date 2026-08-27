using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Route("[controller]")]
public class ProductsController : Controller
{
    public IActionResult Index([FromQuery] string category = null, [FromQuery] string brand = null, [FromQuery] string sort = null, [FromQuery] int page = 1)
    {
        ViewBag.Category = category;
        ViewBag.Brand = brand;
        ViewBag.Sort = sort;
        ViewBag.Page = page;
        return View();
    }

    public IActionResult Detail(Guid id)
    {
        ViewBag.ProductId = id;
        return View();
    }

    public IActionResult Categories()
    {
        return View();
    }

    public IActionResult Brands()
    {
        return View();
    }

    public IActionResult NewArrivals()
    {
        return View("Index");
    }

    public IActionResult Sale()
    {
        return View("Index");
    }

    public IActionResult ByCategory(string slug)
    {
        ViewBag.Category = slug;
        return View("Index");
    }

    public IActionResult ByBrand(string slug)
    {
        ViewBag.Brand = slug;
        return View("Index");
    }

    public IActionResult Compare([FromQuery] Guid[] ids)
    {
        ViewBag.ProductIds = ids;
        return View();
    }

    public IActionResult QuickView(Guid id)
    {
        ViewBag.ProductId = id;
        return PartialView("_QuickViewPartial");
    }
}
