using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult About() => View();
    public IActionResult Contact() => View();
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        if (statusCode == 404)
            return View("NotFound");
        return View("Error");
    }

    public IActionResult NotFound() => View();
    public IActionResult Forbidden() => View();
    public IActionResult UnderDevelopment() => View();
    public IActionResult Maintenance() => View();
    public IActionResult Faq() => View();
    public IActionResult TermsOfService() => View();
    public IActionResult CookiesPolicy() => View();
    public IActionResult SitemapXml() => Content("application/xml", "<?xml version=\"1.0\"?>");
}
