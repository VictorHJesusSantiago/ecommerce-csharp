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

public class NotificationDtoComprehensiveTests
{
    [Fact]
    public void NotificationDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NotificationDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Order Shipped",
            Message = "Your order has been shipped",
            Type = "Order",
            Channel = "Email",
            IsRead = false,
            ActionUrl = "/orders/123",
            Data = new Dictionary<string, string> { ["OrderId"] = "123", ["TrackingNumber"] = "ABC123" },
            CreatedAt = DateTime.UtcNow,
            ReadAt = null
        };

        dto.Id.Should().NotBeEmpty();
        dto.Title.Should().Be("Order Shipped");
        dto.Message.Should().Be("Your order has been shipped");
        dto.Type.Should().Be("Order");
        dto.Channel.Should().Be("Email");
        dto.IsRead.Should().BeFalse();
        dto.Data.Should().ContainKey("OrderId");
    }

    [Fact]
    public void NotificationDto_IsUnread_ShouldReturnTrueWhenNotRead()
    {
        var dto = new NotificationDto { IsRead = false };

        dto.IsUnread.Should().BeTrue();
    }

    [Fact]
    public void NotificationDto_IsUnread_ShouldReturnFalseWhenRead()
    {
        var dto = new NotificationDto { IsRead = true };

        dto.IsUnread.Should().BeFalse();
    }

    [Fact]
    public void NotificationDto_TimeAgo_ShouldReturnCorrectTimeString()
    {
        var dto = new NotificationDto
        {
            CreatedAt = DateTime.UtcNow.AddMinutes(-5)
        };

        dto.TimeAgo.Should().Contain("5");
    }

    [Fact]
    public void NotificationDto_TimeAgo_ShouldReturnHoursTimeString()
    {
        var dto = new NotificationDto
        {
            CreatedAt = DateTime.UtcNow.AddHours(-3)
        };

        dto.TimeAgo.Should().Contain("3");
    }

    [Fact]
    public void NotificationDto_TimeAgo_ShouldReturnDaysTimeString()
    {
        var dto = new NotificationDto
        {
            CreatedAt = DateTime.UtcNow.AddDays(-2)
        };

        dto.TimeAgo.Should().Contain("2");
    }
}

public class SendNotificationRequestComprehensiveTests
{
    [Fact]
    public void SendNotificationRequest_AllProperties_ShouldBeSettable()
    {
        var request = new SendNotificationRequest
        {
            UserId = Guid.NewGuid(),
            Title = "Order Shipped",
            Message = "Your order has been shipped",
            Type = "Order",
            Channel = "Email",
            ActionUrl = "/orders/123",
            Data = new Dictionary<string, string> { ["OrderId"] = "123" }
        };

        request.UserId.Should().NotBeEmpty();
        request.Title.Should().Be("Order Shipped");
        request.Channel.Should().Be("Email");
    }
}

public class BulkSendNotificationRequestComprehensiveTests
{
    [Fact]
    public void BulkSendNotificationRequest_AllProperties_ShouldBeSettable()
    {
        var request = new BulkSendNotificationRequest
        {
            UserIds = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()],
            Title = "System Maintenance",
            Message = "Scheduled maintenance tonight",
            Type = "System",
            Channel = "Email"
        };

        request.UserIds.Should().HaveCount(3);
        request.Title.Should().Be("System Maintenance");
    }

    [Fact]
    public void BulkSendNotificationRequest_AllUsers_ShouldBeSettable()
    {
        var request = new BulkSendNotificationRequest
        {
            AllUsers = true,
            Title = "System Maintenance",
            Message = "Scheduled maintenance tonight",
            Type = "System",
            Channel = "Email"
        };

        request.AllUsers.Should().BeTrue();
        request.UserIds.Should().BeNull();
    }
}

public class NotificationPreferencesDtoComprehensiveTests
{
    [Fact]
    public void NotificationPreferencesDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NotificationPreferencesDto
        {
            UserId = Guid.NewGuid(),
            EmailEnabled = true,
            SmsEnabled = false,
            PushEnabled = true,
            OrderUpdates = true,
            Promotions = true,
            Newsletter = false,
            SecurityAlerts = true,
            ProductAlerts = true
        };

        dto.UserId.Should().NotBeEmpty();
        dto.EmailEnabled.Should().BeTrue();
        dto.SmsEnabled.Should().BeFalse();
        dto.PushEnabled.Should().BeTrue();
        dto.OrderUpdates.Should().BeTrue();
        dto.Promotions.Should().BeTrue();
        dto.Newsletter.Should().BeFalse();
        dto.SecurityAlerts.Should().BeTrue();
        dto.ProductAlerts.Should().BeTrue();
    }
}

public class EmailTemplateDtoComprehensiveTests
{
    [Fact]
    public void EmailTemplateDto_AllProperties_ShouldBeSettable()
    {
        var dto = new EmailTemplateDto
        {
            Id = Guid.NewGuid(),
            Name = "Order Confirmation",
            Subject = "Your order has been confirmed",
            HtmlContent = "<html><body>Thank you for your order!</body></html>",
            PlainTextContent = "Thank you for your order!",
            Variables = ["OrderNumber", "CustomerName", "TotalAmount"],
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Order Confirmation");
        dto.Subject.Should().Be("Your order has been confirmed");
        dto.Variables.Should().HaveCount(3);
        dto.IsActive.Should().BeTrue();
    }
}

public class NotificationBatchDtoComprehensiveTests
{
    [Fact]
    public void NotificationBatchDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NotificationBatchDto
        {
            Id = Guid.NewGuid(),
            Title = "Summer Sale Announcement",
            Message = "Check out our summer sale!",
            Type = "Promotion",
            Channel = "Email",
            TotalRecipients = 5000,
            SentCount = 4800,
            FailedCount = 200,
            PendingCount = 0,
            Status = "Completed",
            ScheduledAt = DateTime.UtcNow.AddHours(1),
            StartedAt = DateTime.UtcNow,
            CompletedAt = DateTime.UtcNow.AddMinutes(15),
            CreatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.TotalRecipients.Should().Be(5000);
        dto.SentCount.Should().Be(4800);
        dto.FailedCount.Should().Be(200);
        dto.Status.Should().Be("Completed");
    }

    [Fact]
    public void NotificationBatchDto_SuccessRate_ShouldCalculateCorrectly()
    {
        var dto = new NotificationBatchDto
        {
            TotalRecipients = 5000,
            SentCount = 4800,
            FailedCount = 200
        };

        dto.SuccessRate.Should().Be(96m);
    }

    [Fact]
    public void NotificationBatchDto_SuccessRate_ShouldReturnZeroWhenNoRecipients()
    {
        var dto = new NotificationBatchDto
        {
            TotalRecipients = 0,
            SentCount = 0,
            FailedCount = 0
        };

        dto.SuccessRate.Should().Be(0);
    }

    [Fact]
    public void NotificationBatchDto_IsCompleted_ShouldReturnTrueWhenCompleted()
    {
        var dto = new NotificationBatchDto { Status = "Completed" };

        dto.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public void NotificationBatchDto_IsCompleted_ShouldReturnFalseWhenPending()
    {
        var dto = new NotificationBatchDto { Status = "Pending" };

        dto.IsCompleted.Should().BeFalse();
    }
}

public class NotificationAnalyticsDtoComprehensiveTests
{
    [Fact]
    public void NotificationAnalyticsDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NotificationAnalyticsDto
        {
            TotalSent = 10000,
            TotalDelivered = 9800,
            TotalOpened = 4900,
            TotalClicked = 1500,
            DeliveryRate = 98m,
            OpenRate = 50m,
            ClickRate = 15.3m,
            ChannelPerformance = new Dictionary<string, ChannelPerformanceDto>
            {
                ["Email"] = new() { Sent = 7000, Delivered = 6900, Opened = 3450, Clicked = 1000 },
                ["SMS"] = new() { Sent = 2000, Delivered = 1950, Opened = 1500, Clicked = 400 },
                ["Push"] = new() { Sent = 1000, Delivered = 950, Opened = 500, Clicked = 100 }
            }
        };

        dto.TotalSent.Should().Be(10000);
        dto.TotalDelivered.Should().Be(9800);
        dto.TotalOpened.Should().Be(4900);
        dto.TotalClicked.Should().Be(1500);
        dto.DeliveryRate.Should().Be(98m);
        dto.OpenRate.Should().Be(50m);
        dto.ClickRate.Should().Be(15.3m);
        dto.ChannelPerformance.Should().HaveCount(3);
    }
}

public class ChannelPerformanceDtoComprehensiveTests
{
    [Fact]
    public void ChannelPerformanceDto_AllProperties_ShouldBeSettable()
    {
        var dto = new ChannelPerformanceDto
        {
            Sent = 7000,
            Delivered = 6900,
            Opened = 3450,
            Clicked = 1000
        };

        dto.Sent.Should().Be(7000);
        dto.Delivered.Should().Be(6900);
        dto.Opened.Should().Be(3450);
        dto.Clicked.Should().Be(1000);
    }

    [Fact]
    public void ChannelPerformanceDto_DeliveryRate_ShouldCalculateCorrectly()
    {
        var dto = new ChannelPerformanceDto
        {
            Sent = 7000,
            Delivered = 6900
        };

        dto.DeliveryRate.Should().Be(98.57m);
    }

    [Fact]
    public void ChannelPerformanceDto_OpenRate_ShouldCalculateCorrectly()
    {
        var dto = new ChannelPerformanceDto
        {
            Delivered = 6900,
            Opened = 3450
        };

        dto.OpenRate.Should().Be(50m);
    }

    [Fact]
    public void ChannelPerformanceDto_ClickRate_ShouldCalculateCorrectly()
    {
        var dto = new ChannelPerformanceDto
        {
            Opened = 3450,
            Clicked = 1000
        };

        dto.ClickRate.Should().Be(28.99m);
    }
}

public class UpdateNotificationPreferencesRequestComprehensiveTests
{
    [Fact]
    public void UpdateNotificationPreferencesRequest_AllProperties_ShouldBeSettable()
    {
        var request = new UpdateNotificationPreferencesRequest
        {
            EmailEnabled = true,
            SmsEnabled = false,
            PushEnabled = true,
            OrderUpdates = true,
            Promotions = false,
            Newsletter = false,
            SecurityAlerts = true,
            ProductAlerts = true
        };

        request.EmailEnabled.Should().BeTrue();
        request.SmsEnabled.Should().BeFalse();
        request.PushEnabled.Should().BeTrue();
    }
}
