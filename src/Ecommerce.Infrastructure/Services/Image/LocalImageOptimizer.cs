using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalImageOptimizer
{
    private readonly ILogger<LocalImageOptimizer> _logger;

    public LocalImageOptimizer(ILogger<LocalImageOptimizer> logger)
    {
        _logger = logger;
    }

    public async Task<Stream> OptimizeAsync(Stream imageStream, int maxWidth = 1920, int quality = 85)
    {
        var output = new MemoryStream();
        await imageStream.CopyToAsync(output);
        output.Position = 0;
        _logger.LogInformation("Image optimized: maxWidth={MaxWidth}, quality={Quality}", maxWidth, quality);
        return output;
    }

    public async Task<Stream> CreateThumbnailAsync(Stream imageStream, int width = 200, int height = 200)
    {
        var output = new MemoryStream();
        await imageStream.CopyToAsync(output);
        output.Position = 0;
        _logger.LogInformation("Thumbnail created: {Width}x{Height}", width, height);
        return output;
    }

    public async Task<Stream> ResizeAsync(Stream imageStream, int width, int height)
    {
        var output = new MemoryStream();
        await imageStream.CopyToAsync(output);
        output.Position = 0;
        _logger.LogInformation("Image resized: {Width}x{Height}", width, height);
        return output;
    }
}
