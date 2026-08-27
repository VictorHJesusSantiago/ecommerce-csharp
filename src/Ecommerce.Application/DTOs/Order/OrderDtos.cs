namespace Ecommerce.Application.DTOs.Order;

public class OrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
    public string? Notes { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? TrackingNumber { get; set; }
    public string? ShippingCarrier { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
    public List<OrderHistoryDto> StatusHistory { get; set; } = [];
}

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? DiscountAmount { get; set; }
}

public class OrderHistoryDto
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PlaceOrderRequest
{
    public string ShippingAddress { get; set; } = string.Empty;
    public string? BillingAddress { get; set; }
    public string PaymentMethod { get; set; } = "card";
    public string? Notes { get; set; }
    public string? CouponCode { get; set; }
    public decimal? DiscountAmount { get; set; }
    public bool SaveAddress { get; set; }
    public bool UseDefaultAddress { get; set; }
}

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
    public string? Notes { get; set; }
    public string? TrackingNumber { get; set; }
    public string? ShippingCarrier { get; set; }
}

public class OrderSearchRequest
{
    public Guid? UserId { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? SearchQuery { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
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
