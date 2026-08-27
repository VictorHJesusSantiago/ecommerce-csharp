using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminMarketingController : Controller
{
    public IActionResult Index() => View("Marketing");
    public IActionResult Coupons() => View("Coupons");
    public IActionResult Promotions() => View("Promotions");
    public IActionResult Banners() => View("Banners");
    public IActionResult Newsletter() => View("Newsletter");
    public IActionResult CreateCoupon() => View("CreateCoupon");
    public IActionResult CreatePromotion() => View("CreatePromotion");
    public IActionResult CreateBanner() => View("CreateBanner");
    public IActionResult EditCoupon(Guid id) { ViewBag.CouponId = id; return View("EditCoupon"); }
    public IActionResult EditPromotion(Guid id) { ViewBag.PromotionId = id; return View("EditPromotion"); }
    public IActionResult EditBanner(Guid id) { ViewBag.BannerId = id; return View("EditBanner"); }

    [HttpPost]
    public IActionResult CreateCouponPost(Ecommerce.Application.DTOs.Marketing.CreateCouponRequest request)
    {
        return RedirectToAction("Coupons");
    }

    [HttpPost]
    public IActionResult CreatePromotionPost(Ecommerce.Application.DTOs.Marketing.CreatePromotionRequest request)
    {
        return RedirectToAction("Promotions");
    }

    [HttpPost]
    public IActionResult EditCouponPost(Ecommerce.Application.DTOs.Marketing.UpdateCouponRequest request)
    {
        return RedirectToAction("Coupons");
    }

    [HttpPost]
    public IActionResult EditPromotionPost(Ecommerce.Application.DTOs.Marketing.UpdatePromotionRequest request)
    {
        return RedirectToAction("Promotions");
    }
}
