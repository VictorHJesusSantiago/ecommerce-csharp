using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Catalog;

public class Brand : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? LogoUrl { get; private set; }
    public string? BannerUrl { get; private set; }
    public string? WebsiteUrl { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFeatured { get; private set; }
    public int DisplayOrder { get; private set; }
    public string? MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }

    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Brand() { }

    public static Brand Create(
        string name,
        string? description = null,
        string? logoUrl = null,
        string? bannerUrl = null,
        string? websiteUrl = null,
        bool isFeatured = false,
        int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Brand name is required.", nameof(name));
        if (name.Length > 200)
            throw new ArgumentException("Brand name cannot exceed 200 characters.", nameof(name));

        return new Brand
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Slug = Slug.Create(name).Value,
            Description = description?.Trim(),
            LogoUrl = logoUrl,
            BannerUrl = bannerUrl,
            WebsiteUrl = websiteUrl,
            IsActive = true,
            IsFeatured = isFeatured,
            DisplayOrder = displayOrder,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string name,
        string? description = null,
        string? logoUrl = null,
        string? bannerUrl = null,
        string? websiteUrl = null,
        bool isActive = true,
        bool isFeatured = false,
        int displayOrder = 0,
        string? metaTitle = null,
        string? metaDescription = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Brand name is required.", nameof(name));

        Name = name.Trim();
        Description = description?.Trim();
        LogoUrl = logoUrl;
        BannerUrl = bannerUrl;
        WebsiteUrl = websiteUrl;
        IsActive = isActive;
        IsFeatured = isFeatured;
        DisplayOrder = displayOrder;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        UpdateTimestamp();
    }

    public void Activate() { IsActive = true; UpdateTimestamp(); }
    public void Deactivate() { IsActive = false; UpdateTimestamp(); }
}

public class ProductCategory : BaseEntity
{
    public Guid ProductId { get; private set; }
    public Guid CategoryId { get; private set; }
    public bool IsPrimary { get; private set; }
    public int DisplayOrder { get; private set; }

    public Product Product { get; private set; } = null!;
    public Category Category { get; private set; } = null!;

    private ProductCategory() { }

    public static ProductCategory Create(Guid productId, Guid categoryId, bool isPrimary = false, int displayOrder = 0)
    {
        return new ProductCategory
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            CategoryId = categoryId,
            IsPrimary = isPrimary,
            DisplayOrder = displayOrder,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
        UpdateTimestamp();
    }
}

public class ProductTag : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;

    private ProductTag() { }

    public static ProductTag Create(string name, Guid productId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tag name is required.", nameof(name));

        return new ProductTag
        {
            Id = Guid.NewGuid(),
            Name = name.Trim().ToLowerInvariant(),
            Slug = Slug.Create(name).Value,
            ProductId = productId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
