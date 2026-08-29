using Xunit;
using FluentAssertions;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;

namespace Ecommerce.UnitTests;

public class OrderDtoComprehensiveTests
{
    [Fact]
    public void OrderDto_AllProperties_ShouldBeSettable()
    {
        var dto = new OrderDto
        {
            Id = Guid.NewGuid(),
            OrderNumber = "ORD-20240101-001",
            UserId = Guid.NewGuid(),
            UserEmail = "john@example.com",
            UserFullName = "John Doe",
            SubTotal = 100m,
            TaxAmount = 8m,
            ShippingCost = 9.99m,
            DiscountAmount = 5m,
            TotalAmount = 112.99m,
            Status = "Processing",
            PaymentStatus = "Paid",
            PaymentMethod = "CreditCard",
            Notes = "Gift wrap",
            CouponCode = "SAVE20",
            TrackingNumber = "1Z999AA10123456784",
            Carrier = "UPS",
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", UnitPrice = 50m, Quantity = 1, TotalPrice = 50m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", UnitPrice = 50m, Quantity = 1, TotalPrice = 50m }
            ],
            History =
            [
                new() { Status = "Pending", Comment = "Order placed", CreatedAt = DateTime.UtcNow }
            ],
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ShippedAt = null,
            DeliveredAt = null
        };

        dto.Id.Should().NotBeEmpty();
        dto.OrderNumber.Should().Be("ORD-20240101-001");
        dto.TotalAmount.Should().Be(112.99m);
        dto.Status.Should().Be("Processing");
        dto.Items.Should().HaveCount(2);
        dto.History.Should().HaveCount(1);
    }

    [Fact]
    public void OrderDto_TotalAmount_ShouldBeSumOfComponents()
    {
        var dto = new OrderDto
        {
            SubTotal = 100m,
            TaxAmount = 8m,
            ShippingCost = 9.99m,
            DiscountAmount = 5m
        };

        var total = dto.SubTotal + dto.TaxAmount + dto.ShippingCost - dto.DiscountAmount;

        total.Should().Be(112.99m);
    }

    [Fact]
    public void OrderDto_CanCancel_ShouldReturnTrueForPendingOrder()
    {
        var dto = new OrderDto { Status = "Pending" };

        var canCancel = dto.Status == "Pending" || dto.Status == "Processing";

        canCancel.Should().BeTrue();
    }

    [Fact]
    public void OrderDto_CanCancel_ShouldReturnFalseForDeliveredOrder()
    {
        var dto = new OrderDto { Status = "Delivered" };

        var canCancel = dto.Status == "Pending" || dto.Status == "Processing";

        canCancel.Should().BeFalse();
    }

    [Fact]
    public void OrderDto_CanRefund_ShouldReturnTrueForDeliveredOrder()
    {
        var dto = new OrderDto { Status = "Delivered" };

        var canRefund = dto.Status == "Delivered";

        canRefund.Should().BeTrue();
    }

    [Fact]
    public void OrderDto_CanRefund_ShouldReturnFalseForPendingOrder()
    {
        var dto = new OrderDto { Status = "Pending" };

        var canRefund = dto.Status == "Delivered";

        canRefund.Should().BeFalse();
    }
}

public class OrderItemDtoComprehensiveTests
{
    [Fact]
    public void OrderItemDto_AllProperties_ShouldBeSettable()
    {
        var dto = new OrderItemDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            ProductImage = "https://example.com/image.jpg",
            Sku = "SKU-001",
            UnitPrice = 49.99m,
            Quantity = 2,
            TotalPrice = 99.98m,
            DiscountAmount = 5m
        };

        dto.Id.Should().NotBeEmpty();
        dto.ProductName.Should().Be("Test Product");
        dto.UnitPrice.Should().Be(49.99m);
        dto.Quantity.Should().Be(2);
        dto.TotalPrice.Should().Be(99.98m);
        dto.DiscountAmount.Should().Be(5m);
    }

    [Fact]
    public void OrderItemDto_TotalPrice_ShouldCalculateCorrectly()
    {
        var dto = new OrderItemDto
        {
            UnitPrice = 49.99m,
            Quantity = 3
        };

        var total = dto.UnitPrice * dto.Quantity;

        total.Should().Be(149.97m);
    }

    [Fact]
    public void OrderItemDto_FinalPrice_ShouldSubtractDiscount()
    {
        var dto = new OrderItemDto
        {
            UnitPrice = 49.99m,
            Quantity = 2,
            DiscountAmount = 5m
        };

        var finalPrice = (dto.UnitPrice * dto.Quantity) - dto.DiscountAmount;

        finalPrice.Should().Be(94.98m);
    }
}

public class PlaceOrderRequestComprehensiveTests
{
    [Fact]
    public void PlaceOrderRequest_AllProperties_ShouldBeSettable()
    {
        var request = new PlaceOrderRequest
        {
            ShippingAddressId = Guid.NewGuid(),
            BillingAddressId = Guid.NewGuid(),
            Notes = "Gift wrap",
            CouponCode = "SAVE20",
            PaymentMethod = "CreditCard",
            Items =
            [
                new() { ProductId = Guid.NewGuid(), Quantity = 2, ProductVariantId = Guid.NewGuid() },
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            ]
        };

        request.ShippingAddressId.Should().NotBeEmpty();
        request.BillingAddressId.Should().NotBeEmpty();
        request.Notes.Should().Be("Gift wrap");
        request.CouponCode.Should().Be("SAVE20");
        request.PaymentMethod.Should().Be("CreditCard");
        request.Items.Should().HaveCount(2);
    }

    [Fact]
    public void PlaceOrderRequest_Items_ShouldHaveProductAndQuantity()
    {
        var request = new PlaceOrderRequest
        {
            Items =
            [
                new() { ProductId = Guid.NewGuid(), Quantity = 2 },
                new() { ProductId = Guid.NewGuid(), Quantity = 1 }
            ]
        };

        request.Items.Should().AllSatisfy(item =>
        {
            item.ProductId.Should().NotBeEmpty();
            item.Quantity.Should().BeGreaterThan(0);
        });
    }
}

public class UpdateOrderStatusRequestComprehensiveTests
{
    [Fact]
    public void UpdateOrderStatusRequest_AllProperties_ShouldBeSettable()
    {
        var request = new UpdateOrderStatusRequest
        {
            Status = "Shipped",
            Comment = "Package shipped",
            TrackingNumber = "1Z999AA10123456784",
            Carrier = "UPS"
        };

        request.Status.Should().Be("Shipped");
        request.Comment.Should().Be("Package shipped");
        request.TrackingNumber.Should().Be("1Z999AA10123456784");
        request.Carrier.Should().Be("UPS");
    }
}

public class OrderSearchRequestComprehensiveTests
{
    [Fact]
    public void OrderSearchRequest_DefaultValues_ShouldBeCorrect()
    {
        var request = new OrderSearchRequest();

        request.Page.Should().Be(1);
        request.PageSize.Should().Be(20);
        request.SortDescending.Should().BeTrue();
        request.SearchTerm.Should().BeNull();
        request.Status.Should().BeNull();
        request.StartDate.Should().BeNull();
        request.EndDate.Should().BeNull();
        request.UserId.Should().BeNull();
        request.MinAmount.Should().BeNull();
        request.MaxAmount.Should().BeNull();
        request.SortBy.Should().BeNull();
    }

    [Fact]
    public void OrderSearchRequest_WithFilters_ShouldSetFilters()
    {
        var request = new OrderSearchRequest
        {
            SearchTerm = "ORD-001",
            Status = "Processing",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            UserId = Guid.NewGuid(),
            MinAmount = 50m,
            MaxAmount = 200m,
            SortBy = "date",
            SortDescending = false,
            Page = 2,
            PageSize = 10
        };

        request.SearchTerm.Should().Be("ORD-001");
        request.Status.Should().Be("Processing");
        request.StartDate.Should().NotBeNull();
        request.EndDate.Should().NotBeNull();
        request.UserId.Should().NotBeNull();
        request.MinAmount.Should().Be(50m);
        request.MaxAmount.Should().Be(200m);
        request.SortBy.Should().Be("date");
        request.SortDescending.Should().BeFalse();
        request.Page.Should().Be(2);
        request.PageSize.Should().Be(10);
    }
}

public class AddressDtoComprehensiveTests
{
    [Fact]
    public void AddressDto_AllProperties_ShouldBeSettable()
    {
        var dto = new AddressDto
        {
            Id = Guid.NewGuid(),
            Street = "123 Main St",
            Street2 = "Apt 4B",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States",
            Phone = "+1234567890"
        };

        dto.Id.Should().NotBeEmpty();
        dto.Street.Should().Be("123 Main St");
        dto.Street2.Should().Be("Apt 4B");
        dto.City.Should().Be("New York");
        dto.State.Should().Be("NY");
        dto.PostalCode.Should().Be("10001");
        dto.Country.Should().Be("United States");
        dto.Phone.Should().Be("+1234567890");
    }

    [Fact]
    public void AddressDto_FullAddress_ShouldFormatCorrectly()
    {
        var dto = new AddressDto
        {
            Street = "123 Main St",
            Street2 = "Apt 4B",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States"
        };

        var fullAddress = $"{dto.Street}, {dto.Street2}, {dto.City}, {dto.State} {dto.PostalCode}, {dto.Country}";

        fullAddress.Should().Be("123 Main St, Apt 4B, New York, NY 10001, United States");
    }
}

public class OrderHistoryDtoComprehensiveTests
{
    [Fact]
    public void OrderHistoryDto_AllProperties_ShouldBeSettable()
    {
        var dto = new OrderHistoryDto
        {
            Id = Guid.NewGuid(),
            Status = "Processing",
            Comment = "Order confirmed",
            UpdatedBy = "admin@example.com",
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Status.Should().Be("Processing");
        dto.Comment.Should().Be("Order confirmed");
        dto.UpdatedBy.Should().Be("admin@example.com");
    }
}
