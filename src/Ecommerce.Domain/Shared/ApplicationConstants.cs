namespace Ecommerce.Domain.Shared;

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
}
