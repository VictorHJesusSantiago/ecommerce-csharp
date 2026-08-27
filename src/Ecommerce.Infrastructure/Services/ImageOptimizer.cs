namespace Ecommerce.Infrastructure.Services;

public class ImageOptimizer
{
    private readonly ICacheService _cacheService;

    public ImageOptimizer(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<ImageOptimizationResult> OptimizeImageAsync(byte[] imageBytes, string fileName, ImageOptimizationSettings? settings = null)
    {
        settings ??= new ImageOptimizationSettings();

        var result = new ImageOptimizationResult
        {
            OriginalSize = imageBytes.Length,
            FileName = fileName,
            ContentType = GetContentType(fileName),
            Width = 0,
            Height = 0
        };

        await Task.CompletedTask;
        return result;
    }

    public async Task<List<ImageVariant>> GenerateVariantsAsync(byte[] imageBytes, string fileName, List<ImageVariantSettings> variants)
    {
        var result = new List<ImageVariant>();

        foreach (var variant in variants)
        {
            result.Add(new ImageVariant
            {
                FileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{variant.Width}x{variant.Height}{Path.GetExtension(fileName)}",
                Width = variant.Width,
                Height = variant.Height,
                Size = imageBytes.Length,
                Url = $"/images/optimized/{fileName}"
            });
        }

        await Task.CompletedTask;
        return result;
    }

    public async Task<string> GetImageUrlAsync(string imagePath, int? width = null, int? height = null, int? quality = null)
    {
        var cacheKey = $"image:{imagePath}:w{width}:h{height}:q{quality}";
        var cached = await _cacheService.GetAsync<string>(cacheKey);
        if (cached != null) return cached;

        var url = $"/images/optimized/{imagePath}";
        await _cacheService.SetAsync(cacheKey, url, TimeSpan.FromHours(24));
        return url;
    }

    public bool IsSupportedImageType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension is ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp" or ".svg";
    }

    private string GetContentType(string fileName)
    {
        return Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };
    }
}

public class ImageOptimizationSettings
{
    public int MaxWidth { get; set; } = 1920;
    public int MaxHeight { get; set; } = 1080;
    public int Quality { get; set; } = 85;
    public bool PreserveMetadata { get; set; }
    public bool ConvertToWebP { get; set; } = true;
}

public class ImageVariantSettings
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Quality { get; set; } = 85;
    public bool Resize { get; set; } = true;
    public bool Crop { get; set; }
}

public class ImageOptimizationResult
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public int OriginalSize { get; set; }
    public int OptimizedSize { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public double CompressionRatio => OriginalSize > 0 ? Math.Round((1 - (double)OptimizedSize / OriginalSize) * 100, 2) : 0;
    public string? Url { get; set; }
}

public class ImageVariant
{
    public string FileName { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public long Size { get; set; }
    public string Url { get; set; } = string.Empty;
}
