namespace Ecommerce.Application.DTOs.Order;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? TransactionId { get; set; }
    public string? GatewayResponse { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public DateTime ProcessedAt { get; set; }
    public DateTime? RefundedAt { get; set; }
    public decimal? RefundedAmount { get; set; }
    public string? RefundReason { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class ProcessPaymentRequest
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentMethod { get; set; } = string.Empty;
    public string? PaymentToken { get; set; }
    public string? CardNumber { get; set; }
    public int? ExpiryMonth { get; set; }
    public int? ExpiryYear { get; set; }
    public string? Cvv { get; set; }
    public string? CardHolderName { get; set; }
    public string? BillingAddressId { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
    public bool SavePaymentMethod { get; set; }
}

public class RefundDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string Status { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public string? TransactionId { get; set; }
    public string? GatewayResponse { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public string? RefundedBy { get; set; }
    public DateTime ProcessedAt { get; set; }
    public List<RefundItemDto> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class RefundItemDto
{
    public Guid OrderItemId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
}

public class ProcessRefundRequest
{
    public Guid PaymentId { get; set; }
    public decimal? Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public bool RestockItems { get; set; } = true;
    public string? Notes { get; set; }
    public List<RefundItemRequest> Items { get; set; } = [];
}

public class RefundItemRequest
{
    public Guid OrderItemId { get; set; }
    public int Quantity { get; set; }
}

public class PaymentIntentDto
{
    public string Id { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}

public class PaymentSettingsDto
{
    public List<SupportedPaymentMethodDto> SupportedMethods { get; set; } = [];
    public string DefaultCurrency { get; set; } = "USD";
    public decimal MinimumAmount { get; set; }
    public decimal MaximumAmount { get; set; }
    public bool IsTestMode { get; set; }
    public List<string> SupportedCurrencies { get; set; } = [];
}

public class SupportedPaymentMethodDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
    public bool IsEnabled { get; set; }
    public decimal? ProcessingFee { get; set; }
    public string? ProcessingFeeType { get; set; }
    public List<string> SupportedCurrencies { get; set; } = [];
    public bool RequiresCvv { get; set; }
    public bool SupportsRecurring { get; set; }
    public bool SupportsRefunds { get; set; }
    public int? MaxRefundDays { get; set; }
}

public class PaymentWebhookDto
{
    public string EventType { get; set; } = string.Empty;
    public string PaymentId { get; set; } = string.Empty;
    public string? OrderId { get; set; }
    public decimal? Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public Dictionary<string, string> Data { get; set; } = [];
    public DateTime Timestamp { get; set; }
}
