using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class OrdersController : Controller
{
    public IActionResult History() => View();
    public IActionResult Detail(Guid id) { ViewBag.OrderId = id; return View(); }
    public IActionResult Track(string trackingNumber) { ViewBag.TrackingNumber = trackingNumber; return View(); }

    [HttpPost]
    public IActionResult Cancel(Guid id)
    {
        return RedirectToAction("History");
    }

    [HttpPost]
    public IActionResult RequestReturn(Guid id, object model)
    {
        return RedirectToAction("Detail", new { id });
    }

    [HttpGet]
    public IActionResult Invoice(Guid id)
    {
        ViewBag.OrderId = id;
        return View();
    }

    [HttpGet]
    public IActionResult Print(Guid id)
    {
        ViewBag.OrderId = id;
        return View();
    }
}
