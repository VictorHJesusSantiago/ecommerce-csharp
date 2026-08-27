using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Catalog;

public class ProductVariant : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    public string? Barcode { get; private set; }
    public decimal Price { get; private set; }
    public decimal? CompareAtPrice { get; private set; }
    public decimal? CostPrice { get; private set; }
    public decimal? Weight { get; private set; }
    public string? WeightUnit { get; private set; }
    public int StockQuantity { get; private set; }
    public int LowStockThreshold { get; set; }
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public string? Option1 { get; private set; }
    public string? Option2 { get; private set; }
    public string? Option3 { get; private set; }
    public string? ImageUrl { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;

    private readonly List<StockMovement> _stockMovements = [];
    public IReadOnlyCollection<StockMovement> StockMovements => _stockMovements.AsReadOnly();

    private ProductVariant() { }

    public static ProductVariant Create(
        string name,
        string sku,
        decimal price,
        Guid productId,
        decimal? compareAtPrice = null,
        decimal? costPrice = null,
        decimal? weight = null,
        string? weightUnit = null,
        int stockQuantity = 0,
        bool isDefault = false,
        string? option1 = null,
        string? option2 = null,
        string? option3 = null,
        string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Variant name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU is required.", nameof(sku));
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        return new ProductVariant
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            SKU = sku.ToUpperInvariant(),
            Price = price,
            CompareAtPrice = compareAtPrice,
            CostPrice = costPrice,
            Weight = weight,
            WeightUnit = weightUnit,
            StockQuantity = stockQuantity,
            LowStockThreshold = 5,
            IsDefault = isDefault,
            IsActive = true,
            Option1 = option1,
            Option2 = option2,
            Option3 = option3,
            ImageUrl = imageUrl,
            ProductId = productId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string name,
        decimal price,
        decimal? compareAtPrice = null,
        decimal? costPrice = null,
        decimal? weight = null,
        string? weightUnit = null,
        string? option1 = null,
        string? option2 = null,
        string? option3 = null,
        string? imageUrl = null,
        bool? isActive = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Variant name is required.", nameof(name));
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        Name = name.Trim();
        Price = price;
        CompareAtPrice = compareAtPrice;
        CostPrice = costPrice;
        Weight = weight;
        WeightUnit = weightUnit;
        Option1 = option1;
        Option2 = option2;
        Option3 = option3;
        ImageUrl = imageUrl;
        if (isActive.HasValue) IsActive = isActive.Value;
        UpdateTimestamp();
    }

    public void SetAsDefault()
    {
        IsDefault = true;
        UpdateTimestamp();
    }

    public void SetPrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
        Price = newPrice;
        UpdateTimestamp();
    }

    public void AddStock(int quantity, string? reason = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        StockQuantity += quantity;
        UpdateTimestamp();
    }

    public void RemoveStock(int quantity, string? reason = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        if (StockQuantity < quantity && !Product?.AllowBackorder == true)
            throw new InsufficientStockException(Name, quantity, StockQuantity);
        StockQuantity -= quantity;
        UpdateTimestamp();
    }

    public void SetStockQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.", nameof(quantity));
        StockQuantity = quantity;
        UpdateTimestamp();
    }

    public bool IsInStock() => StockQuantity > 0;
    public bool IsLowStock() => StockQuantity <= LowStockThreshold && StockQuantity > 0;
    public bool IsOutOfStock() => StockQuantity <= 0;

    public InventoryStatus GetInventoryStatus()
    {
        if (StockQuantity <= 0) return InventoryStatus.OutOfStock;
        if (IsLowStock()) return InventoryStatus.LowStock;
        return InventoryStatus.InStock;
    }
}
