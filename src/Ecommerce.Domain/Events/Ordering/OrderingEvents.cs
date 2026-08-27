namespace Ecommerce.Domain.Events.Ordering;

public class OrderCreatedEvent : DomainEvent
{
    public Guid OrderId { get; }
    public string OrderNumber { get; }
    public Guid CustomerId { get; }
    public decimal TotalAmount { get; }

    public OrderCreatedEvent(Guid orderId, string orderNumber, Guid customerId, decimal totalAmount)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        CustomerId = customerId;
        TotalAmount = totalAmount;
    }
}

public class OrderStatusChangedEvent : DomainEvent
{
    public Guid OrderId { get; }
    public string OrderNumber { get; }
    public string OldStatus { get; }
    public string NewStatus { get; }

    public OrderStatusChangedEvent(Guid orderId, string orderNumber, string oldStatus, string newStatus)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }
}

public class OrderCancelledEvent : DomainEvent
{
    public Guid OrderId { get; }
    public string OrderNumber { get; }
    public string Reason { get; }

    public OrderCancelledEvent(Guid orderId, string orderNumber, string reason)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        Reason = reason;
    }
}

public class OrderShippedEvent : DomainEvent
{
    public Guid OrderId { get; }
    public string OrderNumber { get; }
    public string TrackingNumber { get; }
    public string Carrier { get; }

    public OrderShippedEvent(Guid orderId, string orderNumber, string trackingNumber, string carrier)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        TrackingNumber = trackingNumber;
        Carrier = carrier;
    }
}

public class OrderDeliveredEvent : DomainEvent
{
    public Guid OrderId { get; }
    public string OrderNumber { get; }
    public DateTime DeliveredAt { get; }

    public OrderDeliveredEvent(Guid orderId, string orderNumber, DateTime deliveredAt)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        DeliveredAt = deliveredAt;
    }
}

public class OrderRefundedEvent : DomainEvent
{
    public Guid OrderId { get; }
    public string OrderNumber { get; }
    public decimal RefundAmount { get; }
    public string Reason { get; }

    public OrderRefundedEvent(Guid orderId, string orderNumber, decimal refundAmount, string reason)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        RefundAmount = refundAmount;
        Reason = reason;
    }
}
