using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Shipping;

public class Shipment : BaseEntity
{
    public Guid OrderId { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string? ShippingMethod { get; set; }
    public decimal ShippingCost { get; set; }
    public ShipmentStatus Status { get; set; } = ShipmentStatus.Preparing;
    public DateTime? EstimatedDelivery { get; set; }
    public DateTime? ActualDelivery { get; set; }
    public DateTime? ShippedAt { get; set; }
    public string? Notes { get; set; }
    public string? ShippingAddress { get; set; }
    public string? ReturnAddress { get; set; }
    public Guid? WarehouseId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public virtual Ordering.Order Order { get; set; } = null!;
    public virtual Inventory.Warehouse? Warehouse { get; set; }
    public virtual ICollection<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();
    public virtual ICollection<ShipmentEvent> Events { get; set; } = new List<ShipmentEvent>();
}

public class ShipmentItem : BaseEntity
{
    public Guid ShipmentId { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Shipment Shipment { get; set; } = null!;
    public virtual Ordering.OrderItem OrderItem { get; set; } = null!;
}

public class ShipmentEvent : BaseEntity
{
    public Guid ShipmentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Description { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public DateTime EventTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Shipment Shipment { get; set; } = null!;
}

public class ShippingRate : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal BaseCost { get; set; }
    public decimal? CostPerKg { get; set; }
    public decimal? FreeShippingThreshold { get; set; }
    public int? EstimatedDays { get; set; }
    public string? TrackingUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; }
    public string? SupportedCountries { get; set; } // JSON array
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum ShipmentStatus
{
    Preparing = 0,
    ReadyForPickup = 1,
    PickedUp = 2,
    InTransit = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Failed = 6,
    Returned = 7,
    Cancelled = 8
}
