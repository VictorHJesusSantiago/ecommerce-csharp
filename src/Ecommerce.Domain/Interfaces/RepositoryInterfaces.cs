using System.Linq.Expressions;

namespace Ecommerce.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Attach(T entity);
    void Detach(T entity);
    IQueryable<T> Query();
}

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IBrandRepository Brands { get; }
    IOrderRepository Orders { get; }
    ICartRepository Carts { get; }
    IPaymentRepository Payments { get; }
    IUserRepository Users { get; }
    IReviewRepository Reviews { get; }
    ICouponRepository Coupons { get; }
    IPromotionRepository Promotions { get; }
    IBannerRepository Banners { get; }
    IInventoryRepository Inventories { get; }
    IWarehouseRepository Warehouses { get; }
    ISupplierRepository Suppliers { get; }
    INotificationRepository Notifications { get; }
    IEmailTemplateRepository EmailTemplates { get; }
    INewsletterRepository Newsletters { get; }
    IShipmentRepository Shipments { get; }
    ICmsPageRepository CmsPages { get; }
    INavigationMenuRepository NavigationMenus { get; }
    ISiteSettingRepository SiteSettings { get; }
    IMediaFileRepository MediaFiles { get; }
    IWishlistRepository Wishlists { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

public interface IProductRepository : IRepository<Entities.Catalog.Product>
{
    Task<Entities.Catalog.Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Entities.Catalog.Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Product>> GetFeaturedAsync(int count, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Product>> GetNewArrivalsAsync(int count, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Product>> GetBestSellersAsync(int count, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Product>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Entities.Catalog.Product> Products, int TotalCount)> GetPagedAsync(
        int page, int pageSize, Guid? categoryId = null, Guid? brandId = null,
        decimal? minPrice = null, decimal? maxPrice = null, string? sortBy = null,
        bool sortDescending = false, string? searchTerm = null,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}

public interface ICategoryRepository : IRepository<Entities.Catalog.Category>
{
    Task<Entities.Catalog.Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Category>> GetWithSubcategoriesAsync(Guid parentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Entities.Catalog.Category?> GetWithProductsAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IBrandRepository : IRepository<Entities.Catalog.Brand>
{
    Task<Entities.Catalog.Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Brand>> GetActiveBrandsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Catalog.Brand>> GetFeaturedBrandsAsync(int count, CancellationToken cancellationToken = default);
}

public interface IOrderRepository : IRepository<Entities.Ordering.Order>
{
    Task<Entities.Ordering.Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Ordering.Order>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<Entities.Ordering.Order?> GetWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Entities.Ordering.Order> Orders, int TotalCount)> GetPagedAsync(
        int page, int pageSize, string? status = null, Guid? customerId = null,
        DateTime? fromDate = null, DateTime? toDate = null, string? searchTerm = null,
        CancellationToken cancellationToken = default);
    Task<int> GetNextOrderNumberAsync();
}

public interface ICartRepository : IRepository<Entities.Cart.ShoppingCart>
{
    Task<Entities.Cart.ShoppingCart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Entities.Cart.ShoppingCart?> GetBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default);
    Task<Entities.Cart.ShoppingCart?> GetWithItemsAsync(Guid cartId, CancellationToken cancellationToken = default);
    Task<Entities.Cart.ShoppingCart?> GetCartWithItemsAsync(Guid? userId, string? sessionId, CancellationToken cancellationToken = default);
}

public interface IPaymentRepository : IRepository<Entities.Payment.PaymentRecord>
{
    Task<Entities.Payment.PaymentRecord?> GetByTransactionIdAsync(string transactionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Payment.PaymentRecord>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Payment.RefundRecord>> GetRefundsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
}

public interface IUserRepository : IRepository<Entities.Identity.ApplicationUser>
{
    Task<Entities.Identity.ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Entities.Identity.ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<Entities.Identity.ApplicationUser?> GetWithAddressesAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Entities.Identity.ApplicationUser?> GetWithPaymentMethodsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Identity.ApplicationUser>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Entities.Identity.ApplicationUser> Users, int TotalCount)> GetPagedAsync(
        int page, int pageSize, string? status = null, string? role = null,
        string? searchTerm = null, CancellationToken cancellationToken = default);
}

public interface IReviewRepository : IRepository<Entities.Review.ProductReview>
{
    Task<IReadOnlyList<Entities.Review.ProductReview>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Review.ProductReview>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Entities.Review.ProductReview?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<double> GetAverageRatingAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<int> GetReviewCountAsync(Guid productId, CancellationToken cancellationToken = default);
}

public interface ICouponRepository : IRepository<Entities.Marketing.Coupon>
{
    Task<Entities.Marketing.Coupon?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Marketing.Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default);
}

public interface IPromotionRepository : IRepository<Entities.Marketing.Promotion>
{
    Task<IReadOnlyList<Entities.Marketing.Promotion>> GetActivePromotionsAsync(CancellationToken cancellationToken = default);
}

public interface IBannerRepository : IRepository<Entities.Marketing.Banner>
{
    Task<IReadOnlyList<Entities.Marketing.Banner>> GetByPositionAsync(Enums.BannerPosition position, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Marketing.Banner>> GetActiveBannersAsync(CancellationToken cancellationToken = default);
}

public interface IInventoryRepository : IRepository<Entities.Inventory.WarehouseInventory>
{
    Task<Entities.Inventory.WarehouseInventory?> GetByWarehouseAndVariantAsync(Guid warehouseId, Guid variantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Inventory.WarehouseInventory>> GetLowStockItemsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Inventory.WarehouseInventory>> GetOutOfStockItemsAsync(CancellationToken cancellationToken = default);
    Task<int> GetTotalStockAsync(Guid productVariantId, CancellationToken cancellationToken = default);
}

public interface IWarehouseRepository : IRepository<Entities.Inventory.Warehouse>
{
    Task<Entities.Inventory.Warehouse?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Entities.Inventory.Warehouse?> GetDefaultWarehouseAsync(CancellationToken cancellationToken = default);
    Task<Entities.Inventory.Warehouse?> GetWithInventoryAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface ISupplierRepository : IRepository<Entities.Inventory.Supplier>
{
    Task<Entities.Inventory.Supplier?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Inventory.Supplier>> GetActiveSuppliersAsync(CancellationToken cancellationToken = default);
}

public interface INotificationRepository : IRepository<Entities.Notification.NotificationRecord>
{
    Task<IReadOnlyList<Entities.Notification.NotificationRecord>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Notification.NotificationRecord>> GetPendingNotificationsAsync(CancellationToken cancellationToken = default);
    Task<int> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default);
}

public interface IEmailTemplateRepository : IRepository<Entities.Notification.EmailTemplate>
{
    Task<Entities.Notification.EmailTemplate?> GetByTypeAsync(Enums.EmailTemplateType type, CancellationToken cancellationToken = default);
    Task<Entities.Notification.EmailTemplate?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

public interface INewsletterRepository : IRepository<Entities.Notification.NewsletterSubscriber>
{
    Task<Entities.Notification.NewsletterSubscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Notification.NewsletterSubscriber>> GetActiveSubscribersAsync(CancellationToken cancellationToken = default);
}

public interface IShipmentRepository : IRepository<Entities.Shipping.Shipment>
{
    Task<Entities.Shipping.Shipment?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Shipping.Shipment>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.Shipping.Shipment>> GetPendingShipmentsAsync(CancellationToken cancellationToken = default);
}

public interface ICmsPageRepository : IRepository<Entities.CMS.CmsPage>
{
    Task<Entities.CMS.CmsPage?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.CMS.CmsPage>> GetPublishedPagesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.CMS.CmsPage>> GetNavigationPagesAsync(CancellationToken cancellationToken = default);
}

public interface INavigationMenuRepository : IRepository<Entities.CMS.NavigationMenu>
{
    Task<Entities.CMS.NavigationMenu?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Entities.CMS.NavigationMenu?> GetWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.CMS.NavigationMenu>> GetByLocationAsync(Enums.MenuLocation location, CancellationToken cancellationToken = default);
}

public interface ISiteSettingRepository : IRepository<Entities.CMS.SiteSetting>
{
    Task<Entities.CMS.SiteSetting?> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
    Task<IDictionary<string, string?>> GetByGroupAsync(string group, CancellationToken cancellationToken = default);
    Task<IDictionary<string, string?>> GetAllSettingsAsync(CancellationToken cancellationToken = default);
}

public interface IMediaFileRepository : IRepository<Entities.CMS.MediaFile>
{
    Task<Entities.CMS.MediaFile?> GetByFileNameAsync(string fileName, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entities.CMS.MediaFile>> GetByFolderAsync(string folder, CancellationToken cancellationToken = default);
}

public interface IWishlistRepository : IRepository<Entities.Identity.UserWishlist>
{
    Task<Entities.Identity.UserWishlist?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Entities.Identity.UserWishlist?> GetByShareTokenAsync(string shareToken, CancellationToken cancellationToken = default);
    Task<Entities.Identity.UserWishlist?> GetWithItemsAsync(Guid wishlistId, CancellationToken cancellationToken = default);
}
