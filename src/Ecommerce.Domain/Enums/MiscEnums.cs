namespace Ecommerce.Domain.Enums;

public enum InventoryStatus
{
    InStock = 0,
    LowStock = 1,
    OutOfStock = 2,
    Backordered = 3,
    PreOrder = 4,
    Discontinued = 5
}

public enum StockMovementType
{
    Purchase = 0,
    Sale = 1,
    Return = 2,
    Adjustment = 3,
    Transfer = 4,
    Damage = 5,
    Theft = 6,
    Expired = 7,
    Initial = 8,
    Restock = 9
}

public enum WarehouseStatus
{
    Active = 0,
    Inactive = 1,
    Maintenance = 2
}

public enum SupplierStatus
{
    Active = 0,
    Inactive = 1,
    Suspended = 2,
    Blacklisted = 3
}

public enum SupplierRating
{
    Poor = 1,
    BelowAverage = 2,
    Average = 3,
    Good = 4,
    Excellent = 5
}

public enum InventoryAdjustmentReason
{
    CycleCount = 0,
    DamagedGoods = 1,
    Theft = 2,
    ReceivedGoods = 3,
    ShippingError = 4,
    VendorReturn = 5,
    Obsolete = 6,
    Promotion = 7,
    Other = 8
}

public enum NotificationType
{
    Email = 0,
    SMS = 1,
    PushNotification = 2,
    InApp = 3,
    Webhook = 4
}

public enum NotificationStatus
{
    Pending = 0,
    Sent = 1,
    Delivered = 2,
    Failed = 3,
    Read = 4,
    Cancelled = 5
}

public enum NotificationPriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Urgent = 3
}

public enum ShippingStatus
{
    Pending = 0,
    LabelCreated = 1,
    PickedUp = 2,
    InTransit = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Failed = 6,
    Returned = 7,
    Exception = 8
}

public enum ShippingMethod
{
    Standard = 0,
    Express = 1,
    Overnight = 2,
    SameDay = 3,
    Pickup = 4,
    Freight = 5,
    International = 6
}

public enum ReviewStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Flagged = 3,
    Hidden = 4
}

public enum CmsPageStatus
{
    Draft = 0,
    Published = 1,
    Archived = 2,
    Scheduled = 3
}

public enum MenuLocation
{
    Header = 0,
    Footer = 1,
    Sidebar = 2,
    Mobile = 3,
    Account = 4
}

public enum CurrencyCode
{
    USD = 0,
    EUR = 1,
    GBP = 2,
    JPY = 3,
    CAD = 4,
    AUD = 5,
    CHF = 6,
    CNY = 7,
    INR = 8,
    MXN = 9
}

public enum ReportType
{
    SalesSummary = 0,
    RevenueByProduct = 1,
    RevenueByCategory = 2,
    CustomerAcquisition = 3,
    OrderFulfillment = 4,
    InventoryReport = 5,
    ReturnReport = 6,
    MarketingCampaign = 7,
    ConversionRate = 8,
    AbandonedCart = 9,
    ProductPerformance = 10,
    CustomerLifetimeValue = 11
}

public enum ReportFormat
{
    PDF = 0,
    Excel = 1,
    CSV = 2,
    JSON = 3
}

public enum ExportStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3
}

public enum ImageType
{
    Thumbnail = 0,
    Small = 1,
    Medium = 2,
    Large = 3,
    Original = 4,
    Gallery = 5
}

public enum LogLevel2
{
    Trace = 0,
    Debug = 1,
    Information = 2,
    Warning = 3,
    Error = 4,
    Critical = 5
}
