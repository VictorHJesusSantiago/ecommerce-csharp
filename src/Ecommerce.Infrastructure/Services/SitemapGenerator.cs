namespace Ecommerce.Infrastructure.Services;

public class SitemapGenerator
{
    private readonly ICmsPageRepository _pageRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SitemapGenerator(
        ICmsPageRepository pageRepository,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _pageRepository = pageRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<string> GenerateSitemapAsync(string baseUrl, CancellationToken ct = default)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        sb.AppendLine(CreateUrl(baseUrl, "", DateTime.UtcNow, 1.0m, "daily"));
        sb.AppendLine(CreateUrl(baseUrl, "/products", DateTime.UtcNow, 0.9m, "daily"));
        sb.AppendLine(CreateUrl(baseUrl, "/about", DateTime.UtcNow, 0.5m, "monthly"));
        sb.AppendLine(CreateUrl(baseUrl, "/contact", DateTime.UtcNow, 0.5m, "monthly"));

        sb.AppendLine("</urlset>");
        return sb.ToString();
    }

    public async Task<List<SitemapUrl>> GetSitemapUrlsAsync(string baseUrl, CancellationToken ct = default)
    {
        var urls = new List<SitemapUrl>
        {
            new() { Url = baseUrl, LastModified = DateTime.UtcNow, Priority = 1.0m, ChangeFrequency = "daily" },
            new() { Url = $"{baseUrl}/products", LastModified = DateTime.UtcNow, Priority = 0.9m, ChangeFrequency = "daily" },
            new() { Url = $"{baseUrl}/about", LastModified = DateTime.UtcNow, Priority = 0.5m, ChangeFrequency = "monthly" },
            new() { Url = $"{baseUrl}/contact", LastModified = DateTime.UtcNow, Priority = 0.5m, ChangeFrequency = "monthly" }
        };

        await Task.CompletedTask;
        return urls;
    }

    private string CreateUrl(string baseUrl, string path, DateTime lastModified, decimal priority, string changeFrequency)
    {
        return $"  <url><loc>{baseUrl}{path}</loc><lastmod>{lastModified:yyyy-MM-dd}</lastmod><changefreq>{changeFrequency}</changefreq><priority>{priority}</priority></url>";
    }
}

public class SitemapUrl
{
    public string Url { get; set; } = string.Empty;
    public DateTime LastModified { get; set; }
    public decimal Priority { get; set; }
    public string ChangeFrequency { get; set; } = "weekly";
}

public class RobotsTxtGenerator
{
    public string GenerateRobotsTxt(string baseUrl, bool allowAll = true)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("User-agent: *");

        if (allowAll)
        {
            sb.AppendLine("Allow: /");
        }
        else
        {
            sb.AppendLine("Disallow: /admin/");
            sb.AppendLine("Disallow: /api/");
            sb.AppendLine("Disallow: /account/");
        }

        sb.AppendLine($"Sitemap: {baseUrl}/sitemap.xml");
        return sb.ToString();
    }
}
