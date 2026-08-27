using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminReportsController : Controller
{
    public IActionResult Revenue() => View("RevenueReport");
    public IActionResult Sales() => View("SalesReport");
    public IActionResult Customers() => View("CustomerReport");
    public IActionResult Inventory() => View("InventoryReport");
    public IActionResult Payments() => View("PaymentReport");
    public IActionResult Marketing() => View("MarketingReport");
    public IActionResult ProductPerformance() => View("ProductPerformance");
}
