using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly ILogger<LocalFileStorageService> _logger;
    private readonly string _basePath;

    public LocalFileStorageService(ILogger<LocalFileStorageService> logger, string basePath = "wwwroot/uploads")
    {
        _logger = logger;
        _basePath = basePath;
        Directory.CreateDirectory(_basePath);
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string? folder = null)
    {
        var path = folder != null ? Path.Combine(_basePath, folder) : _basePath;
        Directory.CreateDirectory(path);
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(path, uniqueFileName);
        using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput);
        _logger.LogInformation("File uploaded: {Path}", filePath);
        return $"/uploads/{(folder != null ? folder + "/" : "")}{uniqueFileName}";
    }

    public Task DeleteFileAsync(string fileUrl)
    {
        var filePath = fileUrl.Replace("/uploads/", "").TrimStart('/');
        var fullPath = Path.Combine(_basePath, filePath);
        if (File.Exists(fullPath)) { File.Delete(fullPath); _logger.LogInformation("File deleted: {Path}", fullPath); }
        return Task.CompletedTask;
    }

    public Task<Stream> GetFileAsync(string fileUrl)
    {
        var filePath = fileUrl.Replace("/uploads/", "").TrimStart('/');
        var fullPath = Path.Combine(_basePath, filePath);
        Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(stream);
    }

    public Task<bool> FileExistsAsync(string fileUrl)
    {
        var filePath = fileUrl.Replace("/uploads/", "").TrimStart('/');
        var fullPath = Path.Combine(_basePath, filePath);
        return Task.FromResult(File.Exists(fullPath));
    }
}
