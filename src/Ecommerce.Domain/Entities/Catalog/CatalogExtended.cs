using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Catalog;

public class ProductCollection : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFeatured { get; private set; }
    public int DisplayOrder { get; private set; }
    public string? RuleJson { get; private set; }

    private readonly List<ProductCollectionItem> _items = [];
    public IReadOnlyCollection<ProductCollectionItem> Items => _items.AsReadOnly();

    private ProductCollection() { }

    public static ProductCollection Create(
        string name,
        string? description = null,
        string? imageUrl = null,
        bool isFeatured = false,
        int displayOrder = 0,
        string? ruleJson = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Collection name is required.", nameof(name));

        return new ProductCollection
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Slug = Slug.Create(name).Value,
            Description = description?.Trim(),
            ImageUrl = imageUrl,
            IsActive = true,
            IsFeatured = isFeatured,
            DisplayOrder = displayOrder,
            RuleJson = ruleJson,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string? description = null, string? imageUrl = null,
        bool isFeatured = false, int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Collection name is required.", nameof(name));
        Name = name.Trim();
        Description = description?.Trim();
        ImageUrl = imageUrl;
        IsFeatured = isFeatured;
        DisplayOrder = displayOrder;
        UpdateTimestamp();
    }

    public void AddProduct(Guid productId, int displayOrder = 0)
    {
        if (_items.Any(i => i.ProductId == productId))
            throw new InvalidDomainOperationException("Product already in collection.");
        _items.Add(ProductCollectionItem.Create(Id, productId, displayOrder));
        UpdateTimestamp();
    }

    public void RemoveProduct(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            UpdateTimestamp();
        }
    }

    public int ProductCount => _items.Count;
}

public class ProductCollectionItem : BaseEntity
{
    public Guid CollectionId { get; private set; }
    public Guid ProductId { get; private set; }
    public int DisplayOrder { get; private set; }
    public ProductCollection Collection { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private ProductCollectionItem() { }

    public static ProductCollectionItem Create(Guid collectionId, Guid productId, int displayOrder = 0)
    {
        return new ProductCollectionItem
        {
            Id = Guid.NewGuid(),
            CollectionId = collectionId,
            ProductId = productId,
            DisplayOrder = displayOrder,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class StockMovement : BaseEntity
{
    public Guid ProductVariantId { get; private set; }
    public StockMovementType MovementType { get; private set; }
    public int Quantity { get; private set; }
    public int PreviousStock { get; private set; }
    public int NewStock { get; private set; }
    public string? Reference { get; private set; }
    public string? Notes { get; private set; }
    public Guid? WarehouseId { get; private set; }
    public string? PerformedBy { get; private set; }
    public ProductVariant ProductVariant { get; private set; } = null!;

    private StockMovement() { }

    public static StockMovement Create(
        Guid productVariantId,
        StockMovementType movementType,
        int quantity,
        int previousStock,
        Guid? warehouseId = null,
        string? reference = null,
        string? notes = null,
        string? performedBy = null)
    {
        return new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductVariantId = productVariantId,
            MovementType = movementType,
            Quantity = quantity,
            PreviousStock = previousStock,
            NewStock = previousStock + quantity,
            WarehouseId = warehouseId,
            Reference = reference,
            Notes = notes,
            PerformedBy = performedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}
