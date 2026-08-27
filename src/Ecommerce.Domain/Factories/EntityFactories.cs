using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Domain.Factories;

public class ProductFactory
{
    public Product Create(string name, decimal price, int stockQuantity, string sku, Guid? categoryId = null)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            StockQuantity = stockQuantity,
            Sku = sku,
            Slug = GenerateSlug(name),
            CategoryId = categoryId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public Product CreateDigital(string name, decimal price, string sku, string downloadUrl)
    {
        var product = Create(name, price, 0, sku);
        product.IsDigital = true;
        return product;
    }

    private static string GenerateSlug(string name)
    {
        return name.ToLowerInvariant().Replace(" ", "-").Replace("'", "").Replace("\"", "");
    }
}

public class CategoryFactory
{
    public Category Create(string name, Guid? parentId = null, int sortOrder = 0)
    {
        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Slug = name.ToLowerInvariant().Replace(" ", "-"),
            ParentId = parentId,
            SortOrder = sortOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class OrderFactory
{
    public Order Create(Guid userId, string shippingAddress)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            UserId = userId,
            ShippingAddress = shippingAddress,
            Status = Entities.Ordering.OrderStatus.Pending,
            PaymentStatus = Entities.Ordering.PaymentStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class CartFactory
{
    public ShoppingCart Create(Guid? userId = null, string? sessionId = null)
    {
        return new ShoppingCart
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            SessionId = sessionId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
