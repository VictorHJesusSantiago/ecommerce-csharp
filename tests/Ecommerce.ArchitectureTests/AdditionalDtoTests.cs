using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Payment;
using Ecommerce.Application.DTOs.Shipping;

namespace Ecommerce.ArchitectureTests;

public class AdditionalDtoTests
{
    [Fact]
    public void CartDto_ShouldHaveRequiredProperties()
    {
        var dto = new CartDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Items = new List<CartItemDto>
            {
                new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), ProductName = "Widget", Quantity = 2, UnitPrice = 19.99m }
            },
            CouponCode = "SAVE10",
            SubTotal = 39.98m,
            Discount = 4.00m,
            Total = 35.98m,
            ItemCount = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.ItemCount.Should().Be(2);
        dto.Total.Should().Be(35.98m);
    }

    [Fact]
    public void CouponDto_ShouldHaveRequiredProperties()
    {
        var dto = new CouponDto
        {
            Id = Guid.NewGuid(),
            Code = "SAVE20",
            Name = "20% Off",
            Description = "Get 20% off your order",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 20,
            MinimumOrderAmount = 50,
            MaximumDiscountAmount = 100,
            UsageLimit = 100,
            UsedCount = 15,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true,
            ApplicableProductIds = new List<Guid>(),
            ApplicableCategoryIds = new List<Guid>(),
            CreatedAt = DateTime.UtcNow
        };

        dto.DiscountType.Should().Be(DiscountType.Percentage);
        dto.UsedCount.Should().Be(15);
    }

    [Fact]
    public void BannerDto_ShouldHaveRequiredProperties()
    {
        var dto = new BannerDto
        {
            Id = Guid.NewGuid(),
            Title = "Summer Sale",
            Subtitle = "Up to 50% off",
            ImageUrl = "/images/banner.jpg",
            LinkUrl = "/products/sale",
            Position = BannerPosition.Homepage,
            SortOrder = 0,
            IsActive = true,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            ClickCount = 150,
            Impressions = 5000,
            CreatedAt = DateTime.UtcNow
        };

        dto.Position.Should().Be(BannerPosition.Homepage);
        dto.ClickCount.Should().Be(150);
    }

    [Fact]
    public void WarehouseDto_ShouldHaveRequiredProperties()
    {
        var dto = new WarehouseDto
        {
            Id = Guid.NewGuid(),
            Name = "Main Warehouse",
            Code = "WH-001",
            Address = "456 Industrial Ave",
            City = "Chicago",
            State = "IL",
            Country = "US",
            PostalCode = "60601",
            Phone = "+13125551234",
            Email = "warehouse@example.com",
            Manager = "Bob Builder",
            Capacity = 10000,
            IsActive = true,
            TotalProducts = 500,
            TotalValue = 250000m,
            CreatedAt = DateTime.UtcNow
        };

        dto.Code.Should().Be("WH-001");
        dto.TotalValue.Should().Be(250000m);
    }

    [Fact]
    public void SupplierDto_ShouldHaveRequiredProperties()
    {
        var dto = new SupplierDto
        {
            Id = Guid.NewGuid(),
            Name = "Tech Supplies Inc",
            ContactPerson = "Alice Johnson",
            Email = "alice@techsupplies.com",
            Phone = "+14155556789",
            Website = "https://techsupplies.com",
            Address = "789 Commerce St",
            City = "San Francisco",
            State = "CA",
            Country = "US",
            PostalCode = "94102",
            LeadTimeDays = 14,
            PaymentTerms = "Net 30",
            IsActive = true,
            ProductCount = 120,
            AverageRating = 4.2,
            TotalOrders = 50,
            CreatedAt = DateTime.UtcNow
        };

        dto.LeadTimeDays.Should().Be(14);
        dto.AverageRating.Should().Be(4.2);
    }

    [Fact]
    public void ReviewDto_ShouldHaveRequiredProperties()
    {
        var dto = new ReviewDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UserName = "JaneDoe",
            Rating = 5,
            Title = "Amazing!",
            Content = "Best product I've ever bought.",
            Pros = "High quality, fast shipping",
            Cons = "None",
            RecommendToFriend = true,
            IsVerifiedPurchase = true,
            HelpfulCount = 10,
            NotHelpfulCount = 1,
            Images = new List<string> { "/images/review1.jpg" },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Rating.Should().Be(5);
        dto.HelpfulCount.Should().Be(10);
    }

    [Fact]
    public void NotificationDto_ShouldHaveRequiredProperties()
    {
        var dto = new NotificationDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Order Shipped",
            Message = "Your order #ORD-001 has been shipped.",
            Type = "OrderUpdate",
            IsRead = false,
            ActionUrl = "/orders/ORD-001",
            CreatedAt = DateTime.UtcNow
        };

        dto.IsRead.Should().BeFalse();
        dto.ActionUrl.Should().Be("/orders/ORD-001");
    }

    [Fact]
    public void SearchResultDto_ShouldHaveRequiredProperties()
    {
        var dto = new SearchResultDto
        {
            Query = "laptop",
            TotalResults = 250,
            CurrentPage = 1,
            PageSize = 20,
            TotalPages = 13,
            Results = new List<SearchResultItemDto>
            {
                new() { ProductId = Guid.NewGuid(), Name = "Laptop Pro", Price = 999.99m, Rating = 4.5m, ImageUrl = "/images/laptop.jpg", Url = "/products/laptop-pro" }
            },
            Categories = new List<SearchFacetDto> { new() { Name = "Electronics", Count = 100 } },
            Brands = new List<SearchFacetDto> { new() { Name = "TechBrand", Count = 50 } },
            PriceRanges = new List<SearchPriceRangeDto>
            {
                new() { Min = 0, Max = 500, Count = 50 },
                new() { Min = 500, Max = 1000, Count = 100 },
                new() { Min = 1000, Max = 2000, Count = 75 }
            },
            Suggestions = new List<string> { "laptop bag", "laptop stand" },
            ElapsedMilliseconds = 45
        };

        dto.TotalResults.Should().Be(250);
        dto.Results.Should().HaveCount(1);
        dto.Suggestions.Should().Contain("laptop bag");
    }

    [Fact]
    public void RevenueReportDto_ShouldHaveRequiredProperties()
    {
        var dto = new RevenueReportDto
        {
            TotalRevenue = 150000m,
            TotalOrders = 750,
            AverageOrderValue = 200m,
            RevenueGrowth = 12.5m,
            DailyRevenue = new List<DailyRevenueDto>
            {
                new() { Date = DateTime.UtcNow.Date, Revenue = 5000m, OrderCount = 25 }
            }
        };

        dto.AverageOrderValue.Should().Be(200m);
    }

    [Fact]
    public void StripePaymentDto_ShouldHaveRequiredProperties()
    {
        var dto = new StripePaymentDto
        {
            PaymentIntentId = "pi_1234567890",
            Amount = 10000,
            Currency = "usd",
            Status = "succeeded",
            ClientSecret = "cs_secret_123",
            PaymentMethod = "pm_card_visa",
            ReceiptUrl = "https://receipt.stripe.com/123"
        };

        dto.Amount.Should().Be(10000);
        dto.Currency.Should().Be("usd");
    }

    [Fact]
    public void ShippingRateDto_ShouldHaveRequiredProperties()
    {
        var dto = new ShippingRateDto
        {
            Id = Guid.NewGuid(),
            Name = "Standard Shipping",
            Carrier = "USPS",
            EstimatedDays = 5,
            Cost = 9.99m,
            MinWeight = 0,
            MaxWeight = 50,
            IsTracked = true,
            IsActive = true
        };

        dto.Cost.Should().Be(9.99m);
        dto.IsTracked.Should().BeTrue();
    }

    [Fact]
    public void PageDto_ShouldHaveRequiredProperties()
    {
        var dto = new CmsPageDto
        {
            Id = Guid.NewGuid(),
            Title = "About Us",
            Slug = "about-us",
            Content = "<p>We are a company...</p>",
            Excerpt = "Learn about us",
            FeaturedImage = "/images/about.jpg",
            MetaTitle = "About Us | ECommerce",
            MetaDescription = "Learn about our company",
            IsPublished = true,
            PublishedAt = DateTime.UtcNow,
            Author = "Admin",
            ViewCount = 1500,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Slug.Should().Be("about-us");
        dto.ViewCount.Should().Be(1500);
    }
}
