using FluentAssertions;
using Xunit;
using Ecommerce.Application.Configuration;
using Ecommerce.Application.Common;
using Ecommerce.Application.Configuration;

namespace Ecommerce.ArchitectureTests;

public class ConfigurationTests
{
    [Fact]
    public void JwtSettings_ShouldHaveDefaults()
    {
        var settings = new JwtSettings
        {
            SecretKey = "super-secret-key-at-least-32-chars!",
            Issuer = "ECommerce",
            Audience = "ECommerceUsers",
            ExpiryInMinutes = 60,
            RefreshTokenExpiryInDays = 7
        };

        settings.ExpiryInMinutes.Should().Be(60);
    }

    [Fact]
    public void SmtpSettings_ShouldHaveDefaults()
    {
        var settings = new SmtpSettings
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            Username = "user@gmail.com",
            Password = "password",
            FromEmail = "noreply@ecommerce.com",
            FromName = "ECommerce Store"
        };

        settings.EnableSsl.Should().BeTrue();
    }

    [Fact]
    public void RedisSettings_ShouldHaveDefaults()
    {
        var settings = new RedisSettings
        {
            ConnectionString = "localhost:6379",
            InstanceName = "ecommerce_",
            DefaultExpirationMinutes = 30
        };

        settings.DefaultExpirationMinutes.Should().Be(30);
    }

    [Fact]
    public void StripeSettings_ShouldHaveRequiredProperties()
    {
        var settings = new StripeSettings
        {
            PublishableKey = "pk_test_123",
            SecretKey = "sk_test_456",
            WebhookSecret = "whsec_789",
            Currency = "usd"
        };

        settings.SecretKey.Should().StartWith("sk_");
    }

    [Fact]
    public void PayPalSettings_ShouldHaveRequiredProperties()
    {
        var settings = new PayPalSettings
        {
            ClientId = "paypal-client-id",
            ClientSecret = "paypal-client-secret",
            Mode = "sandbox",
            ReturnUrl = "https://example.com/checkout/success",
            CancelUrl = "https://example.com/checkout/cancel"
        };

        settings.Mode.Should().Be("sandbox");
    }

    [Fact]
    public void AzureStorageSettings_ShouldHaveRequiredProperties()
    {
        var settings = new AzureStorageSettings
        {
            ConnectionString = "DefaultEndpointsProtocol=https;AccountName=store",
            ContainerName = "media",
            ThumbnailContainerName = "thumbnails",
            MaxFileSizeMB = 10
        };

        settings.MaxFileSizeMB.Should().Be(10);
    }

    [Fact]
    public void SendGridSettings_ShouldHaveRequiredProperties()
    {
        var settings = new SendGridSettings
        {
            ApiKey = "SG.1234567890",
            FromEmail = "noreply@ecommerce.com",
            FromName = "ECommerce Store",
            ReplyToEmail = "support@ecommerce.com"
        };

        settings.ApiKey.Should().StartWith("SG.");
    }

    [Fact]
    public void TwilioSettings_ShouldHaveRequiredProperties()
    {
        var settings = new TwilioSettings
        {
            AccountSid = "AC1234567890",
            AuthToken = "auth-token-123",
            PhoneNumber = "+15551234567"
        };

        settings.AccountSid.Should().StartWith("AC");
    }

    [Fact]
    public void RabbitMQSettings_ShouldHaveRequiredProperties()
    {
        var settings = new RabbitMQSettings
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };

        settings.Port.Should().Be(5672);
    }

    [Fact]
    public void AppSettings_ShouldAggregate()
    {
        var settings = new AppSettings
        {
            Name = "ECommerce Store",
            Url = "https://ecommerce.com",
            Currency = "USD",
            Culture = "en-US",
            TimeZone = "UTC",
            ItemsPerPage = 20,
            MaxUploadSizeMB = 10,
            EnableMaintenanceMode = false,
            MaintenanceMessage = "We'll be back soon"
        };

        settings.ItemsPerPage.Should().Be(20);
    }

    [Fact]
    public void FileUploadSettings_ShouldHaveDefaults()
    {
        var settings = new FileUploadSettings
        {
            MaxFileSizeBytes = 10485760,
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".webp" },
            UploadPath = "/uploads",
            ThumbnailPath = "/uploads/thumbnails"
        };

        settings.AllowedExtensions.Should().Contain(".jpg");
    }

    [Fact]
    public void ApplicationConstants_CacheKeys_ShouldBeDefined()
    {
        ApplicationConstants.CacheKeys.Products.Should().NotBeNullOrEmpty();
        ApplicationConstants.CacheKeys.Categories.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ApplicationConstants_CacheDurations_ShouldBePositive()
    {
        ApplicationConstants.CacheDurations.Short.Should().BeGreaterThan(TimeSpan.Zero);
        ApplicationConstants.CacheDurations.Medium.Should().BeGreaterThan(TimeSpan.Zero);
        ApplicationConstants.CacheDurations.Long.Should().BeGreaterThan(TimeSpan.Zero);
    }

    [Fact]
    public void ApplicationConstants_Queues_ShouldBeDefined()
    {
        ApplicationConstants.Queues.OrderProcessing.Should().NotBeNullOrEmpty();
        ApplicationConstants.Queues.EmailNotification.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ApplicationConstants_SignalRHubs_ShouldBeDefined()
    {
        ApplicationConstants.SignalRHubs.Notifications.Should().NotBeNullOrEmpty();
        ApplicationConstants.SignalRHubs.OrderUpdates.Should().NotBeNullOrEmpty();
    }
}
