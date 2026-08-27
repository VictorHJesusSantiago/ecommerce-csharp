using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminNotificationController : Controller
{
    public IActionResult Index() => View("Notifications");
    public IActionResult SendNotification() => View("SendNotification");

    [HttpPost]
    public IActionResult SendNotificationPost(Ecommerce.Application.DTOs.Notification.SendNotificationRequest request)
    {
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SendBulkNotification(Ecommerce.Application.DTOs.Notification.BulkSendNotificationRequest request)
    {
        return RedirectToAction("Index");
    }
}
