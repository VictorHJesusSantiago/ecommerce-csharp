namespace Ecommerce.Application.DTOs.User;

public class UserDashboardDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime MemberSince { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
    public int WishlistCount { get; set; }
    public int ReviewCount { get; set; }
    public int LoyaltyPoints { get; set; }
    public string LoyaltyTier { get; set; } = string.Empty;
    public int AddressCount { get; set; }
    public int PaymentMethodCount { get; set; }
    public List<UserOrderSummaryDto> RecentOrders { get; set; } = [];
    public List<UserNotificationDto> Notifications { get; set; } = [];
    public UserPreferencesDto Preferences { get; set; } = new();
}

public class UserOrderSummaryDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public int ItemCount { get; set; }
    public DateTime OrderDate { get; set; }
    public string? FirstItemName { get; set; }
    public string? FirstItemImage { get; set; }
}

public class UserNotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string? Link { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UserPreferencesDto
{
    public string PreferredLanguage { get; set; } = "en";
    public string PreferredCurrency { get; set; } = "USD";
    public string PreferredDateFormat { get; set; } = "MM/dd/yyyy";
    public string PreferredTimeFormat { get; set; } = "12h";
    public bool EmailNotifications { get; set; } = true;
    public bool SmsNotifications { get; set; }
    public bool PushNotifications { get; set; } = true;
    public bool NewsletterSubscribed { get; set; } = true;
    public bool PriceDropAlerts { get; set; } = true;
    public bool BackInStockAlerts { get; set; } = true;
    public bool TwoFactorEnabled { get; set; }
    public string? DefaultShippingAddressId { get; set; }
    public string? DefaultPaymentMethodId { get; set; }
}

public class UpdatePreferencesRequest
{
    public string? PreferredLanguage { get; set; }
    public string? PreferredCurrency { get; set; }
    public string? PreferredDateFormat { get; set; }
    public string? PreferredTimeFormat { get; set; }
    public bool? EmailNotifications { get; set; }
    public bool? SmsNotifications { get; set; }
    public bool? PushNotifications { get; set; }
    public bool? NewsletterSubscribed { get; set; }
    public bool? PriceDropAlerts { get; set; }
    public bool? BackInStockAlerts { get; set; }
    public string? DefaultShippingAddressId { get; set; }
    public string? DefaultPaymentMethodId { get; set; }
}

public class UserRoleDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
    public DateTime AssignedAt { get; set; }
}

public class AssignRoleRequest
{
    public Guid UserId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}

public class RemoveRoleRequest
{
    public Guid UserId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}

public class UserSearchRequest
{
    public string? SearchTerm { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? LastLoginFrom { get; set; }
    public DateTime? LastLoginTo { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class UserActivityLogDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ActivityType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Location { get; set; }
    public string? Device { get; set; }
    public string? Browser { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public DateTime Timestamp { get; set; }
}

public class TwoFactorSetupDto
{
    public bool IsEnabled { get; set; }
    public string? SecretKey { get; set; }
    public string? QrCodeUrl { get; set; }
    public string? ManualEntryKey { get; set; }
    public List<string> RecoveryCodes { get; set; } = [];
    public DateTime? EnabledAt { get; set; }
}

public class EnableTwoFactorRequest
{
    public string Code { get; set; } = string.Empty;
}

public class VerifyTwoFactorRequest
{
    public string Code { get; set; } = string.Empty;
    public bool RememberDevice { get; set; }
}

public class UserAuditDto
{
    public Guid Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? EntityName { get; set; }
    public string? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime Timestamp { get; set; }
}
