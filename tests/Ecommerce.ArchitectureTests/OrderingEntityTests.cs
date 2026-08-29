using FluentAssertions;
using Xunit;
using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.ArchitectureTests;

public class OrderingEntityTests
{
    [Fact]
    public void Order_ShouldHaveDefaultValues()
    {
        var order = new Order();
        order.Status.Should().Be(OrderStatus.Pending);
        order.Items.Should().NotBeNull();
        order.Payments.Should().NotBeNull();
        order.Refunds.Should().NotBeNull();
        order.History.Should().NotBeNull();
        order.Notes.Should().NotBeNull();
    }

    [Fact]
    public void Order_ShouldCalculateSubTotal()
    {
        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { TotalPrice = 100m },
                new() { TotalPrice = 50m },
                new() { TotalPrice = 25m }
            }
        };
        order.SubTotal.Should().Be(175m);
    }

    [Fact]
    public void Order_ShouldCalculateTotalAmount()
    {
        var order = new Order
        {
            Items = new List<OrderItem> { new() { TotalPrice = 100m } },
            ShippingCost = 10m,
            TaxAmount = 8m,
            DiscountAmount = 5m
        };
        order.TotalAmount.Should().Be(113m);
    }

    [Fact]
    public void Order_ShouldSupportCancellation()
    {
        var order = new Order { Status = OrderStatus.Pending };
        order.Status.Should().Be(OrderStatus.Pending);
        order.Status = OrderStatus.Cancelled;
        order.Status.Should().Be(OrderStatus.Cancelled);
    }

    [Fact]
    public void OrderItem_ShouldCalculateTotalPrice()
    {
        var item = new OrderItem
        {
            UnitPrice = 29.99m,
            Quantity = 3
        };
        item.TotalPrice.Should().Be(89.97m);
    }

    [Fact]
    public void OrderHistory_ShouldRecordStatusChange()
    {
        var history = new OrderHistory
        {
            OrderId = Guid.NewGuid(),
            Status = OrderStatus.Processing,
            Comment = "Order confirmed",
            ChangedAt = DateTime.UtcNow,
            ChangedBy = "admin@example.com"
        };

        history.Status.Should().Be(OrderStatus.Processing);
        history.ChangedBy.Should().Be("admin@example.com");
    }

    [Fact]
    public void OrderNote_ShouldStoreContent()
    {
        var note = new OrderNote
        {
            OrderId = Guid.NewGuid(),
            Content = "Customer requested gift wrap",
            IsInternal = false,
            CreatedBy = "staff@example.com",
            CreatedAt = DateTime.UtcNow
        };

        note.Content.Should().Be("Customer requested gift wrap");
        note.IsInternal.Should().BeFalse();
    }

    [Fact]
    public void ShoppingCart_ShouldTrackItems()
    {
        var cart = new ShoppingCart
        {
            UserId = Guid.NewGuid(),
            Items = new List<CartItem>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 2 },
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        cart.Items.Should().HaveCount(2);
    }

    [Fact]
    public void PaymentRecord_ShouldTrackTransaction()
    {
        var payment = new PaymentRecord
        {
            OrderId = Guid.NewGuid(),
            Amount = 99.99m,
            Currency = "USD",
            PaymentMethod = "Credit Card",
            TransactionId = "txn_123",
            Status = PaymentStatus.Completed,
            ProcessedAt = DateTime.UtcNow
        };

        payment.Status.Should().Be(PaymentStatus.Completed);
        payment.TransactionId.Should().Be("txn_123");
    }

    [Fact]
    public void RefundRecord_ShouldTrackRefund()
    {
        var refund = new RefundRecord
        {
            OrderId = Guid.NewGuid(),
            PaymentId = Guid.NewGuid(),
            Amount = 25m,
            Reason = "Product returned",
            Status = RefundStatus.Processing,
            ProcessedAt = DateTime.UtcNow
        };

        refund.Status.Should().Be(RefundStatus.Processing);
        refund.Amount.Should().Be(25m);
    }
}
