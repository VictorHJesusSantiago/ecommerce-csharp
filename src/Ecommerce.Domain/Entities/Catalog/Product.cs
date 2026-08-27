using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Catalog;

public class Product : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    public string? Barcode { get; private set; }
    public string? Description { get; private set; }
    public string? ShortDescription { get; private set; }
    public decimal Price { get; private set; }
    public decimal? CompareAtPrice { get; private set; }
    public decimal? CostPrice { get; private set; }
    public string Currency { get; private set; } = "USD";
    public decimal TaxRate { get; private set; }
    public bool IsTaxable { get; private set; }
    public bool IsShippingRequired { get; private set; }
    public decimal? Weight { get; private set; }
    public string? WeightUnit { get; private set; }
    public decimal? Length { get; private set; }
    public decimal? Width { get; private set; }
    public decimal? Height { get; private set; }
    public string? DimensionUnit { get; private set; }
    public ProductStatus Status { get; private set; }
    public ProductType ProductType { get; private set; }
    public bool IsFeatured { get; private set; }
    public bool IsNewArrival { get; private set; }
    public bool IsBestSeller { get; private set; }
    public bool AllowReviews { get; private set; }
    public bool AllowBackorder { get; private set; }
    public int MinOrderQuantity { get; private set; }
    public int? MaxOrderQuantity { get; private set; }
    public int LowStockThreshold { get; private set; }
    public int TotalSold { get; private set; }
    public double AverageRating { get; private set; }
    public int ReviewCount { get; private set; }
    public int ViewCount { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid? SubCategoryId { get; private set; }
    public Guid? BrandId { get; private set; }
    public string? MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeywords { get; private set; }
    public string? Tags { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public DateTime? AvailableFrom { get; private set; }
    public DateTime? AvailableTo { get; private set; }

    private readonly List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

    private readonly List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    private readonly List<ProductCategory> _productCategories = [];
    public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();

    private readonly List<ProductTag> _tags = [];
    public IReadOnlyCollection<ProductTag> Tags2 => _tags.AsReadOnly();

    private Product() { }

    public static Product Create(
        string name,
        string sku,
        decimal price,
        Guid categoryId,
        string? description = null,
        string? shortDescription = null,
        ProductType productType = ProductType.Physical,
        string currency = "USD",
        decimal taxRate = 0,
        bool isTaxable = true,
        bool isShippingRequired = true,
        decimal? weight = null,
        string? weightUnit = null,
        int lowStockThreshold = 5,
        bool allowReviews = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));
        if (name.Length > 500)
            throw new ArgumentException("Product name cannot exceed 500 characters.", nameof(name));
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU is required.", nameof(sku));
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Slug = Slug.Create(name).Value,
            SKU = sku.ToUpperInvariant(),
            Description = description?.Trim(),
            ShortDescription = shortDescription?.Trim(),
            Price = price,
            Currency = currency,
            TaxRate = taxRate,
            IsTaxable = isTaxable,
            IsShippingRequired = isShippingRequired,
            Weight = weight,
            WeightUnit = weightUnit,
            ProductType = productType,
            Status = ProductStatus.Draft,
            LowStockThreshold = lowStockThreshold,
            AllowReviews = allowReviews,
            AllowBackorder = false,
            MinOrderQuantity = 1,
            CreatedAt = DateTime.UtcNow
        };

        product.AddDomainEvent(new Events.Catalog.ProductCreatedEvent(
            product.Id, product.Name, product.Price, 0));
        return product;
    }

    public void Update(
        string name,
        string? description = null,
        string? shortDescription = null,
        decimal? compareAtPrice = null,
        decimal? costPrice = null,
        decimal? taxRate = null,
        bool? isTaxable = null,
        bool? isShippingRequired = null,
        decimal? weight = null,
        string? weightUnit = null,
        bool? isFeatured = null,
        bool? isNewArrival = null,
        bool? isBestSeller = null,
        bool? allowReviews = null,
        bool? allowBackorder = null,
        int? minOrderQuantity = null,
        int? maxOrderQuantity = null,
        int? lowStockThreshold = null,
        string? metaTitle = null,
        string? metaDescription = null,
        string? metaKeywords = null,
        string? tags = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        var oldPrice = Price;
        Name = name.Trim();
        Description = description?.Trim();
        ShortDescription = shortDescription?.Trim();
        CompareAtPrice = compareAtPrice;
        CostPrice = costPrice;
        if (taxRate.HasValue) TaxRate = taxRate.Value;
        if (isTaxable.HasValue) IsTaxable = isTaxable.Value;
        if (isShippingRequired.HasValue) IsShippingRequired = isShippingRequired.Value;
        if (weight.HasValue) Weight = weight.Value;
        WeightUnit = weightUnit;
        if (isFeatured.HasValue) IsFeatured = isFeatured.Value;
        if (isNewArrival.HasValue) IsNewArrival = isNewArrival.Value;
        if (isBestSeller.HasValue) IsBestSeller = isBestSeller.Value;
        if (allowReviews.HasValue) AllowReviews = allowReviews.Value;
        if (allowBackorder.HasValue) AllowBackorder = allowBackorder.Value;
        if (minOrderQuantity.HasValue) MinOrderQuantity = minOrderQuantity.Value;
        if (maxOrderQuantity.HasValue) MaxOrderQuantity = maxOrderQuantity.Value;
        if (lowStockThreshold.HasValue) LowStockThreshold = lowStockThreshold.Value;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeywords = metaKeywords;
        Tags = tags;
        UpdateTimestamp();

        if (oldPrice != Price)
        {
            AddDomainEvent(new Events.Catalog.ProductPriceChangedEvent(Id, Name, oldPrice, Price));
        }

        AddDomainEvent(new Events.Catalog.ProductUpdatedEvent(Id, Name, oldPrice, Price));
    }

    public void Publish()
    {
        if (Status != ProductStatus.Draft)
            throw new InvalidDomainOperationException("Only draft products can be published.");
        Status = ProductStatus.Active;
        PublishedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Unpublish()
    {
        Status = ProductStatus.Inactive;
        UpdateTimestamp();
    }

    public void Archive()
    {
        Status = ProductStatus.Archived;
        UpdateTimestamp();
    }

    public void Discontinue()
    {
        Status = ProductStatus.Discontinued;
        UpdateTimestamp();
    }

    public void SetPrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
        var oldPrice = Price;
        Price = newPrice;
        UpdateTimestamp();
        AddDomainEvent(new Events.Catalog.ProductPriceChangedEvent(Id, Name, oldPrice, newPrice));
    }

    public void IncrementViewCount()
    {
        ViewCount++;
    }

    public void AddSale(int quantity)
    {
        TotalSold += quantity;
        UpdateTimestamp();
    }

    public void UpdateRating(double newRating, int reviewCount)
    {
        AverageRating = Math.Round(newRating, 2);
        ReviewCount = reviewCount;
        UpdateTimestamp();
    }

    public void AddVariant(ProductVariant variant)
    {
        if (variant is null)
            throw new ArgumentNullException(nameof(variant));
        if (_variants.Any(v => v.Id == variant.Id))
            throw new InvalidDomainOperationException("Variant already exists.");
        _variants.Add(variant);
        UpdateTimestamp();
    }

    public void RemoveVariant(Guid variantId)
    {
        var variant = _variants.FirstOrDefault(v => v.Id == variantId);
        if (variant is not null)
        {
            _variants.Remove(variant);
            UpdateTimestamp();
        }
    }

    public void AddImage(ProductImage image)
    {
        if (image is null)
            throw new ArgumentNullException(nameof(image));
        _images.Add(image);
        UpdateTimestamp();
    }

    public void RemoveImage(Guid imageId)
    {
        var image = _images.FirstOrDefault(i => i.Id == imageId);
        if (image is not null)
        {
            _images.Remove(image);
            UpdateTimestamp();
        }
    }

    public void SetPrimaryImage(Guid imageId)
    {
        foreach (var img in _images)
            img.SetPrimary(img.Id == imageId);
        UpdateTimestamp();
    }

    public ProductVariant? GetDefaultVariant()
    {
        return _variants.FirstOrDefault(v => v.IsDefault) ?? _variants.FirstOrDefault();
    }

    public decimal GetLowestPrice()
    {
        if (_variants.Count == 0) return Price;
        return _variants.Min(v => v.Price);
    }

    public decimal GetHighestPrice()
    {
        if (_variants.Count == 0) return Price;
        return _variants.Max(v => v.Price);
    }

    public bool IsInStock()
    {
        if (_variants.Count == 0) return true;
        return _variants.Any(v => v.IsInStock());
    }

    public bool IsAvailableForPurchase()
    {
        if (Status != ProductStatus.Active) return false;
        if (AvailableFrom.HasValue && DateTime.UtcNow < AvailableFrom.Value) return false;
        if (AvailableTo.HasValue && DateTime.UtcNow > AvailableTo.Value) return false;
        return true;
    }

    public string GetPrimaryImageUrl()
    {
        return _images.FirstOrDefault(i => i.IsPrimary && !i.IsDeleted)?.Url
            ?? _images.FirstOrDefault(i => !i.IsDeleted)?.Url
            ?? "/images/no-image.png";
    }
}
