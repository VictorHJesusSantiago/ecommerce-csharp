namespace Ecommerce.Domain.Enums;

public enum CartStatus
{
    Active = 0,
    Abandoned = 1,
    Converted = 2,
    Expired = 3,
    Merged = 4
}

public enum WishlistStatus
{
    Active = 0,
    Shared = 1,
    Archived = 2
}

public enum CouponType
{
    Percentage = 0,
    FixedAmount = 1,
    FreeShipping = 2,
    BuyXGetY = 3,
    BundleDiscount = 4
}

public enum DiscountType
{
    Percentage = 0,
    FixedAmount = 1,
    BuyXGetY = 2,
    TieredDiscount = 3,
    VolumeDiscount = 4
}

public enum PromotionType
{
    FlashSale = 0,
    SeasonalSale = 1,
    ClearSale = 2,
    BuyOneGetOne = 3,
    BundlePromotion = 4,
    LoyaltyRewards = 5,
    ReferralBonus = 6,
    WelcomeOffer = 7,
    ExitIntentPopup = 8
}

public enum CouponStatus
{
    Active = 0,
    Inactive = 1,
    Expired = 2,
    FullyRedeemed = 3,
    Scheduled = 4
}

public enum BannerPosition
{
    HomeTop = 0,
    HomeMiddle = 1,
    HomeBottom = 2,
    CategoryTop = 3,
    Sidebar = 4,
    Footer = 5,
    Popup = 6,
    CategoryBanner = 7,
    ProductPageTop = 8
}

public enum BannerStatus
{
    Draft = 0,
    Active = 1,
    Inactive = 2,
    Scheduled = 3
}

public enum NewsletterStatus
{
    Active = 0,
    Inactive = 1,
    Unsubscribed = 2,
    Bounced = 3,
    Spam = 4
}

public enum EmailTemplateType
{
    Welcome = 0,
    OrderConfirmation = 1,
    OrderShipped = 2,
    OrderDelivered = 3,
    OrderCancelled = 4,
    PasswordReset = 5,
    EmailVerification = 6,
    Newsletter = 7,
    Promotional = 8,
    AbandonedCart = 9,
    ReviewRequest = 10,
    Invoice = 11,
    RefundConfirmation = 12,
    AccountUpdate = 13,
    LowStockAlert = 14
}
