using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Infrastructure.Caching;
using Ecommerce.Infrastructure.Messaging;
using Ecommerce.Infrastructure.Notification;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;

namespace Ecommerce.Infrastructure.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcommerceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddScoped<INewsletterRepository, NewsletterRepository>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<ICmsPageRepository, CmsPageRepository>();
        services.AddScoped<INavigationMenuRepository, NavigationMenuRepository>();
        services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
        services.AddScoped<IMediaFileRepository, MediaFileRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();

        services.AddScoped<ICacheService, InMemoryCacheService>();
        services.AddScoped<IMessageQueueService, InMemoryMessageBus>();
        services.AddScoped<IEventBus, DomainEventDispatcher>();

        services.AddScoped<IEmailService, ConsoleEmailService>();
        services.AddScoped<ISmsService, ConsoleSmsService>();
        services.AddScoped<IPushNotificationService, ConsolePushNotificationService>();
        services.AddScoped<IFileStorageService, LocalFileStorageService>();
        services.AddScoped<ICurrencyConversionService, CurrencyConversionService>();
        services.AddScoped<IIdempotencyService, IdempotencyService>();

        services.AddAutoMapper(typeof(InfrastructureServiceExtensions).Assembly);

        return services;
    }

    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisEnabled = configuration.GetValue<bool>("Redis:Enabled");
        if (redisEnabled)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:ConnectionString"] ?? "localhost:6379";
                options.InstanceName = configuration["Redis:InstanceName"] ?? "Ecommerce_";
            });
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration2 = ConfigurationOptions.Parse(configuration["Redis:ConnectionString"] ?? "localhost:6379");
                return ConnectionMultiplexer.Connect(configuration2);
            });
            services.AddScoped<ICacheService, RedisCacheService>();
        }
        else
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheService, InMemoryCacheService>();
        }
        return services;
    }

    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQEnabled = configuration.GetValue<bool>("RabbitMQ:Enabled");
        if (rabbitMQEnabled)
        {
            services.AddSingleton<IMessageQueueService, RabbitMQMessageBus>();
        }
        else
        {
            services.AddScoped<IMessageQueueService, InMemoryMessageBus>();
        }
        return services;
    }
}
