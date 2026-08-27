namespace Ecommerce.Application.DTOs.User;

public class UserExtendedDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; }
    public string? Role { get; set; }
    public List<string> Roles { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? LastLoginIp { get; set; }
    public int LoginCount { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
    public int WishlistCount { get; set; }
    public int ReviewCount { get; set; }
    public int LoyaltyPoints { get; set; }
    public string? LoyaltyTier { get; set; }
    public List<UserAddressDto> Addresses { get; set; } = [];
    public List<PaymentMethodDto> PaymentMethods { get; set; } = [];
    public UserPreferencesDto Preferences { get; set; } = new();
    public UserStatsDto Stats { get; set; } = new();
}

public class UserActivityLogExtendedDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Location { get; set; }
    public string? Device { get; set; }
    public string? Browser { get; set; }
    public string? OperatingSystem { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public string? SessionId { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
}

public class UserSegmentDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CustomerCount { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal CustomerLifetimeValue { get; set; }
    public int AverageOrdersPerCustomer { get; set; }
    public decimal RetentionRate { get; set; }
    public string Criteria { get; set; } = string.Empty;
    public DateTime? LastUpdated { get; set; }
}

public class UserImportDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Password { get; set; }
    public bool SendWelcomeEmail { get; set; } = true;
    public Dictionary<string, string> CustomFields { get; set; } = [];
}

public class UserExportDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class UserConsentDto
{
    public Guid UserId { get; set; }
    public bool MarketingConsent { get; set; }
    public bool AnalyticsConsent { get; set; }
    public bool ThirdPartySharingConsent { get; set; }
    public bool CookieConsent { get; set; }
    public DateTime? ConsentDate { get; set; }
    public string? ConsentVersion { get; set; }
    public string? ConsentSource { get; set; }
    public bool DataProcessingConsent { get; set; }
    public bool AgeVerificationConsent { get; set; }
    public DateTime? LastUpdated { get; set; }
}

public class UpdateConsentRequest
{
    public bool? MarketingConsent { get; set; }
    public bool? AnalyticsConsent { get; set; }
    public bool? ThirdPartySharingConsent { get; set; }
    public bool? CookieConsent { get; set; }
    public bool? DataProcessingConsent { get; set; }
    public string? ConsentVersion { get; set; }
}

public class UserDeletionRequest
{
    public Guid UserId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public bool AnonymizeData { get; set; } = true;
    public bool DeleteOrders { get; set; }
    public bool DeleteReviews { get; set; }
    public bool DeleteActivityLogs { get; set; }
    public DateTime? ScheduledDeletionDate { get; set; }
    public string? AdminNotes { get; set; }
}

public class UserDeletionResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int AnonymizedFields { get; set; }
    public int DeletedRecords { get; set; }
    public DateTime ProcessedAt { get; set; }
    public string? ProcessedBy { get; set; }
}

public class UserLoyaltyDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int TotalPoints { get; set; }
    public int AvailablePoints { get; set; }
    public int PendingPoints { get; set; }
    public int ExpiredPoints { get; set; }
    public string Tier { get; set; } = string.Empty;
    public decimal PointsToNextTier { get; set; }
    public DateTime? TierExpiryDate { get; set; }
    public List<LoyaltyTransactionDto> RecentTransactions { get; set; } = [];
    public List<LoyaltyRewardDto> AvailableRewards { get; set; } = [];
}

public class LoyaltyTransactionDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Points { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

public class LoyaltyRewardDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int RequiredPoints { get; set; }
    public string RewardType { get; set; } = string.Empty;
    public decimal? DiscountAmount { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public bool IsAvailable { get; set; }
    public int StockQuantity { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class UserNotificationPreferenceDto
{
    public Guid UserId { get; set; }
    public bool EmailOrderUpdates { get; set; } = true;
    public bool EmailPromotions { get; set; }
    public bool EmailNewsletter { get; set; } = true;
    public bool EmailProductRecommendations { get; set; }
    public bool EmailPriceDropAlerts { get; set; } = true;
    public bool EmailBackInStockAlerts { get; set; } = true;
    public bool EmailReviewRequests { get; set; } = true;
    public bool EmailSecurityAlerts { get; set; } = true;
    public bool SmsOrderUpdates { get; set; }
    public bool SmsPromotions { get; set; }
    public bool SmsDeliveryUpdates { get; set; }
    public bool PushOrderUpdates { get; set; } = true;
    public bool PushPromotions { get; set; }
    public bool PushPriceDropAlerts { get; set; } = true;
    public bool PushNewArrivals { get; set; }
    public string PreferredNotificationTime { get; set; } = "morning";
    public string PreferredFrequency { get; set; } = "daily";
    public DateTime? LastUpdated { get; set; }
}

public class UpdateNotificationPreferenceRequest
{
    public bool? EmailOrderUpdates { get; set; }
    public bool? EmailPromotions { get; set; }
    public bool? EmailNewsletter { get; set; }
    public bool? EmailProductRecommendations { get; set; }
    public bool? EmailPriceDropAlerts { get; set; }
    public bool? EmailBackInStockAlerts { get; set; }
    public bool? EmailReviewRequests { get; set; }
    public bool? EmailSecurityAlerts { get; set; }
    public bool? SmsOrderUpdates { get; set; }
    public bool? SmsPromotions { get; set; }
    public bool? SmsDeliveryUpdates { get; set; }
    public bool? PushOrderUpdates { get; set; }
    public bool? PushPromotions { get; set; }
    public bool? PushPriceDropAlerts { get; set; }
    public bool? PushNewArrivals { get; set; }
    public string? PreferredNotificationTime { get; set; }
    public string? PreferredFrequency { get; set; }
}
