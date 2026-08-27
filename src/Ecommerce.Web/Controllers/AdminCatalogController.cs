using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCatalogController : Controller
{
    public IActionResult Categories() => View("Categories");
    public IActionResult Brands() => View("Brands");
    public IActionResult Pages() => View("Pages");
    public IActionResult CreateCategory() => View("CreateCategory");
    public IActionResult CreateBrand() => View("CreateBrand");
    public IActionResult CreatePage() => View("CreatePage");
    public IActionResult EditCategory(Guid id) { ViewBag.CategoryId = id; return View("EditCategory"); }
    public IActionResult EditBrand(Guid id) { ViewBag.BrandId = id; return View("EditBrand"); }
    public IActionResult EditPage(Guid id) { ViewBag.PageId = id; return View("EditPage"); }

    [HttpPost]
    public IActionResult CreateCategoryPost(Ecommerce.Application.DTOs.Catalog.CreateCategoryRequest request)
    {
        return RedirectToAction("Categories");
    }

    [HttpPost]
    public IActionResult CreateBrandPost(Ecommerce.Application.DTOs.Catalog.CreateBrandRequest request)
    {
        return RedirectToAction("Brands");
    }

    [HttpPost]
    public IActionResult CreatePagePost(Ecommerce.Application.DTOs.CMS.CreateCmsPageRequest request)
    {
        return RedirectToAction("Pages");
    }

    [HttpPost]
    public IActionResult EditCategoryPost(Ecommerce.Application.DTOs.Catalog.UpdateCategoryRequest request)
    {
        return RedirectToAction("Categories");
    }

    [HttpPost]
    public IActionResult EditBrandPost(Ecommerce.Application.DTOs.Catalog.UpdateBrandRequest request)
    {
        return RedirectToAction("Brands");
    }

    [HttpPost]
    public IActionResult EditPagePost(Ecommerce.Application.DTOs.CMS.UpdateCmsPageRequest request)
    {
        return RedirectToAction("Pages");
    }
}
