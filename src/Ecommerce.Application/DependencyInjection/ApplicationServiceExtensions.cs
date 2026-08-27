using FluentValidation;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IExportService, ExportService>();
        services.AddScoped<IShippingCalculatorService, ShippingCalculatorService>();
        services.AddScoped<ICurrencyConversionService, CurrencyConversionService>();
        services.AddScoped<IIdempotencyService, IdempotencyService>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

        return services;
    }

    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
        services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
        services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
        services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorage"));
        services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
        services.Configure<PayPalSettings>(configuration.GetSection("PayPal"));
        services.Configure<RedisSettings>(configuration.GetSection("Redis"));
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
        services.Configure<FileUploadSettings>(configuration.GetSection("FileUpload"));
        services.Configure<AppSettings>(configuration.GetSection("App"));

        return services;
    }
}
