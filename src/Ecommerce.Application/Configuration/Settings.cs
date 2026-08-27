using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Configuration;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; } = 60;
    public int RefreshTokenExpirationInDays { get; set; } = 7;
}

public class SmtpSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
}

public class SendGridSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}

public class TwilioSettings
{
    public string AccountSid { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string FromPhoneNumber { get; set; } = string.Empty;
}

public class AzureStorageSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
    public string BlobEndpoint { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string AccountKey { get; set; } = string.Empty;
    public string SasToken { get; set; } = string.Empty;
}

public class StripeSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string PublishableKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
}

public class PayPalSettings
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Mode { get; set; } = "sandbox";
}

public class RedisSettings
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; } = "localhost:6379";
    public string InstanceName { get; set; } = "Ecommerce_";
}

public class RabbitMQSettings
{
    public bool Enabled { get; set; }
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
}

public class FileUploadSettings
{
    public string BasePath { get; set; } = "./uploads";
    public int MaxFileSizeMB { get; set; } = 10;
    public string[] AllowedExtensions { get; set; } = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
    public long MaxFileSizeBytes => MaxFileSizeMB * 1024 * 1024;
}

public class AppSettings
{
    public string Name { get; set; } = "E-Commerce Store";
    public string Version { get; set; } = "1.0.0";
    public string SupportEmail { get; set; } = "support@ecommerce.com";
    public string SupportPhone { get; set; } = "+1-800-555-0199";
    public string DefaultCurrency { get; set; } = "USD";
    public decimal DefaultTaxRate { get; set; } = 0.08m;
    public decimal FreeShippingThreshold { get; set; } = 50.00m;
    public decimal DefaultShippingCost { get; set; } = 9.99m;
    public int MaxCartItems { get; set; } = 50;
    public int MinPasswordLength { get; set; } = 8;
    public bool AllowGuestCheckout { get; set; } = true;
    public bool ReviewsEnabled { get; set; } = true;
}
