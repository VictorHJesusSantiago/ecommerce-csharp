using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Entities.Marketing;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public int? UsageLimitPerCustomer { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsReusable => UsageLimit is null || UsedCount < UsageLimit;
    public bool IsValid => IsActive && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate && IsReusable;

    public virtual ICollection<CouponUsage> Usages { get; set; } = new List<CouponUsage>();
}

public class CouponUsage : BaseEntity
{
    public Guid CouponId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    public virtual Coupon Coupon { get; set; } = null!;
    public virtual User.ApplicationUser User { get; set; } = null!;
    public virtual Ordering.Order Order { get; set; } = null!;
}

public class Discount : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal Value { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int? MinQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual ICollection<DiscountTier> Tiers { get; set; } = new List<DiscountTier>();
}

public class DiscountTier : BaseEntity
{
    public Guid DiscountId { get; set; }
    public int MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public decimal Value { get; set; }
    public virtual Discount Discount { get; set; } = null!;
}

public class Promotion : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal? MinimumOrderAmount { get; set; }
    public int? MinimumQuantity { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public int? UsageLimitPerCustomer { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? CouponCode { get; set; }
    public virtual ICollection<PromotionUsage> Usages { get; set; } = new List<PromotionUsage>();
}

public class PromotionUsage : BaseEntity
{
    public Guid PromotionId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    public virtual Promotion Promotion { get; set; } = null!;
    public virtual User.ApplicationUser User { get; set; } = null!;
}

public class Banner : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string? MobileImageUrl { get; set; }
    public string? LinkUrl { get; set; }
    public BannerPosition Position { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? DisplayOrder { get; set; }
    public string? TargetUrl { get; set; }
    public bool OpenInNewTab { get; set; }
}

public class NewsletterSubscriber : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UnsubscribedAt { get; set; }
    public string? Source { get; set; }
    public virtual User.ApplicationUser? User { get; set; }
}

public enum DiscountType
{
    Percentage = 0,
    FixedAmount = 1,
    FreeShipping = 2,
    BuyOneGetOne = 3
}

public enum BannerPosition
{
    HomeTop = 0,
    HomeMiddle = 1,
    HomeBottom = 2,
    CategoryTop = 3,
    CategorySidebar = 4,
    ProductTop = 5,
    Footer = 6,
    Popup = 7,
    Sidebar = 8
}
