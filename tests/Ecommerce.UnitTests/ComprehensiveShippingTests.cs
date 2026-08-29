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

public class ShippingDtoComprehensiveTests
{
    [Fact]
    public void ShippingRateDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ShippingRateDto
        {
            Id = Guid.NewGuid(),
            Name = "Standard Shipping",
            Carrier = "UPS",
            Method = "Ground",
            BaseRate = 9.99m,
            PerKgRate = 2.50m,
            EstimatedDaysMin = 3,
            EstimatedDaysMax = 5,
            IsActive = true,
            MaxWeight = 30m,
            FreeShippingThreshold = 50m
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Standard Shipping");
        dto.Carrier.Should().Be("UPS");
        dto.Method.Should().Be("Ground");
        dto.BaseRate.Should().Be(9.99m);
        dto.PerKgRate.Should().Be(2.50m);
        dto.EstimatedDaysMin.Should().Be(3);
        dto.EstimatedDaysMax.Should().Be(5);
        dto.IsActive.Should().BeTrue();
        dto.MaxWeight.Should().Be(30m);
        dto.FreeShippingThreshold.Should().Be(50m);
    }

    [Fact]
    public void ShippingRateDto_EstimatedDelivery_ShouldReturnCorrectRange()
    {
        var dto = new ShippingRateDto
        {
            EstimatedDaysMin = 3,
            EstimatedDaysMax = 5
        };

        dto.EstimatedDelivery.Should().Be("3-5 days");
    }

    [Fact]
    public void ShippingRateDto_IsFreeShipping_ShouldReturnTrueWhenAboveThreshold()
    {
        var dto = new ShippingRateDto
        {
            FreeShippingThreshold = 50m
        };

        dto.IsFreeShipping(100m).Should().BeTrue();
    }

    [Fact]
    public void ShippingRateDto_IsFreeShipping_ShouldReturnFalseWhenBelowThreshold()
    {
        var dto = new ShippingRateDto
        {
            FreeShippingThreshold = 50m
        };

        dto.IsFreeShipping(30m).Should().BeFalse();
    }

    [Fact]
    public void ShippingRateDto_CalculateCost_ShouldReturnBaseRateWhenNoWeight()
    {
        var dto = new ShippingRateDto
        {
            BaseRate = 9.99m,
            PerKgRate = 2.50m
        };

        dto.CalculateCost(0m).Should().Be(9.99m);
    }

    [Fact]
    public void ShippingRateDto_CalculateCost_ShouldAddPerKgRate()
    {
        var dto = new ShippingRateDto
        {
            BaseRate = 9.99m,
            PerKgRate = 2.50m
        };

        dto.CalculateCost(5m).Should().Be(22.49m);
    }

    [Fact]
    public void ShippingRateDto_CalculateCost_ShouldReturnZeroWhenFreeShipping()
    {
        var dto = new ShippingRateDto
        {
            BaseRate = 9.99m,
            PerKgRate = 2.50m,
            FreeShippingThreshold = 50m
        };

        dto.CalculateCost(0m, 60m).Should().Be(0m);
    }
}

public class ShipmentDtoComprehensiveTests
{
    [Fact]
    public void ShipmentDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ShipmentDto
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            TrackingNumber = "1Z999AA10123456784",
            Carrier = "UPS",
            Method = "Ground",
            Status = "InTransit",
            EstimatedDelivery = DateTime.UtcNow.AddDays(3),
            ActualDelivery = null,
            ShippedAt = DateTime.UtcNow,
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", Quantity = 1, Sku = "SKU-001" },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", Quantity = 2, Sku = "SKU-002" }
            ],
            Events =
            [
                new() { Status = "Picked Up", Location = "Chicago, IL", Timestamp = DateTime.UtcNow.AddDays(-1), Description = "Package picked up" },
                new() { Status = "In Transit", Location = "Indianapolis, IN", Timestamp = DateTime.UtcNow, Description = "Package in transit" }
            ],
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.TrackingNumber.Should().Be("1Z999AA10123456784");
        dto.Carrier.Should().Be("UPS");
        dto.Status.Should().Be("InTransit");
        dto.Items.Should().HaveCount(2);
        dto.Events.Should().HaveCount(2);
    }

    [Fact]
    public void ShipmentDto_IsDelivered_ShouldReturnTrueWhenDelivered()
    {
        var dto = new ShipmentDto { Status = "Delivered" };

        dto.IsDelivered.Should().BeTrue();
    }

    [Fact]
    public void ShipmentDto_IsDelivered_ShouldReturnFalseWhenInTransit()
    {
        var dto = new ShipmentDto { Status = "InTransit" };

        dto.IsDelivered.Should().BeFalse();
    }

    [Fact]
    public void ShipmentDto_IsInTransit_ShouldReturnTrueWhenInTransit()
    {
        var dto = new ShipmentDto { Status = "InTransit" };

        dto.IsInTransit.Should().BeTrue();
    }

    [Fact]
    public void ShipmentDto_IsInTransit_ShouldReturnFalseWhenDelivered()
    {
        var dto = new ShipmentDto { Status = "Delivered" };

        dto.IsInTransit.Should().BeFalse();
    }

    [Fact]
    public void ShipmentDto_HasTracking_ShouldReturnTrueWhenHasTrackingNumber()
    {
        var dto = new ShipmentDto { TrackingNumber = "1Z999AA10123456784" };

        dto.HasTracking.Should().BeTrue();
    }

    [Fact]
    public void ShipmentDto_HasTracking_ShouldReturnFalseWhenNoTrackingNumber()
    {
        var dto = new ShipmentDto { TrackingNumber = null };

        dto.HasTracking.Should().BeFalse();
    }

    [Fact]
    public void ShipmentDto_LatestEvent_ShouldReturnMostRecentEvent()
    {
        var dto = new ShipmentDto
        {
            Events =
            [
                new() { Status = "Picked Up", Location = "Chicago, IL", Timestamp = DateTime.UtcNow.AddDays(-1), Description = "Package picked up" },
                new() { Status = "In Transit", Location = "Indianapolis, IN", Timestamp = DateTime.UtcNow, Description = "Package in transit" }
            ]
        };

        dto.LatestEvent.Should().NotBeNull();
        dto.LatestEvent!.Status.Should().Be("In Transit");
    }

    [Fact]
    public void ShipmentDto_LatestEvent_ShouldReturnNullWhenNoEvents()
    {
        var dto = new ShipmentDto { Events = [] };

        dto.LatestEvent.Should().BeNull();
    }
}

public class ShipmentItemDtoComprehensiveTests
{
    [Fact]
    public void ShipmentItemDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ShipmentItemDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            Sku = "SKU-001",
            Quantity = 2
        };

        dto.ProductId.Should().NotBeEmpty();
        dto.ProductName.Should().Be("Test Product");
        dto.Sku.Should().Be("SKU-001");
        dto.Quantity.Should().Be(2);
    }
}

public class ShipmentEventDtoComprehensiveTests
{
    [Fact]
    public void ShipmentEventDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ShipmentEventDto
        {
            Status = "In Transit",
            Location = "Indianapolis, IN",
            Timestamp = DateTime.UtcNow,
            Description = "Package in transit"
        };

        dto.Status.Should().Be("In Transit");
        dto.Location.Should().Be("Indianapolis, IN");
        dto.Description.Should().Be("Package in transit");
    }
}

public class ShippingCostCalculationDtoComprehensiveTests
{
    [Fact]
    public void ShippingCostCalculationDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ShippingCostCalculationDto
        {
            ShippingRateId = Guid.NewGuid(),
            ShippingRateName = "Standard Shipping",
            Carrier = "UPS",
            Method = "Ground",
            BaseRate = 9.99m,
            WeightRate = 5.00m,
            TotalRate = 14.99m,
            IsFreeShipping = false,
            EstimatedDaysMin = 3,
            EstimatedDaysMax = 5,
            EstimatedDeliveryRange = "3-5 days"
        };

        dto.ShippingRateId.Should().NotBeEmpty();
        dto.ShippingRateName.Should().Be("Standard Shipping");
        dto.TotalRate.Should().Be(14.99m);
        dto.IsFreeShipping.Should().BeFalse();
    }

    [Fact]
    public void ShippingCostCalculationDto_IsFreeShipping_ShouldReturnTrueWhenFree()
    {
        var dto = new ShippingCostCalculationDto
        {
            IsFreeShipping = true,
            TotalRate = 0m
        };

        dto.IsFreeShipping.Should().BeTrue();
        dto.TotalRate.Should().Be(0m);
    }
}

public class CreateShippingRateRequestComprehensiveTests
{
    [Fact]
    public void CreateShippingRateRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateShippingRateRequest
        {
            Name = "Express Shipping",
            Carrier = "FedEx",
            Method = "Express",
            BaseRate = 19.99m,
            PerKgRate = 4.00m,
            EstimatedDaysMin = 1,
            EstimatedDaysMax = 2,
            MaxWeight = 20m,
            FreeShippingThreshold = 100m
        };

        request.Name.Should().Be("Express Shipping");
        request.Carrier.Should().Be("FedEx");
        request.BaseRate.Should().Be(19.99m);
    }
}

public class UpdateShipmentRequestComprehensiveTests
{
    [Fact]
    public void UpdateShipmentRequest_AllProperties_ShouldBeOptional()
    {
        var request = new UpdateShipmentRequest();

        request.TrackingNumber.Should().BeNull();
        request.Carrier.Should().BeNull();
        request.Method.Should().BeNull();
        request.EstimatedDelivery.Should().BeNull();
        request.ActualDelivery.Should().BeNull();
    }

    [Fact]
    public void UpdateShipmentRequest_WithValues_ShouldSetValues()
    {
        var request = new UpdateShipmentRequest
        {
            TrackingNumber = "1Z999AA10123456784",
            Carrier = "UPS",
            Method = "Ground",
            EstimatedDelivery = DateTime.UtcNow.AddDays(3),
            ActualDelivery = DateTime.UtcNow.AddDays(2)
        };

        request.TrackingNumber.Should().Be("1Z999AA10123456784");
        request.Carrier.Should().Be("UPS");
        request.Method.Should().Be("Ground");
    }
}

public class ShippingTrackingResultDtoComprehensiveTests
{
    [Fact]
    public void ShippingTrackingResultDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ShippingTrackingResultDto
        {
            TrackingNumber = "1Z999AA10123456784",
            Carrier = "UPS",
            Status = "In Transit",
            EstimatedDelivery = DateTime.UtcNow.AddDays(3),
            Events =
            [
                new() { Status = "Picked Up", Location = "Chicago, IL", Timestamp = DateTime.UtcNow.AddDays(-1), Description = "Package picked up" },
                new() { Status = "In Transit", Location = "Indianapolis, IN", Timestamp = DateTime.UtcNow, Description = "Package in transit" }
            ]
        };

        dto.TrackingNumber.Should().Be("1Z999AA10123456784");
        dto.Carrier.Should().Be("UPS");
        dto.Status.Should().Be("In Transit");
        dto.Events.Should().HaveCount(2);
    }
}
