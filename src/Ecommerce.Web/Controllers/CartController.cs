using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    public IActionResult Index() => View();
    public IActionResult AddConfirmation() => View();

    [HttpPost]
    public IActionResult Add(Guid productId, int quantity = 1)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Update(Guid itemId, int quantity)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(Guid itemId)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Clear()
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ApplyCoupon(string code)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveCoupon()
    {
        return RedirectToAction("Index");
    }

    public IActionResult MiniCart() => PartialView("_MiniCartPartial");
    public IActionResult Summary() => PartialView("_CartSummaryPartial");
    public IActionResult GetCount() => Json(new { count = 0 });
}
