using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;

namespace Ecommerce.ArchitectureTests;

public class MarketingInventoryNotificationDtoTests
{
    [Fact]
    public void PromotionDto_ShouldHaveRequiredProperties()
    {
        var dto = new PromotionDto
        {
            Id = Guid.NewGuid(),
            Name = "Summer Sale 2024",
            Description = "Get up to 50% off",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 50,
            MinimumOrderAmount = 100,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true,
            UsageLimit = 500,
            UsedCount = 125,
            ApplicableProductIds = new List<Guid>(),
            ApplicableCategoryIds = new List<Guid>(),
            BannerImageUrl = "/images/promo.jpg",
            TermsAndConditions = "Cannot combine with other offers",
            Priority = 1,
            CreatedAt = DateTime.UtcNow
        };

        dto.Name.Should().Be("Summer Sale 2024");
        dto.Priority.Should().Be(1);
    }

    [Fact]
    public void NewsletterSubscriberDto_ShouldHaveRequiredProperties()
    {
        var dto = new NewsletterSubscriberDto
        {
            Id = Guid.NewGuid(),
            Email = "subscriber@example.com",
            FirstName = "John",
            LastName = "Subscriber",
            IsSubscribed = true,
            SubscribedAt = DateTime.UtcNow,
            UnsubscribedAt = null,
            Source = "Website Footer",
            Tags = new List<string> { "newsletter", "promotions" }
        };

        dto.IsSubscribed.Should().BeTrue();
        dto.Tags.Should().Contain("newsletter");
    }

    [Fact]
    public void WarehouseInventoryDto_ShouldHaveRequiredProperties()
    {
        var dto = new WarehouseInventoryDto
        {
            Id = Guid.NewGuid(),
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            ProductId = Guid.NewGuid(),
            ProductName = "Widget",
            ProductSku = "WGT-001",
            Quantity = 150,
            ReservedQuantity = 10,
            AvailableQuantity = 140,
            ReorderLevel = 25,
            MaxLevel = 500,
            LastCountDate = DateTime.UtcNow.AddDays(-5),
            LastRestockDate = DateTime.UtcNow.AddDays(-10),
            Cost = 15.99m,
            TotalValue = 2398.50m,
            Location = "Aisle 3, Shelf B"
        };

        dto.AvailableQuantity.Should().Be(140);
        dto.TotalValue.Should().Be(2398.50m);
    }

    [Fact]
    public void StockMovementDto_ShouldHaveRequiredProperties()
    {
        var dto = new StockMovementDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Widget",
            WarehouseId = Guid.NewGuid(),
            WarehouseName = "Main Warehouse",
            MovementType = "In",
            Quantity = 50,
            PreviousQuantity = 100,
            NewQuantity = 150,
            Reference = "PO-001",
            Notes = "Purchase order received",
            PerformedBy = "admin@example.com",
            PerformedAt = DateTime.UtcNow
        };

        dto.PreviousQuantity.Should().Be(100);
        dto.NewQuantity.Should().Be(150);
        dto.Quantity.Should().Be(50);
    }

    [Fact]
    public void AdjustStockDto_ShouldHaveRequiredProperties()
    {
        var dto = new AdjustStockDto
        {
            ProductId = Guid.NewGuid(),
            WarehouseId = Guid.NewGuid(),
            AdjustmentType = "Increase",
            Quantity = 25,
            Reason = "Inventory count correction",
            Reference = "AUDIT-001",
            Notes = "Found 25 extra units during audit"
        };

        dto.AdjustmentType.Should().Be("Increase");
    }

    [Fact]
    public void TransferStockDto_ShouldHaveRequiredProperties()
    {
        var dto = new TransferStockDto
        {
            ProductId = Guid.NewGuid(),
            FromWarehouseId = Guid.NewGuid(),
            ToWarehouseId = Guid.NewGuid(),
            Quantity = 30,
            Reason = "Stock rebalancing",
            Reference = "TRF-001"
        };

        dto.Quantity.Should().Be(30);
    }

    [Fact]
    public void SendNotificationDto_ShouldHaveRequiredProperties()
    {
        var dto = new SendNotificationDto
        {
            UserId = Guid.NewGuid(),
            Title = "Order Shipped",
            Message = "Your order has been shipped!",
            Type = "OrderUpdate",
            ActionUrl = "/orders/123",
            Channel = "Email",
            Priority = "Normal",
            ScheduledAt = null,
            Metadata = new Dictionary<string, string> { { "order_id", "123" } }
        };

        dto.Type.Should().Be("OrderUpdate");
        dto.Channel.Should().Be("Email");
    }

    [Fact]
    public void BulkSendNotificationDto_ShouldHaveRequiredProperties()
    {
        var dto = new BulkSendNotificationDto
        {
            UserIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
            Title = "System Maintenance",
            Message = "Scheduled maintenance tonight",
            Type = "SystemAlert",
            Channel = "Email",
            Priority = "High"
        };

        dto.UserIds.Should().HaveCount(2);
    }

    [Fact]
    public void NotificationPreferenceDto_ShouldHaveRequiredProperties()
    {
        var dto = new NotificationPreferenceDto
        {
            UserId = Guid.NewGuid(),
            EmailEnabled = true,
            SmsEnabled = false,
            PushEnabled = false,
            OrderUpdates = true,
            Promotions = true,
            Newsletter = false,
            ProductAlerts = true,
            ReviewReminders = false,
            SecurityAlerts = true,
            Frequency = "Immediate"
        };

        dto.Frequency.Should().Be("Immediate");
    }

    [Fact]
    public void EmailTemplateDto_ShouldHaveRequiredProperties()
    {
        var dto = new EmailTemplateDto
        {
            Id = Guid.NewGuid(),
            Name = "OrderConfirmation",
            Subject = "Order Confirmed - {{OrderNumber}}",
            HtmlBody = "<h1>Thank you for your order!</h1>",
            TextBody = "Thank you for your order!",
            Variables = new List<string> { "OrderNumber", "CustomerName", "TotalAmount" },
            IsActive = true,
            Category = "Ordering",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Variables.Should().Contain("OrderNumber");
        dto.Category.Should().Be("Ordering");
    }
}
