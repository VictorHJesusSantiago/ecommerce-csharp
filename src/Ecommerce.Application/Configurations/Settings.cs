namespace Ecommerce.Application.Configurations;

public class JwtSettings
{
    public const string SectionName = "Jwt";
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 60;
    public int RefreshTokenExpirationDays { get; set; } = 7;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateIssuerSigningKey { get; set; } = true;
}

public class SmtpSettings
{
    public const string SectionName = "Smtp";
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}

public class SendGridSettings
{
    public const string SectionName = "SendGrid";
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}

public class TwilioSettings
{
    public const string SectionName = "Twilio";
    public string AccountSid { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string FromPhoneNumber { get; set; } = string.Empty;
}

public class AzureStorageSettings
{
    public const string SectionName = "AzureStorage";
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
    public string BlobBaseUrl { get; set; } = string.Empty;
}

public class StripeSettings
{
    public const string SectionName = "Stripe";
    public string SecretKey { get; set; } = string.Empty;
    public string PublishableKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
}

public class PayPalSettings
{
    public const string SectionName = "PayPal";
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public bool SandboxMode { get; set; } = true;
    public string WebhookId { get; set; } = string.Empty;
}

public class RedisSettings
{
    public const string SectionName = "Redis";
    public string ConnectionString { get; set; } = "localhost:6379";
    public string InstanceName { get; set; } = "Ecommerce_";
    public int DefaultExpirationMinutes { get; set; } = 30;
    public bool Enabled { get; set; } = false;
}

public class RabbitMQSettings
{
    public const string SectionName = "RabbitMQ";
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public bool Enabled { get; set; } = false;
}

public class FileUploadSettings
{
    public const string SectionName = "FileUpload";
    public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;
    public string[] AllowedImageTypes { get; set; } = ["image/jpeg", "image/png", "image/webp", "image/gif"];
    public string[] AllowedDocumentTypes { get; set; } = ["application/pdf", "text/csv"];
    public string ImagesFolder { get; set; } = "images";
    public string DocumentsFolder { get; set; } = "documents";
}

public class ApplicationInfoSettings
{
    public const string SectionName = "ApplicationInfo";
    public string Name { get; set; } = "Ecommerce";
    public string Version { get; set; } = "1.0.0";
    public string Description { get; set; } = string.Empty;
    public string SupportEmail { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
}

public class PaginationSettings
{
    public const string SectionName = "Pagination";
    public int DefaultPageSize { get; set; } = 20;
    public int MaxPageSize { get; set; } = 100;
    public int ProductsPageSize { get; set; } = 24;
    public int OrdersPageSize { get; set; } = 25;
}

public class SeoSettings
{
    public const string SectionName = "Seo";
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string OgImage { get; set; } = string.Empty;
    public string RobotsTxt { get; set; } = string.Empty;
    public string GoogleAnalyticsId { get; set; } = string.Empty;
    public string FacebookPixelId { get; set; } = string.Empty;
}

public class CurrencySettings
{
    public const string SectionName = "Currency";
    public string DefaultCurrency { get; set; } = "USD";
    public string SupportedCurrencies { get; set; } = "USD,EUR,GBP,JPY,CAD,AUD";
    public bool EnableAutoConversion { get; set; } = true;
    public int ExchangeRateCacheMinutes { get; set; } = 60;
}

public class SecuritySettings
{
    public const string SectionName = "Security";
    public int MaxLoginAttempts { get; set; } = 5;
    public int LockoutDurationMinutes { get; set; } = 15;
    public int PasswordExpirationDays { get; set; } = 90;
    public bool RequireEmailVerification { get; set; } = true;
    public bool RequirePhoneNumberVerification { get; set; }
    public string AllowedIpAddresses { get; set; } = string.Empty;
    public string BlockedIpAddresses { get; set; } = string.Empty;
    public int SessionTimeoutMinutes { get; set; } = 30;
}

public class BusinessSettings
{
    public const string SectionName = "Business";
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyEmail { get; set; } = string.Empty;
    public string CompanyPhone { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public decimal DefaultTaxRate { get; set; } = 0;
    public bool EnableTax { get; set; } = true;
    public bool EnableCoupon { get; set; } = true;
    public bool EnableLoyaltyPoints { get; set; } = true;
    public int PointsPerDollarSpent { get; set; } = 1;
    public decimal PointsRedemptionRate { get; set; } = 0.01m;
    public decimal FreeShippingThreshold { get; set; } = 50m;
    public int CartAbandonmentDelayMinutes { get; set; } = 60;
}
