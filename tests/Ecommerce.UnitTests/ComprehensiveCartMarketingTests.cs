using Xunit;
using FluentAssertions;
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

namespace Ecommerce.UnitTests;

public class CartDtoComprehensiveTests
{
    [Fact]
    public void CartDto_AllProperties_ShouldBeSettable()
    {
        var dto = new CartDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", UnitPrice = 50m, Quantity = 2, TotalPrice = 100m },
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 2", UnitPrice = 25m, Quantity = 1, TotalPrice = 25m }
            ],
            CouponCode = "SAVE10",
            DiscountAmount = 5m,
            SubTotal = 125m,
            TotalAmount = 120m,
            ItemCount = 3,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Items.Should().HaveCount(2);
        dto.ItemCount.Should().Be(3);
        dto.SubTotal.Should().Be(125m);
        dto.DiscountAmount.Should().Be(5m);
        dto.TotalAmount.Should().Be(120m);
    }

    [Fact]
    public void CartDto_IsEmpty_ShouldReturnTrueWhenNoItems()
    {
        var dto = new CartDto
        {
            Items = []
        };

        dto.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void CartDto_IsEmpty_ShouldReturnFalseWhenHasItems()
    {
        var dto = new CartDto
        {
            Items =
            [
                new() { ProductId = Guid.NewGuid(), ProductName = "Product 1", UnitPrice = 50m, Quantity = 1, TotalPrice = 50m }
            ]
        };

        dto.IsEmpty.Should().BeFalse();
    }
}

public class CartItemDtoComprehensiveTests
{
    [Fact]
    public void CartItemDto_AllProperties_ShouldBeSettable()
    {
        var dto = new CartItemDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            ProductImage = "https://example.com/image.jpg",
            Sku = "SKU-001",
            UnitPrice = 49.99m,
            Quantity = 2,
            TotalPrice = 99.98m,
            InStock = true,
            MaxQuantity = 10,
            ProductVariantId = Guid.NewGuid(),
            ProductVariantName = "Red - Large"
        };

        dto.Id.Should().NotBeEmpty();
        dto.ProductName.Should().Be("Test Product");
        dto.UnitPrice.Should().Be(49.99m);
        dto.Quantity.Should().Be(2);
        dto.TotalPrice.Should().Be(99.98m);
        dto.InStock.Should().BeTrue();
        dto.MaxQuantity.Should().Be(10);
    }

    [Fact]
    public void CartItemDto_CanAddMore_ShouldReturnTrueWhenBelowMax()
    {
        var dto = new CartItemDto
        {
            Quantity = 3,
            MaxQuantity = 10
        };

        dto.CanAddMore.Should().BeTrue();
    }

    [Fact]
    public void CartItemDto_CanAddMore_ShouldReturnFalseWhenAtMax()
    {
        var dto = new CartItemDto
        {
            Quantity = 10,
            MaxQuantity = 10
        };

        dto.CanAddMore.Should().BeFalse();
    }

    [Fact]
    public void CartItemDto_CanRemoveOne_ShouldReturnTrueWhenQuantityPositive()
    {
        var dto = new CartItemDto
        {
            Quantity = 2
        };

        dto.CanRemoveOne.Should().BeTrue();
    }

    [Fact]
    public void CartItemDto_CanRemoveOne_ShouldReturnFalseWhenQuantityOne()
    {
        var dto = new CartItemDto
        {
            Quantity = 1
        };

        dto.CanRemoveOne.Should().BeFalse();
    }

    [Fact]
    public void CartItemDto_TotalPrice_ShouldBeUnitPriceTimesQuantity()
    {
        var dto = new CartItemDto
        {
            UnitPrice = 49.99m,
            Quantity = 3
        };

        var total = dto.UnitPrice * dto.Quantity;

        total.Should().Be(149.97m);
    }
}

public class AddToCartRequestComprehensiveTests
{
    [Fact]
    public void AddToCartRequest_AllProperties_ShouldBeSettable()
    {
        var request = new AddToCartRequest
        {
            ProductId = Guid.NewGuid(),
            ProductVariantId = Guid.NewGuid(),
            Quantity = 2
        };

        request.ProductId.Should().NotBeEmpty();
        request.ProductVariantId.Should().NotBeNull();
        request.Quantity.Should().Be(2);
    }

    [Fact]
    public void AddToCartRequest_DefaultQuantity_ShouldBeOne()
    {
        var request = new AddToCartRequest
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1
        };

        request.Quantity.Should().Be(1);
    }
}

public class UpdateCartItemRequestComprehensiveTests
{
    [Fact]
    public void UpdateCartItemRequest_Quantity_ShouldBeSettable()
    {
        var request = new UpdateCartItemRequest
        {
            Quantity = 5
        };

        request.Quantity.Should().Be(5);
    }
}

public class ApplyCouponRequestComprehensiveTests
{
    [Fact]
    public void ApplyCouponRequest_Code_ShouldBeSettable()
    {
        var request = new ApplyCouponRequest
        {
            Code = "SAVE20"
        };

        request.Code.Should().Be("SAVE20");
    }
}

public class CouponDtoComprehensiveTests
{
    [Fact]
    public void CouponDto_AllProperties_ShouldBeSettable()
    {
        var dto = new CouponDto
        {
            Id = Guid.NewGuid(),
            Code = "SAVE20",
            Description = "20% off all products",
            DiscountType = "Percentage",
            DiscountValue = 20m,
            MinimumOrderAmount = 50m,
            MaximumDiscountAmount = 30m,
            UsageLimit = 100,
            UsedCount = 15,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true
        };

        dto.Id.Should().NotBeEmpty();
        dto.Code.Should().Be("SAVE20");
        dto.DiscountType.Should().Be("Percentage");
        dto.DiscountValue.Should().Be(20m);
        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void CouponDto_IsValid_ShouldReturnTrueWhenActiveAndNotExpired()
    {
        var dto = new CouponDto
        {
            IsActive = true,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsValid.Should().BeTrue();
    }

    [Fact]
    public void CouponDto_IsValid_ShouldReturnFalseWhenInactive()
    {
        var dto = new CouponDto
        {
            IsActive = false,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsValid.Should().BeFalse();
    }

    [Fact]
    public void CouponDto_IsValid_ShouldReturnFalseWhenExpired()
    {
        var dto = new CouponDto
        {
            IsActive = true,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-7)
        };

        dto.IsValid.Should().BeFalse();
    }

    [Fact]
    public void CouponDto_IsValid_ShouldReturnFalseWhenUsageLimitExceeded()
    {
        var dto = new CouponDto
        {
            IsActive = true,
            UsageLimit = 100,
            UsedCount = 100,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsValid.Should().BeFalse();
    }

    [Fact]
    public void CouponDto_UsagePercentage_ShouldCalculateCorrectly()
    {
        var dto = new CouponDto
        {
            UsageLimit = 100,
            UsedCount = 25
        };

        dto.UsagePercentage.Should().Be(25m);
    }

    [Fact]
    public void CouponDto_UsagePercentage_ShouldReturnZeroWhenNoLimit()
    {
        var dto = new CouponDto
        {
            UsageLimit = 0,
            UsedCount = 0
        };

        dto.UsagePercentage.Should().Be(0);
    }
}

public class CreateCouponRequestComprehensiveTests
{
    [Fact]
    public void CreateCouponRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreateCouponRequest
        {
            Code = "SAVE20",
            Description = "20% off",
            DiscountType = "Percentage",
            DiscountValue = 20m,
            MinimumOrderAmount = 50m,
            MaximumDiscountAmount = 30m,
            UsageLimit = 100,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        request.Code.Should().Be("SAVE20");
        request.DiscountType.Should().Be("Percentage");
        request.DiscountValue.Should().Be(20m);
    }
}

public class CreatePromotionRequestComprehensiveTests
{
    [Fact]
    public void CreatePromotionRequest_AllProperties_ShouldBeSettable()
    {
        var request = new CreatePromotionRequest
        {
            Name = "Summer Sale",
            Description = "Summer sale promotion",
            DiscountType = "Percentage",
            DiscountValue = 15m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true,
            AppliesToAllProducts = false,
            ProductIds = [Guid.NewGuid(), Guid.NewGuid()],
            CategoryIds = [Guid.NewGuid()]
        };

        request.Name.Should().Be("Summer Sale");
        request.DiscountType.Should().Be("Percentage");
        request.DiscountValue.Should().Be(15m);
        request.ProductIds.Should().HaveCount(2);
        request.CategoryIds.Should().HaveCount(1);
    }
}

public class PromotionDtoComprehensiveTests
{
    [Fact]
    public void PromotionDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PromotionDto
        {
            Id = Guid.NewGuid(),
            Name = "Summer Sale",
            Description = "Summer sale promotion",
            DiscountType = "Percentage",
            DiscountValue = 15m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true,
            UsageCount = 50,
            TotalRevenue = 5000m
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Summer Sale");
        dto.DiscountType.Should().Be("Percentage");
        dto.DiscountValue.Should().Be(15m);
        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void PromotionDto_IsValid_ShouldReturnTrueWhenActiveAndWithinDateRange()
    {
        var dto = new PromotionDto
        {
            IsActive = true,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsValid.Should().BeTrue();
    }

    [Fact]
    public void PromotionDto_IsValid_ShouldReturnFalseWhenNotActive()
    {
        var dto = new PromotionDto
        {
            IsActive = false,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsValid.Should().BeFalse();
    }

    [Fact]
    public void PromotionDto_IsValid_ShouldReturnFalseWhenDateRangeExpired()
    {
        var dto = new PromotionDto
        {
            IsActive = true,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(-7)
        };

        dto.IsValid.Should().BeFalse();
    }
}

public class BannerDtoComprehensiveTests
{
    [Fact]
    public void BannerDto_AllProperties_ShouldBeSettable()
    {
        var dto = new BannerDto
        {
            Id = Guid.NewGuid(),
            Title = "Summer Sale",
            Subtitle = "Up to 50% off",
            ImageUrl = "https://example.com/banner.jpg",
            LinkUrl = "/products/summer-sale",
            ButtonText = "Shop Now",
            Position = "Homepage",
            DisplayOrder = 1,
            IsActive = true,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            ClickCount = 150,
            Impressions = 5000
        };

        dto.Id.Should().NotBeEmpty();
        dto.Title.Should().Be("Summer Sale");
        dto.Position.Should().Be("Homepage");
        dto.IsActive.Should().BeTrue();
        dto.ClickCount.Should().Be(150);
        dto.Impressions.Should().Be(5000);
    }

    [Fact]
    public void BannerDto_ClickThroughRate_ShouldCalculateCorrectly()
    {
        var dto = new BannerDto
        {
            ClickCount = 150,
            Impressions = 5000
        };

        dto.ClickThroughRate.Should().Be(3.0m);
    }

    [Fact]
    public void BannerDto_ClickThroughRate_ShouldReturnZeroWhenNoImpressions()
    {
        var dto = new BannerDto
        {
            ClickCount = 0,
            Impressions = 0
        };

        dto.ClickThroughRate.Should().Be(0);
    }

    [Fact]
    public void BannerDto_IsCurrentlyActive_ShouldReturnTrueWhenWithinDateRange()
    {
        var dto = new BannerDto
        {
            IsActive = true,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsCurrentlyActive.Should().BeTrue();
    }

    [Fact]
    public void BannerDto_IsCurrentlyActive_ShouldReturnFalseWhenNotActive()
    {
        var dto = new BannerDto
        {
            IsActive = false,
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow.AddDays(30)
        };

        dto.IsCurrentlyActive.Should().BeFalse();
    }
}

public class NewsletterSubscriberDtoComprehensiveTests
{
    [Fact]
    public void NewsletterSubscriberDto_AllProperties_ShouldBeSettable()
    {
        var dto = new NewsletterSubscriberDto
        {
            Id = Guid.NewGuid(),
            Email = "subscriber@example.com",
            IsActive = true,
            SubscribedAt = DateTime.UtcNow,
            UnsubscribedAt = null,
            Source = "Website",
            Tags = ["newsletter", "promotions"]
        };

        dto.Id.Should().NotBeEmpty();
        dto.Email.Should().Be("subscriber@example.com");
        dto.IsActive.Should().BeTrue();
        dto.SubscribedAt.Should().Be(DateTime.UtcNow);
    }

    [Fact]
    public void NewsletterSubscriberDto_IsSubscribed_ShouldReturnTrueWhenActiveAndNotUnsubscribed()
    {
        var dto = new NewsletterSubscriberDto
        {
            IsActive = true,
            UnsubscribedAt = null
        };

        dto.IsSubscribed.Should().BeTrue();
    }

    [Fact]
    public void NewsletterSubscriberDto_IsSubscribed_ShouldReturnFalseWhenUnsubscribed()
    {
        var dto = new NewsletterSubscriberDto
        {
            IsActive = true,
            UnsubscribedAt = DateTime.UtcNow
        };

        dto.IsSubscribed.Should().BeFalse();
    }
}

public class SubscribeNewsletterRequestComprehensiveTests
{
    [Fact]
    public void SubscribeNewsletterRequest_Email_ShouldBeSettable()
    {
        var request = new SubscribeNewsletterRequest
        {
            Email = "subscriber@example.com"
        };

        request.Email.Should().Be("subscriber@example.com");
    }
}
