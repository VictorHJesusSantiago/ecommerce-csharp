using System.Linq.Expressions;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly EcommerceDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(EcommerceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate is null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual void Attach(T entity)
    {
        _dbSet.Attach(entity);
    }

    public virtual void Detach(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
    }

    public virtual IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly EcommerceDbContext _context;
    private IProductRepository? _products;
    private ICategoryRepository? _categories;
    private IBrandRepository? _brands;
    private IOrderRepository? _orders;
    private ICartRepository? _carts;
    private IPaymentRepository? _payments;
    private IUserRepository? _users;
    private IReviewRepository? _reviews;
    private ICouponRepository? _coupons;
    private IPromotionRepository? _promotions;
    private IBannerRepository? _banners;
    private IInventoryRepository? _inventories;
    private IWarehouseRepository? _warehouses;
    private ISupplierRepository? _suppliers;
    private INotificationRepository? _notifications;
    private IEmailTemplateRepository? _emailTemplates;
    private INewsletterRepository? _newsletters;
    private IShipmentRepository? _shipments;
    private ICmsPageRepository? _cmsPages;
    private INavigationMenuRepository? _navigationMenus;
    private ISiteSettingRepository? _siteSettings;
    private IMediaFileRepository? _mediaFiles;
    private IWishlistRepository? _wishlists;

    public UnitOfWork(EcommerceDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products => _products ??= new ProductRepository(_context);
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
    public IBrandRepository Brands => _brands ??= new BrandRepository(_context);
    public IOrderRepository Orders => _orders ??= new OrderRepository(_context);
    public ICartRepository Carts => _carts ??= new CartRepository(_context);
    public IPaymentRepository Payments => _payments ??= new PaymentRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);
    public ICouponRepository Coupons => _coupons ??= new CouponRepository(_context);
    public IPromotionRepository Promotions => _promotions ??= new PromotionRepository(_context);
    public IBannerRepository Banners => _banners ??= new BannerRepository(_context);
    public IInventoryRepository Inventories => _inventories ??= new InventoryRepository(_context);
    public IWarehouseRepository Warehouses => _warehouses ??= new WarehouseRepository(_context);
    public ISupplierRepository Suppliers => _suppliers ??= new SupplierRepository(_context);
    public INotificationRepository Notifications => _notifications ??= new NotificationRepository(_context);
    public IEmailTemplateRepository EmailTemplates => _emailTemplates ??= new EmailTemplateRepository(_context);
    public INewsletterRepository Newsletters => _newsletters ??= new NewsletterRepository(_context);
    public IShipmentRepository Shipments => _shipments ??= new ShipmentRepository(_context);
    public ICmsPageRepository CmsPages => _cmsPages ??= new CmsPageRepository(_context);
    public INavigationMenuRepository NavigationMenus => _navigationMenus ??= new NavigationMenuRepository(_context);
    public ISiteSettingRepository SiteSettings => _siteSettings ??= new SiteSettingRepository(_context);
    public IMediaFileRepository MediaFiles => _mediaFiles ??= new MediaFileRepository(_context);
    public IWishlistRepository Wishlists => _wishlists ??= new WishlistRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
