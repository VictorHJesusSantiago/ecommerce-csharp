using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.Payment;

public class PaymentRecord : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public Guid? CustomerId { get; private set; }
    public string TransactionId { get; private set; } = string.Empty;
    public string? GatewayTransactionId { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentGateway Gateway { get; private set; }
    public PaymentStatus Status { get; private set; }
    public decimal Amount { get; private set; }
    public decimal? ProcessedAmount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public decimal? ExchangeRate { get; private set; }
    public decimal? ConvertedAmount { get; private set; }
    public string? CardLast4 { get; private set; }
    public string? CardBrand { get; private set; }
    public int? CardExpMonth { get; private set; }
    public int? CardExpYear { get; private set; }
    public string? CardHolderName { get; private set; }
    public string? BillingEmail { get; private set; }
    public string? BillingName { get; private set; }
    public string? FailureReason { get; private set; }
    public string? FailureCode { get; private set; }
    public DateTime? AuthorizedAt { get; private set; }
    public DateTime? CapturedAt { get; private set; }
    public DateTime? VoidedAt { get; private set; }
    public DateTime? FailedAt { get; private set; }
    public string? GatewayResponse { get; private set; }
    public string? MetadataJson { get; private set; }
    public string? IpAddress { get; private set; }
    public bool Is3DSecure { get; private set; }
    public string? ThreeDSecureStatus { get; private set; }

    private readonly List<RefundRecord> _refunds = [];
    public IReadOnlyCollection<RefundRecord> Refunds => _refunds.AsReadOnly();

    private PaymentRecord() { }

    public static PaymentRecord Create(
        Guid orderId,
        PaymentMethod method,
        PaymentGateway gateway,
        decimal amount,
        string currency = "USD",
        Guid? customerId = null,
        string? cardLast4 = null,
        string? cardBrand = null,
        int? cardExpMonth = null,
        int? cardExpYear = null,
        string? cardHolderName = null,
        string? billingEmail = null,
        string? billingName = null,
        string? ipAddress = null)
    {
        return new PaymentRecord
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            CustomerId = customerId,
            TransactionId = $"TXN-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..8]}",
            Method = method,
            Gateway = gateway,
            Status = PaymentStatus.Pending,
            Amount = amount,
            Currency = currency,
            CardLast4 = cardLast4,
            CardBrand = cardBrand,
            CardExpMonth = cardExpMonth,
            CardExpYear = cardExpYear,
            CardHolderName = cardHolderName?.Trim(),
            BillingEmail = billingEmail?.Trim(),
            BillingName = billingName?.Trim(),
            IpAddress = ipAddress,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Authorize(string gatewayTransactionId, string? responseJson = null)
    {
        Status = PaymentStatus.Authorized;
        GatewayTransactionId = gatewayTransactionId;
        GatewayResponse = responseJson;
        AuthorizedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Capture(decimal? amount = null, string? responseJson = null)
    {
        if (Status != PaymentStatus.Authorized)
            throw new InvalidDomainOperationException("Payment must be authorized before capture.");

        ProcessedAmount = amount ?? Amount;
        Status = PaymentStatus.Captured;
        GatewayResponse = responseJson;
        CapturedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Void(string? responseJson = null)
    {
        if (Status != PaymentStatus.Authorized)
            throw new InvalidDomainOperationException("Only authorized payments can be voided.");

        Status = PaymentStatus.Voided;
        GatewayResponse = responseJson;
        VoidedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Fail(string reason, string? code = null, string? responseJson = null)
    {
        Status = PaymentStatus.Failed;
        FailureReason = reason;
        FailureCode = code;
        GatewayResponse = responseJson;
        FailedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Expire()
    {
        if (Status == PaymentStatus.Pending || Status == PaymentStatus.Authorized)
        {
            Status = PaymentStatus.Expired;
            UpdateTimestamp();
        }
    }

    public RefundRecord ProcessRefund(decimal amount, RefundReason reason, string? notes = null)
    {
        if (Status != PaymentStatus.Captured && Status != PaymentStatus.PartiallyCaptured)
            throw new InvalidDomainOperationException("Can only refund captured payments.");

        var totalRefunded = _refunds
            .Where(r => r.Status == RefundStatus.Completed)
            .Sum(r => r.Amount);

        if (totalRefunded + amount > Amount)
            throw new InvalidDomainOperationException($"Refund amount exceeds available amount. Available: {Amount - totalRefunded}");

        var refund = RefundRecord.Create(Id, OrderId, amount, Currency, reason, notes);
        _refunds.Add(refund);

        if (totalRefunded + amount >= Amount)
            Status = PaymentStatus.Refunded;
        else
            Status = PaymentStatus.PartiallyRefunded;

        UpdateTimestamp();
        return refund;
    }

    public bool IsRefundable => Status == PaymentStatus.Captured || Status == PaymentStatus.PartiallyRefunded;
    public decimal RefundableAmount => Amount - (_refunds.Where(r => r.Status == RefundStatus.Completed).Sum(r => r.Amount));
}

public class RefundRecord : BaseEntity
{
    public Guid PaymentId { get; private set; }
    public Guid OrderId { get; private set; }
    public string RefundTransactionId { get; private set; } = string.Empty;
    public string? GatewayRefundId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public RefundStatus Status { get; private set; }
    public RefundReason Reason { get; private set; }
    public string? Notes { get; private set; }
    public string? AdminNotes { get; private set; }
    public string? FailureReason { get; private set; }
    public string? ProcessedBy { get; private set; }
    public string? GatewayResponse { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public string? RefundMethod { get; private set; }
    public PaymentRecord Payment { get; private set; } = null!;

    private RefundRecord() { }

    public static RefundRecord Create(
        Guid paymentId,
        Guid orderId,
        decimal amount,
        string currency,
        RefundReason reason,
        string? notes = null)
    {
        return new RefundRecord
        {
            Id = Guid.NewGuid(),
            PaymentId = paymentId,
            OrderId = orderId,
            RefundTransactionId = $"REF-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..8]}",
            Amount = amount,
            Currency = currency,
            Status = RefundStatus.Pending,
            Reason = reason,
            Notes = notes?.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Approve(string? approvedBy = null)
    {
        Status = RefundStatus.Approved;
        ProcessedBy = approvedBy;
        UpdateTimestamp();
    }

    public void Process(string gatewayRefundId, string? responseJson = null)
    {
        Status = RefundStatus.Processing;
        GatewayRefundId = gatewayRefundId;
        GatewayResponse = responseJson;
        UpdateTimestamp();
    }

    public void Complete(string? responseJson = null)
    {
        Status = RefundStatus.Completed;
        GatewayResponse = responseJson;
        ProcessedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Reject(string? reason = null, string? rejectedBy = null)
    {
        Status = RefundStatus.Rejected;
        FailureReason = reason;
        ProcessedBy = rejectedBy;
        UpdateTimestamp();
    }

    public void Fail(string reason, string? responseJson = null)
    {
        Status = RefundStatus.Failed;
        FailureReason = reason;
        GatewayResponse = responseJson;
        UpdateTimestamp();
    }
}
