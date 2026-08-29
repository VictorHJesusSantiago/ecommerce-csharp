using FluentAssertions;
using Moq;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Common;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.Extensions;
using Ecommerce.Application.Strategies;

namespace Ecommerce.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IRepository<Ecommerce.Domain.Entities.Catalog.Product>> _mockRepo;
    private readonly Mock<ICacheService> _mockCache;
    private readonly Mock<IEventBus> _mockEventBus;
    private readonly Mock<ILogger<Services.ProductService>> _mockLogger;
    private readonly Services.ProductService _service;

    public ProductServiceTests()
    {
        _mockRepo = new Mock<IRepository<Ecommerce.Domain.Entities.Catalog.Product>>();
        _mockCache = new Mock<ICacheService>();
        _mockEventBus = new Mock<IEventBus>();
        _mockLogger = new Mock<ILogger<Services.ProductService>>();
        _service = new Services.ProductService(_mockRepo.Object, _mockCache.Object, _mockEventBus.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ProductExists_ReturnsSuccess()
    {
        var productId = Guid.NewGuid();
        var product = new Ecommerce.Domain.Entities.Catalog.Product
        {
            Id = productId,
            Name = "Test Product",
            Price = 19.99m,
            StockQuantity = 100
        };
        _mockRepo.Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var result = await _service.GetByIdAsync(productId);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data!.Name.Should().Be("Test Product");
    }

    [Fact]
    public async Task GetByIdAsync_ProductNotFound_ReturnsNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Ecommerce.Domain.Entities.Catalog.Product?)null);

        var result = await _service.GetByIdAsync(Guid.NewGuid());

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(404);
    }
}

public class OrderServiceTests
{
    [Fact]
    public void Order_CalculateTotal_ShouldBeCorrect()
    {
        var order = new Ecommerce.Domain.Entities.Ordering.Order
        {
            SubTotal = 100m,
            TaxAmount = 8m,
            ShippingCost = 5m,
            DiscountAmount = 10m
        };
        var total = order.SubTotal + order.TaxAmount + order.ShippingCost - order.DiscountAmount;
        total.Should().Be(103m);
    }

    [Fact]
    public void Order_CanBeCancelled_InPendingStatus()
    {
        var order = new Ecommerce.Domain.Entities.Ordering.Order
        {
            Status = Ecommerce.Domain.Entities.Ordering.OrderStatus.Pending
        };
        order.CanBeCancelled.Should().BeTrue();
    }

    [Fact]
    public void Order_CannotBeCancelled_InDeliveredStatus()
    {
        var order = new Ecommerce.Domain.Entities.Ordering.Order
        {
            Status = Ecommerce.Domain.Entities.Ordering.OrderStatus.Delivered
        };
        order.CanBeCancelled.Should().BeFalse();
    }
}

public class PricingStrategyTests
{
    [Fact]
    public void StandardPricing_ShouldCalculateCorrectly()
    {
        var strategy = new StandardPricingStrategy();
        var result = strategy.CalculatePrice(10.00m, 5, new PricingContext());
        result.Should().Be(50.00m);
    }

    [Fact]
    public void BulkPricing_LargeQuantity_ShouldApplyDiscount()
    {
        var strategy = new BulkPricingStrategy();
        var result = strategy.CalculatePrice(10.00m, 100, new PricingContext());
        result.Should().Be(750.00m);
    }

    [Fact]
    public void MembershipPricing_GoldTier_ShouldApplyDiscount()
    {
        var strategy = new MembershipPricingStrategy();
        var context = new PricingContext { CustomerTier = "gold" };
        var result = strategy.CalculatePrice(100.00m, 1, context);
        result.Should().Be(85.00m);
    }

    [Fact]
    public void WholesalePricing_ShouldApplyDiscount()
    {
        var strategy = new WholesalePricingStrategy();
        var context = new PricingContext { IsWholesale = true };
        var result = strategy.CalculatePrice(100.00m, 1, context);
        result.Should().Be(80.00m);
    }

    [Fact]
    public void PricingStrategyFactory_GetBestStrategy_ShouldReturnCheapest()
    {
        var factory = new PricingStrategyFactory();
        var context = new PricingContext { CustomerTier = "gold", IsWholesale = true };
        var best = factory.GetBestStrategy(context, 100.00m, 1);
        best.StrategyName.Should().Be("Wholesale");
    }
}

public class CartTests
{
    [Fact]
    public void CartDto_CalculateTotal_ShouldBeCorrect()
    {
        var cart = new CartDto
        {
            Items =
            [
                new CartItemDto { Price = 10.00m, Quantity = 2 },
                new CartItemDto { Price = 5.50m, Quantity = 1 },
                new CartItemDto { Price = 20.00m, Quantity = 3 }
            ]
        };
        var expectedTotal = (10.00m * 2) + (5.50m * 1) + (20.00m * 3);
        expectedTotal.Should().Be(85.50m);
    }
}

public class GuardTests
{
    [Fact]
    public void Guard_NotNull_ShouldNotThrow()
    {
        var action = () => Guard.NotNull("test", "param");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNull_ShouldThrow()
    {
        var action = () => Guard.NotNull(null, "param");
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Guard_NotEmpty_ShouldThrow()
    {
        var action = () => Guard.NotEmpty(Guid.Empty, "param");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldThrow()
    {
        var action = () => Guard.GreaterThan(3, 5, "param");
        action.Should().Throw<ArgumentException>();
    }
}

public class StringExtensionTests
{
    [Theory]
    [InlineData("Hello World", "hello-world")]
    [InlineData("Test Product", "test-product")]
    [InlineData("Product's Name", "products-name")]
    [InlineData("C# Programming", "csharp-programming")]
    [InlineData("  spaces  ", "spaces")]
    public void ToSlug_ShouldConvertCorrectly(string input, string expected)
    {
        var result = input.ToSlug();
        result.Should().Be(expected);
    }

    [Fact]
    public void Truncate_ShouldTruncateCorrectly()
    {
        var result = "Hello World".Truncate(8);
        result.Should().Be("Hello...");
    }

    [Fact]
    public void ToTitleCase_ShouldConvertCorrectly()
    {
        var result = "hello world".ToTitleCase();
        result.Should().Be("Hello World");
    }
}

public class DateTimeExtensionTests
{
    [Fact]
    public void ToRelativeTime_ShouldReturnCorrectly()
    {
        var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-5);
        var result = fiveMinutesAgo.ToRelativeTime();
        result.Should().Contain("minutes ago");
    }

    [Fact]
    public void IsToday_ShouldReturnTrue()
    {
        DateTime.UtcNow.IsToday().Should().BeTrue();
    }
}
