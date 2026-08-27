using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Catalog;

public class ProductImage : BaseEntity
{
    public string Url { get; private set; } = string.Empty;
    public string? AltText { get; private set; }
    public string? Title { get; private set; }
    public string? FileName { get; private set; }
    public string? ContentType { get; private set; }
    public long? FileSize { get; private set; }
    public int? Width { get; private set; }
    public int? Height { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsPrimary { get; private set; }
    public ImageType ImageType { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;

    private ProductImage() { }

    public static ProductImage Create(
        string url,
        Guid productId,
        string? altText = null,
        string? title = null,
        string? fileName = null,
        string? contentType = null,
        long? fileSize = null,
        int? width = null,
        int? height = null,
        int displayOrder = 0,
        bool isPrimary = false,
        ImageType imageType = ImageType.Gallery)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Image URL is required.", nameof(url));

        return new ProductImage
        {
            Id = Guid.NewGuid(),
            Url = url.Trim(),
            AltText = altText?.Trim(),
            Title = title?.Trim(),
            FileName = fileName,
            ContentType = contentType,
            FileSize = fileSize,
            Width = width,
            Height = height,
            DisplayOrder = displayOrder,
            IsPrimary = isPrimary,
            ImageType = imageType,
            ProductId = productId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateUrl(string newUrl)
    {
        if (string.IsNullOrWhiteSpace(newUrl))
            throw new ArgumentException("Image URL is required.", nameof(newUrl));
        Url = newUrl.Trim();
        UpdateTimestamp();
    }

    public void SetAltText(string? altText)
    {
        AltText = altText?.Trim();
        UpdateTimestamp();
    }

    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
        UpdateTimestamp();
    }

    public void SetDisplayOrder(int order)
    {
        DisplayOrder = order;
        UpdateTimestamp();
    }

    public void SetImageType(ImageType type)
    {
        ImageType = type;
        UpdateTimestamp();
    }
}
