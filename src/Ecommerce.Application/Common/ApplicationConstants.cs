namespace Ecommerce.Application.Common;

public static class ApplicationConstants
{
    public static class CacheKeys
    {
        public const string Products = "products";
        public const string Categories = "categories";
        public const string ProductById = "product_{0}";
        public const string ProductBySlug = "product_slug_{0}";
        public const string CategoriesTree = "categories_tree";
        public const string FeaturedProducts = "featured_products";
        public const string BestSellers = "best_sellers";
        public const string NewArrivals = "new_arrivals";
        public const string UserCart = "user_cart_{0}";
        public const string SessionCart = "session_cart_{0}";
        public const string SearchResults = "search_{0}";
        public const string SiteSettings = "site_settings";
        public const string NavigationMenus = "navigation_menus";
        public const string Banners = "banners_{0}";
        public const string Dashboard = "dashboard";
        public const string SalesReport = "sales_report_{0}";
        public const string InventoryReport = "inventory_report";
        public const string CurrencyRates = "currency_rates";
        public const string Idempotent = "idempotent_{0}";
    }

    public static class CacheDurations
    {
        public static readonly TimeSpan Short = TimeSpan.FromMinutes(5);
        public static readonly TimeSpan Medium = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan Long = TimeSpan.FromHours(1);
        public static readonly TimeSpan VeryLong = TimeSpan.FromHours(6);
        public static readonly TimeSpan Day = TimeSpan.FromHours(24);
    }

    public static class Queues
    {
        public const string OrderCreated = "order.created";
        public const string OrderUpdated = "order.updated";
        public const string OrderCancelled = "order.cancelled";
        public const string PaymentProcessed = "payment.processed";
        public const string PaymentFailed = "payment.failed";
        public const string RefundProcessed = "refund.processed";
        public const string EmailQueue = "email.queue";
        public const string SmsQueue = "sms.queue";
        public const string PushQueue = "push.queue";
        public const string InventoryUpdate = "inventory.update";
        public const string LowStockAlert = "inventory.lowstock";
        public const string ReportGeneration = "report.generation";
    }

    public static class SignalRHubs
    {
        public const string OrderNotifications = "/hubs/orders";
        public const string AdminDashboard = "/hubs/admin";
        public const string UserNotifications = "/hubs/notifications";
        public const string InventoryUpdates = "/hubs/inventory";
    }

    public static class StatusMessages
    {
        public const string Success = "Operation completed successfully.";
        public const string NotFound = "The requested resource was not found.";
        public const string Unauthorized = "You are not authorized to perform this action.";
        public const string Forbidden = "You do not have permission to perform this action.";
        public const string ValidationFailed = "One or more validation errors occurred.";
        public const string InternalError = "An internal error occurred. Please try again later.";
        public const string RateLimitExceeded = "Rate limit exceeded. Please try again later.";
        public const string Conflict = "The request conflicts with the current state of the resource.";
    }

    public static class RegexPatterns
    {
        public const string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public const string Phone = @"^\+?[1-9]\d{1,14}$";
        public const string Url = @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$";
        public const string StrongPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    }

    public static class FileUpload
    {
        public const int MaxFileSizeMB = 10;
        public const int MaxFileSizeBytes = MaxFileSizeMB * 1024 * 1024;
        public static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg"];
        public static readonly string[] AllowedDocumentExtensions = [".pdf", ".doc", ".docx", ".xls", ".xlsx"];
        public const string ImageContentType = "image/";
        public const string DocumentsPath = "uploads/documents";
        public const string ImagesPath = "uploads/images";
        public const string ProductsPath = "uploads/products";
        public const string AvatarsPath = "uploads/avatars";
    }
}
