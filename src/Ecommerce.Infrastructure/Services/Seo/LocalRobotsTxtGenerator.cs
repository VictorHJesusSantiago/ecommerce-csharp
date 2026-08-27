using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalRobotsTxtGenerator
{
    private readonly ILogger<LocalRobotsTxtGenerator> _logger;

    public LocalRobotsTxtGenerator(ILogger<LocalRobotsTxtGenerator> logger)
    {
        _logger = logger;
    }

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
        }
        sb.AppendLine();
        sb.AppendLine($"Sitemap: {baseUrl}/sitemap.xml");
        _logger.LogInformation("Robots.txt generated");
        return sb.ToString();
    }
}
