using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Cart;

public class ShoppingCart : AggregateRoot
{
    public Guid? UserId { get; private set; }
    public string? SessionId { get; private set; }
    public CartStatus Status { get; private set; }
    public string? CouponCode { get; private set; }
    public decimal? CouponDiscount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public string? Notes { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }

    private readonly List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private ShoppingCart() { }

    public static ShoppingCart CreateForUser(Guid userId, string currency = "USD")
    {
        return new ShoppingCart
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Status = CartStatus.Active,
            Currency = currency,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static ShoppingCart CreateForSession(string sessionId, string currency = "USD")
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.", nameof(sessionId));

        return new ShoppingCart
        {
            Id = Guid.NewGuid(),
            SessionId = sessionId,
            Status = CartStatus.Active,
            Currency = currency,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddItem(
        Guid productId,
        string productName,
        string productSlug,
        string productImageUrl,
        int quantity,
        decimal unitPrice,
        string? variantId = null,
        string? variantName = null,
        string? sku = null,
        string? options = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        if (unitPrice < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(unitPrice));

        var existingItem = _items.FirstOrDefault(i =>
            i.ProductId == productId && i.VariantId == variantId);

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            _items.Add(CartItem.Create(
                Id, productId, productName, productSlug, productImageUrl,
                quantity, unitPrice, variantId, variantName, sku, options));
        }

        UpdateTimestamp();
    }

    public void RemoveItem(Guid productId, string? variantId = null)
    {
        var item = _items.FirstOrDefault(i =>
            i.ProductId == productId && i.VariantId == variantId);

        if (item is not null)
        {
            _items.Remove(item);
            UpdateTimestamp();
        }
    }

    public void UpdateItemQuantity(Guid productId, int quantity, string? variantId = null)
    {
        var item = _items.FirstOrDefault(i =>
            i.ProductId == productId && i.VariantId == variantId);

        if (item is null)
            throw new EntityNotFoundException("CartItem", productId);

        if (quantity <= 0)
        {
            _items.Remove(item);
        }
        else
        {
            item.SetQuantity(quantity);
        }

        UpdateTimestamp();
    }

    public void ClearItems()
    {
        _items.Clear();
        CouponCode = null;
        CouponDiscount = null;
        UpdateTimestamp();
    }

    public void ApplyCoupon(string code, decimal discountAmount)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Coupon code is required.", nameof(code));
        if (discountAmount < 0)
            throw new ArgumentException("Discount cannot be negative.", nameof(discountAmount));

        CouponCode = code.ToUpperInvariant().Trim();
        CouponDiscount = discountAmount;
        UpdateTimestamp();
    }

    public void RemoveCoupon()
    {
        CouponCode = null;
        CouponDiscount = null;
        UpdateTimestamp();
    }

    public void MarkAsConverted()
    {
        Status = CartStatus.Converted;
        UpdateTimestamp();
    }

    public void MarkAsAbandoned()
    {
        Status = CartStatus.Abandoned;
        UpdateTimestamp();
    }

    public void MergeWith(ShoppingCart otherCart)
    {
        if (otherCart is null) throw new ArgumentNullException(nameof(otherCart));
        foreach (var item in otherCart._items)
        {
            var existingItem = _items.FirstOrDefault(i =>
                i.ProductId == item.ProductId && i.VariantId == item.VariantId);
            if (existingItem is not null)
            {
                existingItem.IncreaseQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
            }
        }
        otherCart.Status = CartStatus.Merged;
        UpdateTimestamp();
    }

    public int TotalItemCount => _items.Sum(i => i.Quantity);

    public decimal Subtotal => _items.Sum(i => i.LineTotal);

    public decimal Discount => CouponDiscount ?? 0;

    public decimal Total => Math.Max(0, Subtotal - Discount);

    public bool IsEmpty => _items.Count == 0;

    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    public CartItem? GetItem(Guid productId, string? variantId = null)
    {
        return _items.FirstOrDefault(i =>
            i.ProductId == productId && i.VariantId == variantId);
    }
}

public class CartItem : BaseEntity
{
    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string ProductSlug { get; private set; } = string.Empty;
    public string ProductImageUrl { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal? CompareAtPrice { get; private set; }
    public string? VariantId { get; private set; }
    public string? VariantName { get; private set; }
    public string? SKU { get; private set; }
    public string? Options { get; private set; }
    public decimal? Weight { get; private set; }
    public bool IsAvailable { get; private set; }
    public string? UnavailableReason { get; private set; }
    public ShoppingCart Cart { get; private set; } = null!;

    private CartItem() { }

    public static CartItem Create(
        Guid cartId,
        Guid productId,
        string productName,
        string productSlug,
        string productImageUrl,
        int quantity,
        decimal unitPrice,
        string? variantId = null,
        string? variantName = null,
        string? sku = null,
        string? options = null,
        decimal? weight = null,
        decimal? compareAtPrice = null)
    {
        return new CartItem
        {
            Id = Guid.NewGuid(),
            CartId = cartId,
            ProductId = productId,
            ProductName = productName,
            ProductSlug = productSlug,
            ProductImageUrl = productImageUrl,
            Quantity = quantity,
            UnitPrice = unitPrice,
            CompareAtPrice = compareAtPrice,
            VariantId = variantId,
            VariantName = variantName,
            SKU = sku,
            Options = options,
            Weight = weight,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void SetQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        Quantity = quantity;
        UpdateTimestamp();
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));
        Quantity += amount;
        UpdateTimestamp();
    }

    public void DecreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));
        Quantity = Math.Max(0, Quantity - amount);
        UpdateTimestamp();
    }

    public void UpdatePrice(decimal newPrice)
    {
        UnitPrice = newPrice;
        UpdateTimestamp();
    }

    public void MarkUnavailable(string reason)
    {
        IsAvailable = false;
        UnavailableReason = reason;
        UpdateTimestamp();
    }

    public void MarkAvailable()
    {
        IsAvailable = true;
        UnavailableReason = null;
        UpdateTimestamp();
    }

    public decimal LineTotal => UnitPrice * Quantity;
    public decimal? SavingsAmount => CompareAtPrice.HasValue ? (CompareAtPrice.Value - UnitPrice) * Quantity : null;
}
