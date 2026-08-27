namespace Ecommerce.Application.DTOs.Payment;

public class PaymentGatewaySettingsDto
{
    public bool IsStripeEnabled { get; set; }
    public bool IsPayPalEnabled { get; set; }
    public bool IsSquareEnabled { get; set; }
    public bool IsManualPaymentEnabled { get; set; }
    public string DefaultGateway { get; set; } = string.Empty;
    public StripeSettingsDto? Stripe { get; set; }
    public PayPalSettingsDto? PayPal { get; set; }
    public SquareSettingsDto? Square { get; set; }
    public ManualPaymentSettingsDto? ManualPayment { get; set; }
    public List<string> SupportedCurrencies { get; set; } = [];
    public string DefaultCurrency { get; set; } = "USD";
}

public class StripeSettingsDto
{
    public string PublishableKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
    public bool IsTestMode { get; set; }
    public string? TestPublishableKey { get; set; }
    public string? TestSecretKey { get; set; }
    public string? TestWebhookSecret { get; set; }
    public bool CaptureMethod { get; set; }
    public int PaymentTimeout { get; set; } = 30;
    public List<string> SupportedPaymentMethods { get; set; } = [];
    public bool EnableApplePay { get; set; }
    public bool EnableGooglePay { get; set; }
    public bool EnableLinkPay { get; set; }
    public bool Enable3DSecure { get; set; }
}

public class PayPalSettingsDto
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string WebhookId { get; set; } = string.Empty;
    public bool IsSandbox { get; set; }
    public string? SandboxClientId { get; set; }
    public string? SandboxClientSecret { get; set; }
    public string? SandboxWebhookId { get; set; }
    public string Environment { get; set; } = "production";
    public bool EnablePayPalCredit { get; set; }
    public bool EnableVenmo { get; set; }
    public bool EnablePayLater { get; set; }
}

public class SquareSettingsDto
{
    public string ApplicationId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string LocationId { get; set; } = string.Empty;
    public string WebhookSignatureKey { get; set; } = string.Empty;
    public bool IsSandbox { get; set; }
    public string? SandboxApplicationId { get; set; }
    public string? SandboxAccessToken { get; set; }
    public string? SandboxLocationId { get; set; }
}

public class ManualPaymentSettingsDto
{
    public bool Enabled { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public string? BankName { get; set; }
    public string? AccountNumber { get; set; }
    public string? RoutingNumber { get; set; }
    public string? SwiftCode { get; set; }
    public string? Iban { get; set; }
    public string? BeneficiaryName { get; set; }
    public string? BeneficiaryAddress { get; set; }
    public int PaymentDeadlineHours { get; set; } = 48;
    public bool AutoConfirmOnPayment { get; set; }
}

public class ProcessStripePaymentRequest
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentMethodId { get; set; } = string.Empty;
    public string? CustomerEmail { get; set; }
    public string? CustomerName { get; set; }
    public bool CaptureMethod { get; set; } = true;
    public int? ConfirmationDelayMinutes { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
    public bool SavePaymentMethod { get; set; }
    public bool Enable3DSecure { get; set; }
}

public class ProcessPayPalPaymentRequest
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string? ReturnUrl { get; set; }
    public string? CancelUrl { get; set; }
    public string? Description { get; set; }
    public bool EnablePayPalCredit { get; set; }
    public bool EnablePayLater { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}

public class PaymentWebhookEventDto
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Gateway { get; set; } = string.Empty;
    public string? PaymentId { get; set; }
    public string? OrderId { get; set; }
    public decimal? Amount { get; set; }
    public string? Currency { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EventTime { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? FailureReason { get; set; }
    public Dictionary<string, object> Data { get; set; } = [];
}

public class PaymentReconciliationDto
{
    public Guid Id { get; set; }
    public string Gateway { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public Guid? OrderId { get; set; }
    public string? OrderNumber { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? FailureReason { get; set; }
    public bool IsReconciled { get; set; }
    public DateTime? ReconciledAt { get; set; }
    public string? ReconciledBy { get; set; }
    public string? Notes { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PaymentAnalyticsDto
{
    public decimal TotalProcessed { get; set; }
    public int TotalTransactions { get; set; }
    public int SuccessfulTransactions { get; set; }
    public int FailedTransactions { get; set; }
    public int RefundedTransactions { get; set; }
    public decimal TotalRefunded { get; set; }
    public decimal SuccessRate { get; set; }
    public decimal AverageTransactionAmount { get; set; }
    public decimal TotalFees { get; set; }
    public decimal NetRevenue { get; set; }
    public List<PaymentMethodAnalyticsDto> PaymentMethods { get; set; } = [];
    public List<DailyPaymentDto> DailyPayments { get; set; } = [];
    public List<FailedPaymentReasonDto> FailedReasons { get; set; } = [];
}

public class PaymentMethodAnalyticsDto
{
    public string Method { get; set; } = string.Empty;
    public int TransactionCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AverageAmount { get; set; }
    public decimal SuccessRate { get; set; }
    public decimal RefundRate { get; set; }
    public decimal Fees { get; set; }
}

public class DailyPaymentDto
{
    public DateTime Date { get; set; }
    public int TransactionCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal SuccessAmount { get; set; }
    public decimal FailedAmount { get; set; }
    public decimal RefundAmount { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
}

public class FailedPaymentReasonDto
{
    public string Reason { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Percentage { get; set; }
    public string? Recommendation { get; set; }
}

public class PaymentFraudCheckDto
{
    public bool IsSuspicious { get; set; }
    public decimal RiskScore { get; set; }
    public string RiskLevel { get; set; } = string.Empty;
    public List<string> RiskFactors { get; set; } = [];
    public string? Recommendation { get; set; }
    public string? IpAddress { get; set; }
    public string? Country { get; set; }
    public string? DeviceFingerprint { get; set; }
    public bool IsKnownFraudster { get; set; }
    public bool IsVelocityCheckFailed { get; set; }
    public bool IsAddressMismatch { get; set; }
    public bool IsHighValueTransaction { get; set; }
}

public class PaymentScheduleDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public int TotalInstallments { get; set; }
    public int PaidInstallments { get; set; }
    public decimal InstallmentAmount { get; set; }
    public string Frequency { get; set; } = string.Empty;
    public DateTime NextPaymentDate { get; set; }
    public DateTime? FinalPaymentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<PaymentScheduleInstallmentDto> Installments { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}

public class PaymentScheduleInstallmentDto
{
    public int Number { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaidDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? TransactionId { get; set; }
    public decimal? LateFee { get; set; }
    public string? FailureReason { get; set; }
}

public class CreatePaymentScheduleRequest
{
    public Guid OrderId { get; set; }
    public int Installments { get; set; }
    public string Frequency { get; set; } = "Monthly";
    public DateTime? FirstPaymentDate { get; set; }
    public decimal? DownPayment { get; set; }
}
