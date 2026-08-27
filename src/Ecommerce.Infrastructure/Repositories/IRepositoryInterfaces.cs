using Ecommerce.Application.Interfaces;

namespace Ecommerce.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Ecommerce.Domain.Entities.Catalog.Product>
{
    Task<Ecommerce.Domain.Entities.Catalog.Product?> GetBySlugAsync(string slug, CancellationToken ct = default);
    Task<Ecommerce.Domain.Entities.Catalog.Product?> GetBySkuAsync(string sku, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> SearchAsync(string query, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetByCategoryAsync(Guid categoryId, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetByBrandAsync(Guid brandId, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetFeaturedProductsAsync(int count, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetNewArrivalsAsync(int count, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetBestSellersAsync(int count, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetLowStockProductsAsync(int threshold, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
}

public interface ICategoryRepository : IRepository<Ecommerce.Domain.Entities.Catalog.Category>
{
    Task<Ecommerce.Domain.Entities.Catalog.Category?> GetBySlugAsync(string slug, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Category>> GetRootCategoriesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Category>> GetSubCategoriesAsync(Guid parentId, CancellationToken ct = default);
}

public interface IBrandRepository : IRepository<Ecommerce.Domain.Entities.Catalog.Brand> { }
public interface IOrderRepository : IRepository<Ecommerce.Domain.Entities.Ordering.Order>
{
    Task<Ecommerce.Domain.Entities.Ordering.Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Ordering.Order>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Ordering.Order>> GetByStatusAsync(Ecommerce.Domain.Entities.Ordering.OrderStatus status, CancellationToken ct = default);
    Task<Ecommerce.Domain.Entities.Ordering.Order?> GetWithDetailsAsync(Guid id, CancellationToken ct = default);
}

public interface ICartRepository : IRepository<Ecommerce.Domain.Entities.Ordering.ShoppingCart>
{
    Task<Ecommerce.Domain.Entities.Ordering.ShoppingCart?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<Ecommerce.Domain.Entities.Ordering.ShoppingCart?> GetBySessionIdAsync(string sessionId, CancellationToken ct = default);
    Task ClearCartAsync(Guid cartId, CancellationToken ct = default);
}

public interface IPaymentRepository : IRepository<Ecommerce.Domain.Entities.Ordering.PaymentRecord>
{
    Task<IReadOnlyList<Ecommerce.Domain.Entities.Ordering.PaymentRecord>> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default);
}

public interface IUserRepository : IRepository<Ecommerce.Domain.Entities.User.ApplicationUser>
{
    Task<Ecommerce.Domain.Entities.User.ApplicationUser?> GetByEmailAsync(string email, CancellationToken ct = default);
}

public interface IReviewRepository : IRepository<Ecommerce.Domain.Entities.Catalog.ProductReview> { }
public interface ICouponRepository : IRepository<Ecommerce.Domain.Entities.Marketing.Coupon>
{
    Task<Ecommerce.Domain.Entities.Marketing.Coupon?> GetByCodeAsync(string code, CancellationToken ct = default);
}

public interface IPromotionRepository : IRepository<Ecommerce.Domain.Entities.Marketing.Promotion> { }
public interface IBannerRepository : IRepository<Ecommerce.Domain.Entities.Marketing.Banner> { }
public interface IInventoryRepository : IRepository<Ecommerce.Domain.Entities.Inventory.WarehouseInventory> { }
public interface IWarehouseRepository : IRepository<Ecommerce.Domain.Entities.Inventory.Warehouse> { }
public interface ISupplierRepository : IRepository<Ecommerce.Domain.Entities.Inventory.Supplier> { }
public interface INotificationRepository : IRepository<Ecommerce.Domain.Entities.Notification.NotificationRecord> { }
public interface IEmailTemplateRepository : IRepository<Ecommerce.Domain.Entities.Notification.EmailTemplate> { }
public interface INewsletterRepository : IRepository<Ecommerce.Domain.Entities.Marketing.NewsletterSubscriber> { }
public interface IShipmentRepository : IRepository<Ecommerce.Domain.Entities.Shipping.Shipment> { }
public interface ICmsPageRepository : IRepository<Ecommerce.Domain.Entities.Cms.CmsPage> { }
public interface INavigationMenuRepository : IRepository<Ecommerce.Domain.Entities.Cms.NavigationMenu> { }
public interface ISiteSettingRepository : IRepository<Ecommerce.Domain.Entities.Cms.SiteSetting> { }
public interface IMediaFileRepository : IRepository<Ecommerce.Domain.Entities.Cms.MediaFile> { }
public interface IWishlistRepository : IRepository<Ecommerce.Domain.Entities.User.Wishlist> { }
