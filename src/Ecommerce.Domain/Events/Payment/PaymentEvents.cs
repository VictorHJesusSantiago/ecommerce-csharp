namespace Ecommerce.Domain.Events.Payment;

public class PaymentProcessedEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public Guid OrderId { get; }
    public decimal Amount { get; }
    public string Currency { get; }
    public string Gateway { get; }

    public PaymentProcessedEvent(Guid paymentId, Guid orderId, decimal amount, string currency, string gateway)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
        Currency = currency;
        Gateway = gateway;
    }
}

public class PaymentFailedEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public Guid OrderId { get; }
    public string Reason { get; }

    public PaymentFailedEvent(Guid paymentId, Guid orderId, string reason)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Reason = reason;
    }
}

public class RefundProcessedEvent : DomainEvent
{
    public Guid RefundId { get; }
    public Guid OrderId { get; }
    public Guid PaymentId { get; }
    public decimal Amount { get; }
    public string Reason { get; }

    public RefundProcessedEvent(Guid refundId, Guid orderId, Guid paymentId, decimal amount, string reason)
    {
        RefundId = refundId;
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
        Reason = reason;
    }
}

public class RefundFailedEvent : DomainEvent
{
    public Guid RefundId { get; }
    public string Reason { get; }

    public RefundFailedEvent(Guid refundId, string reason)
    {
        RefundId = refundId;
        Reason = reason;
    }
}
