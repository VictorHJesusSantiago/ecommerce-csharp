using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalSitemapGenerator : ISitemapGenerator
{
    private readonly ILogger<LocalSitemapGenerator> _logger;

    public LocalSitemapGenerator(ILogger<LocalSitemapGenerator> logger)
    {
        _logger = logger;
    }

    public Task<string> GenerateSitemapAsync(string baseUrl, List<SitemapUrl> urls)
    {
        var xml = new System.Text.StringBuilder();
        xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        xml.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        foreach (var url in urls)
        {
            xml.AppendLine("  <url>");
            xml.AppendLine($"    <loc>{baseUrl}{url.Path}</loc>");
            xml.AppendLine($"    <lastmod>{url.LastModified:yyyy-MM-dd}</lastmod>");
            xml.AppendLine($"    <changefreq>{url.ChangeFrequency}</changefreq>");
            xml.AppendLine($"    <priority>{url.Priority}</priority>");
            xml.AppendLine("  </url>");
        }

        xml.AppendLine("</urlset>");
        _logger.LogInformation("Sitemap generated with {Count} URLs", urls.Count);
        return Task.FromResult(xml.ToString());
    }
}

public class SitemapUrl
{
    public string Path { get; set; } = string.Empty;
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
    public string ChangeFrequency { get; set; } = "weekly";
    public double Priority { get; set; } = 0.5;
}

public interface ISitemapGenerator
{
    Task<string> GenerateSitemapAsync(string baseUrl, List<SitemapUrl> urls);
}
