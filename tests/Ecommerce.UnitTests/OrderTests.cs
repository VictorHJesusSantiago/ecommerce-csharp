using Xunit;
using FluentAssertions;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.User;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Domain.Factories;

namespace Ecommerce.UnitTests.Domain;

public class OrderTests
{
    [Fact]
    public void Order_Create_ShouldSetProperties()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.OrderNumber.Should().Be("ORD-001");
        order.Status.Should().Be("Pending");
        order.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Order_AddItem_ShouldAddItem()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var item = new OrderItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 49.99m,
            Quantity = 2
        };

        order.Items.Add(item);

        order.Items.Should().HaveCount(1);
        order.Items.First().ProductName.Should().Be("Test Product");
    }

    [Fact]
    public void Order_CalculateTotal_ShouldCalculateCorrectly()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        order.Items.Add(new OrderItem { UnitPrice = 49.99m, Quantity = 2 });
        order.Items.Add(new OrderItem { UnitPrice = 29.99m, Quantity = 1 });
        order.TaxAmount = 10.00m;
        order.ShippingCost = 9.99m;

        var total = order.Items.Sum(i => i.UnitPrice * i.Quantity) + order.TaxAmount + order.ShippingCost;

        total.Should().Be(149.96m);
    }

    [Fact]
    public void Order_UpdateStatus_ShouldUpdateStatus()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.Status = "Processing";

        order.Status.Should().Be("Processing");
    }

    [Fact]
    public void Order_SetShippingAddress_ShouldSetAddress()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var addressId = Guid.NewGuid();

        order.ShippingAddressId = addressId;

        order.ShippingAddressId.Should().Be(addressId);
    }

    [Fact]
    public void Order_SetBillingAddress_ShouldSetAddress()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var addressId = Guid.NewGuid();

        order.BillingAddressId = addressId;

        order.BillingAddressId.Should().Be(addressId);
    }

    [Fact]
    public void Order_SetNotes_ShouldSetNotes()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.Notes = "Please leave at the door";

        order.Notes.Should().Be("Please leave at the door");
    }

    [Fact]
    public void Order_SetCouponCode_ShouldSetCouponCode()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.CouponCode = "SAVE20";

        order.CouponCode.Should().Be("SAVE20");
    }

    [Fact]
    public void Order_SetDiscountAmount_ShouldSetDiscount()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.DiscountAmount = 10.00m;

        order.DiscountAmount.Should().Be(10.00m);
    }

    [Fact]
    public void Order_AddHistory_ShouldAddHistoryEntry()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var history = new OrderHistory
        {
            Status = "Processing",
            Comment = "Order confirmed"
        };

        order.History.Add(history);

        order.History.Should().HaveCount(1);
        order.History.First().Status.Should().Be("Processing");
    }

    [Fact]
    public void Order_RemoveItem_ShouldRemoveItem()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var item = new OrderItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 49.99m,
            Quantity = 1
        };
        order.Items.Add(item);

        order.Items.Remove(item);

        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void Order_UpdateItemQuantity_ShouldUpdateQuantity()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var item = new OrderItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 49.99m,
            Quantity = 1
        };
        order.Items.Add(item);

        item.Quantity = 5;

        item.Quantity.Should().Be(5);
    }

    [Fact]
    public void Order_SetPaymentMethod_ShouldSetPaymentMethod()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.PaymentMethod = "CreditCard";

        order.PaymentMethod.Should().Be("CreditCard");
    }

    [Fact]
    public void Order_SetPaymentStatus_ShouldSetPaymentStatus()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.PaymentStatus = "Paid";

        order.PaymentStatus.Should().Be("Paid");
    }

    [Fact]
    public void Order_SetShippedAt_ShouldSetShippedDate()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var shippedAt = DateTime.UtcNow;

        order.ShippedAt = shippedAt;

        order.ShippedAt.Should().Be(shippedAt);
    }

    [Fact]
    public void Order_SetDeliveredAt_ShouldSetDeliveredDate()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var deliveredAt = DateTime.UtcNow;

        order.DeliveredAt = deliveredAt;

        order.DeliveredAt.Should().Be(deliveredAt);
    }

    [Fact]
    public void Order_SetCancelledAt_ShouldSetCancelledDate()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var cancelledAt = DateTime.UtcNow;

        order.CancelledAt = cancelledAt;

        order.CancelledAt.Should().Be(cancelledAt);
    }

    [Fact]
    public void Order_SetCancellationReason_ShouldSetReason()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.CancellationReason = "Customer changed mind";

        order.CancellationReason.Should().Be("Customer changed mind");
    }

    [Fact]
    public void Order_SetTrackingNumber_ShouldSetTrackingNumber()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.TrackingNumber = "1Z999AA10123456784";

        order.TrackingNumber.Should().Be("1Z999AA10123456784");
    }

    [Fact]
    public void Order_SetCarrier_ShouldSetCarrier()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.Carrier = "UPS";

        order.Carrier.Should().Be("UPS");
    }
}
