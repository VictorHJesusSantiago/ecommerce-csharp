using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class CheckoutController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Shipping() => View();
    public IActionResult Payment() => View();
    public IActionResult Review() => View();
    public IActionResult Success([FromQuery] Guid? orderId) { ViewBag.OrderId = orderId; return View(); }
    public IActionResult Failure([FromQuery] string reason) { ViewBag.Reason = reason; return View(); }
    public IActionResult Cancel() => View();

    [HttpPost]
    public IActionResult ApplyCoupon([FromQuery] string code)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveCoupon()
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SetShippingMethod(Guid methodId)
    {
        return RedirectToAction("Payment");
    }

    [HttpPost]
    public IActionResult ProcessPayment(object model)
    {
        return RedirectToAction("Success");
    }
}
