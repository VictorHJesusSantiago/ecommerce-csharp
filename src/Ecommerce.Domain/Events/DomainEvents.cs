namespace Ecommerce.Domain.Events;

public abstract class DomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string EventType => GetType().Name;
}

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class;
}

namespace Ecommerce.Domain.Events.CatalogEvents
{
    public class ProductCreatedEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class ProductUpdatedEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }

    public class ProductDeletedEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }

    public class ProductPublishedEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }

    public class ProductStockChangedEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
    }

    public class CategoryCreatedEvent : DomainEvent
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}

namespace Ecommerce.Domain.Events.OrderingEvents
{
    public class OrderPlacedEvent : DomainEvent
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderStatusChangedEvent : DomainEvent
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string OldStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
    }

    public class OrderCancelledEvent : DomainEvent
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }
}

namespace Ecommerce.Domain.Events.PaymentEvents
{
    public class PaymentProcessedEvent : DomainEvent
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class PaymentFailedEvent : DomainEvent
    {
        public Guid OrderId { get; set; }
        public string Error { get; set; } = string.Empty;
    }

    public class RefundProcessedEvent : DomainEvent
    {
        public Guid RefundId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}

namespace Ecommerce.Domain.Events.UserEvents
{
    public class UserRegisteredEvent : DomainEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }

    public class UserLoggedInEvent : DomainEvent
    {
        public Guid UserId { get; set; }
        public string? IpAddress { get; set; }
    }

    public class UserPasswordChangedEvent : DomainEvent
    {
        public Guid UserId { get; set; }
    }
}

namespace Ecommerce.Domain.Events.InventoryEvents
{
    public class StockAdjustedEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public Guid? WarehouseId { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    public class LowStockAlertEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int Threshold { get; set; }
    }
}
