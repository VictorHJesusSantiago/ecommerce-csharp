namespace Ecommerce.Application.DTOs.Order;

public class OrderExtendedDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string? UserEmail { get; set; }
    public string? UserFullName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? PaymentStatus { get; set; }
    public string? PaymentMethod { get; set; }
    public string? TransactionId { get; set; }
    public AddressDto? ShippingAddress { get; set; }
    public AddressDto? BillingAddress { get; set; }
    public string? Notes { get; set; }
    public string? CouponCode { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public string? CancellationReason { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
    public List<OrderHistoryDto> History { get; set; } = [];
    public List<OrderNoteDto> Notes_List { get; set; } = [];
    public OrderCalculationDto Calculations { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public DateTime? RefundedAt { get; set; }
}

public class OrderNoteDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Note { get; set; } = string.Empty;
    public bool IsInternal { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class OrderCalculationDto
{
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal CouponDiscount { get; set; }
    public decimal PromotionDiscount { get; set; }
    public decimal TotalDiscount => CouponDiscount + PromotionDiscount;
    public decimal GrandTotal { get; set; }
    public decimal? Weight { get; set; }
    public int TotalItems { get; set; }
    public int TotalQuantity { get; set; }
}

public class OrderAnalyticsDto
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal MedianOrderValue { get; set; }
    public int TotalItemsSold { get; set; }
    public decimal AverageItemsPerOrder { get; set; }
    public decimal ConversionRate { get; set; }
    public decimal CartAbandonmentRate { get; set; }
    public decimal ReturnRate { get; set; }
    public decimal RefundRate { get; set; }
    public decimal CancellationRate { get; set; }
    public List<OrderStatusBreakdownDto> StatusBreakdown { get; set; } = [];
    public List<PaymentMethodBreakdownDto> PaymentMethodBreakdown { get; set; } = [];
    public List<DailyOrderDto> DailyOrders { get; set; } = [];
    public List<HourlyOrderDto> HourlyOrders { get; set; } = [];
    public List<TopProductDto> TopProducts { get; set; } = [];
}

public class OrderStatusBreakdownDto
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
    public decimal Revenue { get; set; }
}

public class PaymentMethodBreakdownDto
{
    public string Method { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AverageAmount { get; set; }
}

public class DailyOrderDto
{
    public DateTime Date { get; set; }
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
    public int ItemsSold { get; set; }
    public decimal AverageOrderValue { get; set; }
    public int NewCustomers { get; set; }
    public int ReturningCustomers { get; set; }
}

public class HourlyOrderDto
{
    public int Hour { get; set; }
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
}

public class TopProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public decimal ProfitMargin { get; set; }
}

public class OrderExportDto
{
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Shipping { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public string Items { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class OrderTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public List<OrderTemplateItemDto> Items { get; set; } = [];
    public string? DefaultShippingAddressId { get; set; }
    public string? DefaultPaymentMethodId { get; set; }
    public string? Notes { get; set; }
    public int UsageCount { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class OrderTemplateItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Sku { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public Guid? VariantId { get; set; }
    public string? VariantName { get; set; }
}

public class CreateOrderTemplateRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<OrderTemplateItemDto> Items { get; set; } = [];
    public string? DefaultShippingAddressId { get; set; }
    public string? DefaultPaymentMethodId { get; set; }
    public string? Notes { get; set; }
}

public class OrderFraudCheckDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public bool IsSuspicious { get; set; }
    public decimal RiskScore { get; set; }
    public string RiskLevel { get; set; } = string.Empty;
    public List<string> RiskFactors { get; set; } = [];
    public string? Recommendation { get; set; }
    public string? IpAddress { get; set; }
    public string? BillingCountry { get; set; }
    public string? ShippingCountry { get; set; }
    public bool IsAddressMismatch { get; set; }
    public bool IsHighValueOrder { get; set; }
    public bool IsVelocityCheckFailed { get; set; }
    public bool IsKnownFraudster { get; set; }
    public DateTime? CheckedAt { get; set; }
    public string? CheckedBy { get; set; }
}

public class OrderStatusHistoryDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string OldStatus { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsCustomerVisible { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class BulkOrderUpdateRequest
{
    public List<Guid> OrderIds { get; set; } = [];
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public bool NotifyCustomer { get; set; } = true;
}

public class BulkOrderUpdateResult
{
    public int TotalOrders { get; set; }
    public int SuccessfulUpdates { get; set; }
    public int FailedUpdates { get; set; }
    public List<string> Errors { get; set; } = [];
    public List<Guid> UpdatedOrderIds { get; set; } = [];
    public DateTime CompletedAt { get; set; }
}
