namespace Ecommerce.Application.DTOs.Marketing;

public class CouponDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public int? UsageLimitPerCustomer { get; set; }
    public bool IsActive { get; set; }
    public bool IsValid { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? RemainingUses => UsageLimit.HasValue ? UsageLimit - UsedCount : null;
}

public class CreateCouponRequest
{
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "Percentage";
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int? UsageLimitPerCustomer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdateCouponRequest
{
    public string? Description { get; set; }
    public decimal? DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ValidateCouponRequest
{
    public string Code { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public Guid? UserId { get; set; }
}

public class ValidateCouponResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public string CouponType { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public decimal NewTotal { get; set; }
    public string? Description { get; set; }
}

public class PromotionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public int? MinimumQuantity { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? CouponCode { get; set; }
    public bool IsValid => IsActive && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
}

public class BannerDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? MobileImageUrl { get; set; }
    public string? LinkUrl { get; set; }
    public string Position { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool OpenInNewTab { get; set; }
    public string? TargetUrl { get; set; }
}

public class SubscribeNewsletterRequest
{
    public string Email { get; set; } = string.Empty;
}

public class NewsletterSubscriberDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime SubscribedAt { get; set; }
    public DateTime? UnsubscribedAt { get; set; }
    public string? Source { get; set; }
}

public class DiscountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int? MinQuantity { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<DiscountTierDto> Tiers { get; set; } = [];
}

public class DiscountTierDto
{
    public Guid Id { get; set; }
    public int MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public decimal Value { get; set; }
}
