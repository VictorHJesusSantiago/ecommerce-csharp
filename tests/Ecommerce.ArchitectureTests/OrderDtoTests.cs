using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.ArchitectureTests;

public class OrderDtoTests
{
    [Fact]
    public void OrderDto_ShouldHaveRequiredProperties()
    {
        var dto = new OrderDto
        {
            Id = Guid.NewGuid(),
            OrderNumber = "ORD-00001",
            UserId = Guid.NewGuid(),
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com",
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            PaymentStatus = "Pending",
            SubTotal = 100m,
            ShippingCost = 9.99m,
            TaxAmount = 8.80m,
            DiscountAmount = 10m,
            TotalAmount = 108.79m,
            ShippingAddress = new Application.DTOs.Common.AddressDto
            {
                Street = "123 Main St",
                City = "New York",
                State = "NY",
                PostalCode = "10001",
                Country = "US"
            },
            Items = new List<OrderItemDto>
            {
                new() { ProductName = "Widget", Quantity = 2, UnitPrice = 50m, TotalPrice = 100m }
            },
            TrackingNumber = "TRACK123",
            Carrier = "UPS",
            CouponCode = "SAVE10",
            Notes = "Handle with care",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.OrderNumber.Should().Be("ORD-00001");
        dto.Items.Should().HaveCount(1);
        dto.TotalAmount.Should().Be(108.79m);
    }

    [Fact]
    public void CreateOrderDto_ShouldHaveRequiredProperties()
    {
        var dto = new CreateOrderDto
        {
            ShippingAddressId = Guid.NewGuid(),
            BillingAddressId = Guid.NewGuid(),
            PaymentMethodId = Guid.NewGuid(),
            ShippingMethodId = Guid.NewGuid(),
            CouponCode = "SAVE10",
            Notes = "Gift wrap please"
        };

        dto.ShippingAddressId.Should().NotBeEmpty();
    }

    [Fact]
    public void OrderItemDto_ShouldHaveRequiredProperties()
    {
        var dto = new OrderItemDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Widget",
            ProductImage = "/images/widget.jpg",
            Sku = "WGT-001",
            Quantity = 3,
            UnitPrice = 29.99m,
            TotalPrice = 89.97m,
            IsReviewed = false
        };

        dto.TotalPrice.Should().Be(89.97m);
        dto.IsReviewed.Should().BeFalse();
    }

    [Fact]
    public void OrderStatusHistoryDto_ShouldHaveRequiredProperties()
    {
        var dto = new OrderStatusHistoryDto
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = OrderStatus.Processing,
            Comment = "Order confirmed",
            ChangedAt = DateTime.UtcNow,
            ChangedBy = "admin@example.com"
        };

        dto.Status.Should().Be(OrderStatus.Processing);
        dto.ChangedBy.Should().Be("admin@example.com");
    }

    [Fact]
    public void UpdateOrderStatusDto_ShouldHaveRequiredProperties()
    {
        var dto = new UpdateOrderStatusDto
        {
            Status = OrderStatus.Shipped,
            TrackingNumber = "TRACK-456",
            Carrier = "FedEx",
            Comment = "Shipped via FedEx"
        };

        dto.Status.Should().Be(OrderStatus.Shipped);
        dto.Carrier.Should().Be("FedEx");
    }

    [Fact]
    public void OrderSummaryDto_ShouldHaveRequiredProperties()
    {
        var dto = new OrderSummaryDto
        {
            TotalOrders = 150,
            TotalRevenue = 45000m,
            AverageOrderValue = 300m,
            OrdersInLast24Hours = 12,
            OrdersInLastWeek = 85,
            OrdersInLastMonth = 150
        };

        dto.AverageOrderValue.Should().Be(300m);
    }
}
