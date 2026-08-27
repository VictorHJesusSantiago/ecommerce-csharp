using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Ordering;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CouponCode { get; set; }
    public string? Notes { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? TrackingNumber { get; set; }
    public string? ShippingCarrier { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public string? CancelledReason { get; set; }

    public virtual User.ApplicationUser User { get; set; } = null!;
    public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public virtual ICollection<OrderHistory> StatusHistory { get; set; } = new List<OrderHistory>();
    public virtual ICollection<OrderNote> Notes2 { get; set; } = new List<OrderNote>();
    public virtual PaymentRecord? PaymentRecord { get; set; }
    public virtual Shipment? Shipment { get; set; }

    public int ItemCount => Items.Count;
    public bool CanBeCancelled => Status == OrderStatus.Pending || Status == OrderStatus.Processing;
    public bool CanBeRefunded => PaymentStatus == PaymentStatus.Paid && Status != OrderStatus.Cancelled;

    public void Cancel(string? reason = null)
    {
        if (!CanBeCancelled)
            throw new InvalidOperationException("Order cannot be cancelled in current status.");
        Status = OrderStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancelledReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid(string? paymentIntentId = null)
    {
        PaymentStatus = PaymentStatus.Paid;
        PaymentIntentId = paymentIntentId;
        PaidAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Order Order { get; set; } = null!;
    public virtual Catalog.Product Product { get; set; } = null!;
}

public class OrderHistory : BaseEntity
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Order Order { get; set; } = null!;
}

public class OrderNote : BaseEntity
{
    public Guid OrderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsInternal { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Order Order { get; set; } = null!;
}

public class ShoppingCart : BaseEntity
{
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public string? CouponCode { get; set; }
    public decimal? DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public virtual User.ApplicationUser? User { get; set; }
    public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal SubTotal => Items.Sum(i => i.TotalPrice);
    public decimal Total => SubTotal - (DiscountAmount ?? 0);
    public int TotalItems => Items.Sum(i => i.Quantity);
}

public class CartItem : BaseEntity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public string? VariantId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public virtual ShoppingCart Cart { get; set; } = null!;
    public virtual Catalog.Product Product { get; set; } = null!;
}

public class PaymentRecord : BaseEntity
{
    public Guid OrderId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string? PaymentGateway { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? CardLastFour { get; set; }
    public string? CardType { get; set; }
    public string? PayPalEmail { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}

public class RefundRecord : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid PaymentRecordId { get; set; }
    public string RefundId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
    public RefundStatus Status { get; set; } = RefundStatus.Pending;
    public string? ProcessedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual PaymentRecord PaymentRecord { get; set; } = null!;
}

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Processing = 2,
    Shipped = 3,
    InTransit = 4,
    Delivered = 5,
    Cancelled = 6,
    Refunded = 7,
    OnHold = 8,
    Failed = 9
}

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Paid = 2,
    Failed = 3,
    Refunded = 4,
    PartiallyRefunded = 5,
    Cancelled = 6
}

public enum RefundStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Rejected = 3,
    Failed = 4
}
