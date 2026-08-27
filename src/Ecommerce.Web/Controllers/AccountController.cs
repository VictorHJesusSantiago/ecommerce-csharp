using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class AccountController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Settings() => View();
    public IActionResult Addresses() => View();
    public IActionResult PaymentMethods() => View();
    public IActionResult Wishlist() => View();
    public IActionResult Orders() => View("OrderHistory");
    public IActionResult OrderDetail(Guid id) { ViewBag.OrderId = id; return View(); }
    public IActionResult ChangePassword() => View();
    public IActionResult DeleteAccount() => View();
    public IActionResult TwoFactorAuth() => View();
    public IActionResult Notifications() => View();
    public IActionResult ActivityLog() => View();
    public IActionResult DataExport() => View();
    public IActionResult ConnectedApps() => View();

    [HttpPost]
    public IActionResult UpdateProfile(object model) => RedirectToAction("Index");

    [HttpPost]
    public IActionResult AddAddress(object model) => RedirectToAction("Addresses");

    [HttpPost]
    public IActionResult UpdateAddress(Guid id, object model) => RedirectToAction("Addresses");

    [HttpPost]
    public IActionResult DeleteAddress(Guid id) => RedirectToAction("Addresses");

    [HttpPost]
    public IActionResult SetDefaultAddress(Guid id) => RedirectToAction("Addresses");

    [HttpPost]
    public IActionResult AddPaymentMethod(object model) => RedirectToAction("PaymentMethods");

    [HttpPost]
    public IActionResult DeletePaymentMethod(Guid id) => RedirectToAction("PaymentMethods");

    [HttpPost]
    public IActionResult ChangePasswordPost(object model) => RedirectToAction("ChangePassword");

    [HttpPost]
    public IActionResult ToggleWishlistItem(Guid productId) => RedirectToAction("Wishlist");

    [HttpPost]
    public IActionResult UpdateNotificationPreferences(object model) => RedirectToAction("Notifications");

    [HttpPost]
    public IActionResult RequestDataExport() => RedirectToAction("DataExport");

    [HttpPost]
    public IActionResult DeleteAccountConfirm() => RedirectToAction("Index", "Home");

    [HttpGet]
    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}
