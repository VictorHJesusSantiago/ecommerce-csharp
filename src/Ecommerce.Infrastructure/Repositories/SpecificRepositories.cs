using Ecommerce.Application.Interfaces;

namespace Ecommerce.Infrastructure.Repositories;

public class ProductRepository : Repository<Ecommerce.Domain.Entities.Catalog.Product>, IProductRepository
{
    public ProductRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<Ecommerce.Domain.Entities.Catalog.Product?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Slug == slug, ct);
    }

    public async Task<Ecommerce.Domain.Entities.Catalog.Product?> GetBySkuAsync(string sku, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Sku == sku, ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> SearchAsync(string query, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.Name.Contains(query) || (p.Description != null && p.Description.Contains(query))).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetByCategoryAsync(Guid categoryId, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.CategoryId == categoryId && p.IsActive).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetByBrandAsync(Guid brandId, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.BrandId == brandId && p.IsActive).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetFeaturedProductsAsync(int count, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.IsActive && p.IsFeatured).Take(count).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetNewArrivalsAsync(int count, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.IsActive).OrderByDescending(p => p.CreatedAt).Take(count).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetBestSellersAsync(int count, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.IsActive).OrderByDescending(p => p.ReviewCount).ThenByDescending(p => p.AverageRating).Take(count).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetLowStockProductsAsync(int threshold, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.IsActive && p.StockQuantity <= threshold).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => ids.Contains(p.Id)).ToListAsync(ct);
    }
}

public class CategoryRepository : Repository<Ecommerce.Domain.Entities.Catalog.Category>, ICategoryRepository
{
    public CategoryRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<Ecommerce.Domain.Entities.Catalog.Category?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Slug == slug, ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Category>> GetRootCategoriesAsync(CancellationToken ct = default)
    {
        return await _dbSet.Where(c => c.ParentId == null && c.IsActive).OrderBy(c => c.SortOrder).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Catalog.Category>> GetSubCategoriesAsync(Guid parentId, CancellationToken ct = default)
    {
        return await _dbSet.Where(c => c.ParentId == parentId && c.IsActive).OrderBy(c => c.SortOrder).ToListAsync(ct);
    }
}

public class BrandRepository : Repository<Ecommerce.Domain.Entities.Catalog.Brand>, IBrandRepository
{
    public BrandRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class OrderRepository : Repository<Ecommerce.Domain.Entities.Ordering.Order>, IOrderRepository
{
    public OrderRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<Ecommerce.Domain.Entities.Ordering.Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Ordering.Order>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _dbSet.Where(o => o.UserId == userId).OrderByDescending(o => o.CreatedAt).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Ordering.Order>> GetByStatusAsync(Ecommerce.Domain.Entities.Ordering.OrderStatus status, CancellationToken ct = default)
    {
        return await _dbSet.Where(o => o.Status == status).OrderByDescending(o => o.CreatedAt).ToListAsync(ct);
    }

    public async Task<Ecommerce.Domain.Entities.Ordering.Order?> GetWithDetailsAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.Include(o => o.Items).Include(o => o.StatusHistory).FirstOrDefaultAsync(o => o.Id == id, ct);
    }
}

public class CartRepository : Repository<Ecommerce.Domain.Entities.Ordering.ShoppingCart>, ICartRepository
{
    public CartRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<Ecommerce.Domain.Entities.Ordering.ShoppingCart?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _dbSet.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId, ct);
    }

    public async Task<Ecommerce.Domain.Entities.Ordering.ShoppingCart?> GetBySessionIdAsync(string sessionId, CancellationToken ct = default)
    {
        return await _dbSet.Include(c => c.Items).FirstOrDefaultAsync(c => c.SessionId == sessionId, ct);
    }

    public async Task ClearCartAsync(Guid cartId, CancellationToken ct = default)
    {
        var cart = await _dbSet.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == cartId, ct);
        if (cart is not null)
        {
            cart.Items.Clear();
            await _context.SaveChangesAsync(ct);
        }
    }
}

public class PaymentRepository : Repository<Ecommerce.Domain.Entities.Ordering.PaymentRecord>, IPaymentRepository
{
    public PaymentRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Ecommerce.Domain.Entities.Ordering.PaymentRecord>> GetByOrderIdAsync(Guid orderId, CancellationToken ct = default)
    {
        return await _dbSet.Where(p => p.OrderId == orderId).ToListAsync(ct);
    }
}

public class UserRepository : Repository<Ecommerce.Domain.Entities.User.ApplicationUser>, IUserRepository
{
    public UserRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<Ecommerce.Domain.Entities.User.ApplicationUser?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email, ct);
    }
}

public class ReviewRepository : Repository<Ecommerce.Domain.Entities.Catalog.ProductReview>, IReviewRepository
{
    public ReviewRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class CouponRepository : Repository<Ecommerce.Domain.Entities.Marketing.Coupon>, ICouponRepository
{
    public CouponRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }

    public async Task<Ecommerce.Domain.Entities.Marketing.Coupon?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Code == code, ct);
    }
}

public class PromotionRepository : Repository<Ecommerce.Domain.Entities.Marketing.Promotion>, IPromotionRepository
{
    public PromotionRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class BannerRepository : Repository<Ecommerce.Domain.Entities.Marketing.Banner>, IBannerRepository
{
    public BannerRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class InventoryRepository : Repository<Ecommerce.Domain.Entities.Inventory.WarehouseInventory>, IInventoryRepository
{
    public InventoryRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class WarehouseRepository : Repository<Ecommerce.Domain.Entities.Inventory.Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class SupplierRepository : Repository<Ecommerce.Domain.Entities.Inventory.Supplier>, ISupplierRepository
{
    public SupplierRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class NotificationRepository : Repository<Ecommerce.Domain.Entities.Notification.NotificationRecord>, INotificationRepository
{
    public NotificationRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class EmailTemplateRepository : Repository<Ecommerce.Domain.Entities.Notification.EmailTemplate>, IEmailTemplateRepository
{
    public EmailTemplateRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class NewsletterRepository : Repository<Ecommerce.Domain.Entities.Marketing.NewsletterSubscriber>, INewsletterRepository
{
    public NewsletterRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class ShipmentRepository : Repository<Ecommerce.Domain.Entities.Shipping.Shipment>, IShipmentRepository
{
    public ShipmentRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class CmsPageRepository : Repository<Ecommerce.Domain.Entities.Cms.CmsPage>, ICmsPageRepository
{
    public CmsPageRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class NavigationMenuRepository : Repository<Ecommerce.Domain.Entities.Cms.NavigationMenu>, INavigationMenuRepository
{
    public NavigationMenuRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class SiteSettingRepository : Repository<Ecommerce.Domain.Entities.Cms.SiteSetting>, ISiteSettingRepository
{
    public SiteSettingRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class MediaFileRepository : Repository<Ecommerce.Domain.Entities.Cms.MediaFile>, IMediaFileRepository
{
    public MediaFileRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}

public class WishlistRepository : Repository<Ecommerce.Domain.Entities.User.Wishlist>, IWishlistRepository
{
    public WishlistRepository(Ecommerce.Data.EcommerceDbContext context) : base(context) { }
}
