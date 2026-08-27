namespace Ecommerce.Domain.Events.Catalog;

public class ProductCreatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public decimal Price { get; }
    public int StockQuantity { get; }

    public ProductCreatedEvent(Guid productId, string productName, decimal price, int stockQuantity)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        StockQuantity = stockQuantity;
    }
}

public class ProductUpdatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public decimal? OldPrice { get; }
    public decimal? NewPrice { get; }

    public ProductUpdatedEvent(Guid productId, string productName, decimal? oldPrice, decimal? newPrice)
    {
        ProductId = productId;
        ProductName = productName;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

public class ProductDeletedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }

    public ProductDeletedEvent(Guid productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

public class ProductStockChangedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int OldQuantity { get; }
    public int NewQuantity { get; }

    public ProductStockChangedEvent(Guid productId, string productName, int oldQuantity, int newQuantity)
    {
        ProductId = productId;
        ProductName = productName;
        OldQuantity = oldQuantity;
        NewQuantity = newQuantity;
    }
}

public class ProductPriceChangedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }

    public ProductPriceChangedEvent(Guid productId, string productName, decimal oldPrice, decimal newPrice)
    {
        ProductId = productId;
        ProductName = productName;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

public class CategoryCreatedEvent : DomainEvent
{
    public Guid CategoryId { get; }
    public string CategoryName { get; }

    public CategoryCreatedEvent(Guid categoryId, string categoryName)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
    }
}

public class CategoryUpdatedEvent : DomainEvent
{
    public Guid CategoryId { get; }
    public string CategoryName { get; }

    public CategoryUpdatedEvent(Guid categoryId, string categoryName)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
    }
}

public class CategoryDeletedEvent : DomainEvent
{
    public Guid CategoryId { get; }
    public string CategoryName { get; }

    public CategoryDeletedEvent(Guid categoryId, string categoryName)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
    }
}
