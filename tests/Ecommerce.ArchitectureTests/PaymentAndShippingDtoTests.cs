using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Payment;
using Ecommerce.Application.DTOs.Shipping;

namespace Ecommerce.ArchitectureTests;

public class PaymentAndShippingDtoTests
{
    [Fact]
    public void PaymentDto_ShouldHaveRequiredProperties()
    {
        var dto = new PaymentDto
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Amount = 100m,
            Currency = "USD",
            Status = "Completed",
            PaymentMethod = "Credit Card",
            TransactionId = "txn_123",
            CardLastFour = "4242",
            CardType = "Visa",
            PaymentDate = DateTime.UtcNow,
            RefundAmount = 0m,
            IsRefunded = false,
            Metadata = new Dictionary<string, string> { { "order_number", "ORD-001" } }
        };

        dto.Amount.Should().Be(100m);
        dto.IsRefunded.Should().BeFalse();
        dto.Metadata.Should().ContainKey("order_number");
    }

    [Fact]
    public void ProcessPaymentDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProcessPaymentDto
        {
            OrderId = Guid.NewGuid(),
            Amount = 50m,
            Currency = "USD",
            PaymentMethodId = Guid.NewGuid(),
            SavePaymentMethod = true,
            ReturnUrl = "https://example.com/checkout/success",
            CancelUrl = "https://example.com/checkout/cancel"
        };

        dto.SavePaymentMethod.Should().BeTrue();
    }

    [Fact]
    public void RefundDto_ShouldHaveRequiredProperties()
    {
        var dto = new RefundDto
        {
            PaymentId = Guid.NewGuid(),
            Amount = 25m,
            Reason = "Product returned",
            OrderId = Guid.NewGuid()
        };

        dto.Reason.Should().Be("Product returned");
    }

    [Fact]
    public void PayPalPaymentDto_ShouldHaveRequiredProperties()
    {
        var dto = new PayPalPaymentDto
        {
            OrderId = "PAY-1234567890",
            PayerId = "payer_123",
            PayerEmail = "buyer@example.com",
            Status = "COMPLETED",
            Amount = 99.99m,
            Currency = "USD",
            ApprovalUrl = "https://paypal.com/approve",
            CaptureId = "cap_123"
        };

        dto.PayerEmail.Should().Be("buyer@example.com");
    }

    [Fact]
    public void StripePaymentDto_ShouldHaveRequiredProperties()
    {
        var dto = new StripePaymentDto
        {
            PaymentIntentId = "pi_123456",
            Amount = 5000,
            Currency = "usd",
            Status = "succeeded",
            ClientSecret = "cs_secret_123",
            PaymentMethod = "pm_card_visa",
            ReceiptUrl = "https://receipt.stripe.com/123"
        };

        dto.PaymentIntentId.Should().StartWith("pi_");
    }

    [Fact]
    public void ShippingRateDto_ShouldHaveRequiredProperties()
    {
        var dto = new ShippingRateDto
        {
            Id = Guid.NewGuid(),
            Name = "Express Shipping",
            Carrier = "FedEx",
            EstimatedDays = 2,
            Cost = 19.99m,
            MinWeight = 0,
            MaxWeight = 20,
            IsTracked = true,
            IsActive = true,
            Description = "Fast express delivery"
        };

        dto.EstimatedDays.Should().Be(2);
        dto.IsTracked.Should().BeTrue();
    }

    [Fact]
    public void ShipmentDto_ShouldHaveRequiredProperties()
    {
        var dto = new ShipmentDto
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            TrackingNumber = "1Z999AA10123456784",
            Carrier = "UPS",
            Status = "InTransit",
            ShippedDate = DateTime.UtcNow,
            EstimatedDelivery = DateTime.UtcNow.AddDays(3),
            ActualDelivery = null,
            Weight = 2.5m,
            ShippingCost = 12.99m,
            Items = new List<ShipmentItemDto>
            {
                new() { ProductName = "Widget", Quantity = 1, Weight = 0.5m }
            },
            Events = new List<ShipmentEventDto>
            {
                new() { Status = "Picked Up", Location = "Chicago, IL", Timestamp = DateTime.UtcNow.AddHours(-2) }
            }
        };

        dto.Items.Should().HaveCount(1);
        dto.Events.Should().HaveCount(1);
        dto.TrackingNumber.Should().Be("1Z999AA10123456784");
    }

    [Fact]
    public void CalculateShippingDto_ShouldHaveRequiredProperties()
    {
        var dto = new CalculateShippingDto
        {
            ShippingAddress = new AddressDto
            {
                Street = "123 Main St",
                City = "New York",
                State = "NY",
                PostalCode = "10001",
                Country = "US"
            },
            Items = new List<ShippingItemDto>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 2, Weight = 1.0m }
            }
        };

        dto.Items.Should().HaveCount(1);
    }

    [Fact]
    public void TrackingEventDto_ShouldHaveRequiredProperties()
    {
        var dto = new TrackingEventDto
        {
            Status = "Delivered",
            Description = "Package delivered to front door",
            Location = "New York, NY",
            Timestamp = DateTime.UtcNow,
            CarrierStatus = "DL"
        };

        dto.CarrierStatus.Should().Be("DL");
    }
}
