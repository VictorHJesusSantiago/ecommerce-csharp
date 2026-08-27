namespace Ecommerce.Domain.Events.Inventory;

public class StockLowEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int CurrentStock { get; }
    public int Threshold { get; }

    public StockLowEvent(Guid productId, string productName, int currentStock, int threshold)
    {
        ProductId = productId;
        ProductName = productName;
        CurrentStock = currentStock;
        Threshold = threshold;
    }
}

public class StockOutEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }

    public StockOutEvent(Guid productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

public class StockReplenishedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int QuantityAdded { get; }
    public int NewTotal { get; }

    public StockReplenishedEvent(Guid productId, string productName, int quantityAdded, int newTotal)
    {
        ProductId = productId;
        ProductName = productName;
        QuantityAdded = quantityAdded;
        NewTotal = newTotal;
    }
}

public class StockTransferredEvent : DomainEvent
{
    public Guid ProductId { get; }
    public Guid FromWarehouseId { get; }
    public Guid ToWarehouseId { get; }
    public int Quantity { get; }

    public StockTransferredEvent(Guid productId, Guid fromWarehouseId, Guid toWarehouseId, int quantity)
    {
        ProductId = productId;
        FromWarehouseId = fromWarehouseId;
        ToWarehouseId = toWarehouseId;
        Quantity = quantity;
    }
}

public class InventoryAdjustedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int OldQuantity { get; }
    public int NewQuantity { get; }
    public string Reason { get; }

    public InventoryAdjustedEvent(Guid productId, int oldQuantity, int newQuantity, string reason)
    {
        ProductId = productId;
        OldQuantity = oldQuantity;
        NewQuantity = newQuantity;
        Reason = reason;
    }
}
