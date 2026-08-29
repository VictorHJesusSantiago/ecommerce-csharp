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

public class ApplicationConstantTests
{
    [Fact]
    public void CacheKeys_Products_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Products.Should().StartWith("products:");
    }

    [Fact]
    public void CacheKeys_Categories_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Categories.Should().StartWith("categories:");
    }

    [Fact]
    public void CacheKeys_Orders_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Orders.Should().StartWith("orders:");
    }

    [Fact]
    public void CacheKeys_Users_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Users.Should().StartWith("users:");
    }

    [Fact]
    public void CacheKeys_Carts_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Carts.Should().StartWith("carts:");
    }

    [Fact]
    public void CacheKeys_Reviews_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Reviews.Should().StartWith("reviews:");
    }

    [Fact]
    public void CacheKeys_Coupons_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Coupons.Should().StartWith("coupons:");
    }

    [Fact]
    public void CacheKeys_Banners_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Banners.Should().StartWith("banners:");
    }

    [Fact]
    public void CacheKeys_Settings_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Settings.Should().StartWith("settings:");
    }

    [Fact]
    public void CacheKeys_Search_ShouldHaveCorrectPrefix()
    {
        ApplicationConstants.CacheKeys.Search.Should().StartWith("search:");
    }

    [Fact]
    public void CacheDurations_Short_ShouldBe5Minutes()
    {
        ApplicationConstants.CacheDurations.Short.Should().Be(TimeSpan.FromMinutes(5));
    }

    [Fact]
    public void CacheDurations_Medium_ShouldBe30Minutes()
    {
        ApplicationConstants.CacheDurations.Medium.Should().Be(TimeSpan.FromMinutes(30));
    }

    [Fact]
    public void CacheDurations_Long_ShouldBe1Hour()
    {
        ApplicationConstants.CacheDurations.Long.Should().Be(TimeSpan.FromHours(1));
    }

    [Fact]
    public void CacheDurations_VeryLong_ShouldBe24Hours()
    {
        ApplicationConstants.CacheDurations.VeryLong.Should().Be(TimeSpan.FromHours(24));
    }

    [Fact]
    public void Queues_Orders_ShouldHaveCorrectName()
    {
        ApplicationConstants.Queues.Orders.Should().Be("orders");
    }

    [Fact]
    public void Queues_Payments_ShouldHaveCorrectName()
    {
        ApplicationConstants.Queues.Payments.Should().Be("payments");
    }

    [Fact]
    public void Queues_Notifications_ShouldHaveCorrectName()
    {
        ApplicationConstants.Queues.Notifications.Should().Be("notifications");
    }

    [Fact]
    public void Queues_Inventory_ShouldHaveCorrectName()
    {
        ApplicationConstants.Queues.Inventory.Should().Be("inventory");
    }

    [Fact]
    public void SignalRHubs_Orders_ShouldHaveCorrectName()
    {
        ApplicationConstants.SignalRHubs.Orders.Should().Be("orders");
    }

    [Fact]
    public void SignalRHubs_Notifications_ShouldHaveCorrectName()
    {
        ApplicationConstants.SignalRHubs.Notifications.Should().Be("notifications");
    }

    [Fact]
    public void SignalRHubs_Dashboard_ShouldHaveCorrectName()
    {
        ApplicationConstants.SignalRHubs.Dashboard.Should().Be("dashboard");
    }
}

public class ErrorMessagesTests
{
    [Fact]
    public void ErrorMessages_ProductNotFound_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.ProductNotFound.Should().Contain("product");
    }

    [Fact]
    public void ErrorMessages_CategoryNotFound_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.CategoryNotFound.Should().Contain("category");
    }

    [Fact]
    public void ErrorMessages_OrderNotFound_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.OrderNotFound.Should().Contain("order");
    }

    [Fact]
    public void ErrorMessages_UserNotFound_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.UserNotFound.Should().Contain("user");
    }

    [Fact]
    public void ErrorMessages_InsufficientStock_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.InsufficientStock.Should().Contain("stock");
    }

    [Fact]
    public void ErrorMessages_InvalidCoupon_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.InvalidCoupon.Should().Contain("coupon");
    }

    [Fact]
    public void ErrorMessages_UnauthorizedAccess_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.UnauthorizedAccess.Should().Contain("unauthorized");
    }

    [Fact]
    public void ErrorMessages_ForbiddenAccess_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.ForbiddenAccess.Should().Contain("forbidden");
    }

    [Fact]
    public void ErrorMessages_ValidationError_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.ValidationError.Should().Contain("validation");
    }

    [Fact]
    public void ErrorMessages_InternalServerError_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.ErrorMessages.InternalServerError.Should().Contain("internal");
    }
}

public class SuccessMessagesTests
{
    [Fact]
    public void SuccessMessages_ProductCreated_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.ProductCreated.Should().Contain("created");
    }

    [Fact]
    public void SuccessMessages_ProductUpdated_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.ProductUpdated.Should().Contain("updated");
    }

    [Fact]
    public void SuccessMessages_ProductDeleted_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.ProductDeleted.Should().Contain("deleted");
    }

    [Fact]
    public void SuccessMessages_OrderPlaced_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.OrderPlaced.Should().Contain("placed");
    }

    [Fact]
    public void SuccessMessages_OrderCancelled_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.OrderCancelled.Should().Contain("cancelled");
    }

    [Fact]
    public void SuccessMessages_PaymentProcessed_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.PaymentProcessed.Should().Contain("processed");
    }

    [Fact]
    public void SuccessMessages_ReviewCreated_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.ReviewCreated.Should().Contain("created");
    }

    [Fact]
    public void SuccessMessages_CouponApplied_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.CouponApplied.Should().Contain("applied");
    }

    [Fact]
    public void SuccessMessages_ProfileUpdated_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.ProfileUpdated.Should().Contain("updated");
    }

    [Fact]
    public void SuccessMessages_PasswordChanged_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.SuccessMessages.PasswordChanged.Should().Contain("changed");
    }
}

public class LogMessagesTests
{
    [Fact]
    public void LogMessages_UserLoggedIn_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.UserLoggedIn.Should().Contain("logged in");
    }

    [Fact]
    public void LogMessages_UserRegistered_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.UserRegistered.Should().Contain("registered");
    }

    [Fact]
    public void LogMessages_OrderCreated_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.OrderCreated.Should().Contain("created");
    }

    [Fact]
    public void LogMessages_PaymentProcessed_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.PaymentProcessed.Should().Contain("processed");
    }

    [Fact]
    public void LogMessages_ProductCreated_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.ProductCreated.Should().Contain("created");
    }

    [Fact]
    public void LogMessages_CacheHit_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.CacheHit.Should().Contain("cache");
    }

    [Fact]
    public void LogMessages_CacheMiss_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.CacheMiss.Should().Contain("cache");
    }

    [Fact]
    public void LogMessages_ApiRequest_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.ApiRequest.Should().Contain("request");
    }

    [Fact]
    public void LogMessages_ApiResponse_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.ApiResponse.Should().Contain("response");
    }

    [Fact]
    public void LogMessages_ExceptionOccurred_ShouldHaveCorrectMessage()
    {
        ApplicationConstants.LogMessages.ExceptionOccurred.Should().Contain("exception");
    }
}

public class JwtSettingsTests
{
    [Fact]
    public void JwtSettings_DefaultValues_ShouldBeCorrect()
    {
        var settings = new JwtSettings();

        settings.SecretKey.Should().BeNull();
        settings.Issuer.Should().BeNull();
        settings.Audience.Should().BeNull();
        settings.ExpirationInMinutes.Should().Be(0);
        settings.RefreshTokenExpirationInDays.Should().Be(0);
    }
}

public class SmtpSettingsTests
{
    [Fact]
    public void SmtpSettings_DefaultValues_ShouldBeCorrect()
    {
        var settings = new SmtpSettings();

        settings.Host.Should().BeNull();
        settings.Port.Should().Be(0);
        settings.UserName.Should().BeNull();
        settings.Password.Should().BeNull();
        settings.EnableSsl.Should().BeFalse();
        settings.FromEmail.Should().BeNull();
        settings.FromName.Should().BeNull();
    }
}

public class RedisSettingsTests
{
    [Fact]
    public void RedisSettings_DefaultValues_ShouldBeCorrect()
    {
        var settings = new RedisSettings();

        settings.ConnectionString.Should().BeNull();
        settings.InstanceName.Should().BeNull();
        settings.DefaultExpirationMinutes.Should().Be(0);
    }
}

public class StripeSettingsTests
{
    [Fact]
    public void StripeSettings_DefaultValues_ShouldBeCorrect()
    {
        var settings = new StripeSettings();

        settings.SecretKey.Should().BeNull();
        settings.PublishableKey.Should().BeNull();
        settings.WebhookSecret.Should().BeNull();
    }
}

public class PayPalSettingsTests
{
    [Fact]
    public void PayPalSettings_DefaultValues_ShouldBeCorrect()
    {
        var settings = new PayPalSettings();

        settings.ClientId.Should().BeNull();
        settings.ClientSecret.Should().BeNull();
        settings.WebhookId.Should().BeNull();
        settings.IsSandbox.Should().BeFalse();
    }
}

public class AppSettingsTests
{
    [Fact]
    public void AppSettings_DefaultValues_ShouldBeCorrect()
    {
        var settings = new AppSettings();

        settings.Name.Should().BeNull();
        settings.Description.Should().BeNull();
        settings.Website.Should().BeNull();
        settings.SupportEmail.Should().BeNull();
        settings.DefaultCurrency.Should().BeNull();
        settings.DefaultLanguage.Should().BeNull();
    }
}
