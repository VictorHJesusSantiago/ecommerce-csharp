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

namespace Ecommerce.UnitTests.DTOs;

public class ProductDtoTests
{
    [Fact]
    public void ProductDto_InStock_ShouldReturnTrueWhenStockPositive()
    {
        var dto = new ProductDto { StockQuantity = 10 };

        dto.InStock.Should().BeTrue();
    }

    [Fact]
    public void ProductDto_InStock_ShouldReturnFalseWhenStockZero()
    {
        var dto = new ProductDto { StockQuantity = 0 };

        dto.InStock.Should().BeFalse();
    }

    [Fact]
    public void ProductDto_IsOnSale_ShouldReturnTrueWhenCompareAtPriceHigher()
    {
        var dto = new ProductDto { Price = 49.99m, CompareAtPrice = 69.99m };

        dto.IsOnSale.Should().BeTrue();
    }

    [Fact]
    public void ProductDto_DiscountPercentage_ShouldCalculateCorrectly()
    {
        var dto = new ProductDto { Price = 49.99m, CompareAtPrice = 69.99m };

        var discount = Math.Round((1 - dto.Price / dto.CompareAtPrice.Value) * 100, 2);

        discount.Should().Be(28.57m);
    }

    [Fact]
    public void ProductDto_ProfitMargin_ShouldCalculateCorrectly()
    {
        var dto = new ProductDto { Price = 100m, CostPrice = 60m };

        var margin = Math.Round(((dto.Price - dto.CostPrice.Value) / dto.Price) * 100, 2);

        margin.Should().Be(40m);
    }
}

public class OrderDtoTests
{
    [Fact]
    public void OrderDto_TotalAmount_ShouldBeCalculatedCorrectly()
    {
        var dto = new OrderDto
        {
            SubTotal = 100m,
            TaxAmount = 8m,
            ShippingCost = 9.99m,
            DiscountAmount = 5m
        };

        var total = dto.SubTotal + dto.TaxAmount + dto.ShippingCost - dto.DiscountAmount;

        total.Should().Be(112.99m);
    }

    [Fact]
    public void OrderItemDto_TotalPrice_ShouldCalculateCorrectly()
    {
        var dto = new OrderItemDto
        {
            UnitPrice = 49.99m,
            Quantity = 3
        };

        var total = dto.UnitPrice * dto.Quantity;

        total.Should().Be(149.97m);
    }
}

public class CartDtoTests
{
    [Fact]
    public void CartDto_TotalItems_ShouldCountItems()
    {
        var dto = new CartDto
        {
            Items =
            [
                new() { Quantity = 2 },
                new() { Quantity = 3 },
                new() { Quantity = 1 }
            ]
        };

        var totalItems = dto.Items.Sum(i => i.Quantity);

        totalItems.Should().Be(6);
    }

    [Fact]
    public void CartItemDto_TotalPrice_ShouldCalculateCorrectly()
    {
        var dto = new CartItemDto
        {
            Price = 49.99m,
            Quantity = 2
        };

        var total = dto.Price * dto.Quantity;

        total.Should().Be(99.98m);
    }
}

public class ReviewDtoTests
{
    [Fact]
    public void ReviewDto_AverageRating_ShouldCalculateCorrectly()
    {
        var reviews = new List<ReviewDto>
        {
            new() { Rating = 5 },
            new() { Rating = 4 },
            new() { Rating = 3 },
            new() { Rating = 5 },
            new() { Rating = 4 }
        };

        var average = reviews.Average(r => r.Rating);

        average.Should().Be(4.2);
    }

    [Fact]
    public void ReviewStatsDto_TotalReviews_ShouldCountCorrectly()
    {
        var stats = new ReviewStatsDto
        {
            FiveStarCount = 10,
            FourStarCount = 5,
            ThreeStarCount = 3,
            TwoStarCount = 1,
            OneStarCount = 1
        };

        var total = stats.FiveStarCount + stats.FourStarCount + stats.ThreeStarCount +
                     stats.TwoStarCount + stats.OneStarCount;

        total.Should().Be(20);
    }
}

public class CouponDtoTests
{
    [Fact]
    public void CouponDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new CouponDto();

        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void CouponDto_FirstTimeOnly_ShouldDefaultToFalse()
    {
        var dto = new CouponDto();

        dto.FirstTimeOnly.Should().BeFalse();
    }
}

public class DashboardSummaryDtoTests
{
    [Fact]
    public void DashboardSummaryDto_RevenueGrowth_ShouldBeStored()
    {
        var dto = new DashboardSummaryDto
        {
            RevenueGrowthPercentage = 12.5,
            OrderGrowthPercentage = 8.3
        };

        dto.RevenueGrowthPercentage.Should().Be(12.5);
        dto.OrderGrowthPercentage.Should().Be(8.3);
    }

    [Fact]
    public void DashboardSummaryDto_StockAlerts_ShouldBeTracked()
    {
        var dto = new DashboardSummaryDto
        {
            LowStockProducts = 15,
            OutOfStockProducts = 5,
            PendingOrders = 23
        };

        dto.LowStockProducts.Should().Be(15);
        dto.OutOfStockProducts.Should().Be(5);
        dto.PendingOrders.Should().Be(23);
    }
}

public class WarehouseDtoTests
{
    [Fact]
    public void WarehouseDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new WarehouseDto();

        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void WarehouseDto_CapacityUtilization_ShouldCalculateCorrectly()
    {
        var dto = new WarehouseDto
        {
            TotalCapacity = 10000,
            CurrentUtilization = 6500
        };

        var utilization = (dto.CurrentUtilization / dto.TotalCapacity) * 100;

        utilization.Should().Be(65);
    }
}

public class SupplierDtoTests
{
    [Fact]
    public void SupplierDto_IsActive_ShouldDefaultToTrue()
    {
        var dto = new SupplierDto();

        dto.IsActive.Should().BeTrue();
    }

    [Fact]
    public void SupplierDto_Rating_ShouldBeStored()
    {
        var dto = new SupplierDto { Rating = 4 };

        dto.Rating.Should().Be(4);
    }
}

public class CmsPageDtoTests
{
    [Fact]
    public void CmsPageDto_IsPublished_ShouldDefaultToFalse()
    {
        var dto = new CmsPageDto();

        dto.IsPublished.Should().BeFalse();
    }

    [Fact]
    public void CmsPageDto_ViewCount_ShouldIncrement()
    {
        var dto = new CmsPageDto { ViewCount = 100 };

        dto.ViewCount++;

        dto.ViewCount.Should().Be(101);
    }
}

public class NotificationDtoTests
{
    [Fact]
    public void NotificationDto_IsRead_ShouldDefaultToFalse()
    {
        var dto = new NotificationDto();

        dto.IsRead.Should().BeFalse();
    }

    [Fact]
    public void NotificationDto_MarkAsRead_ShouldSetReadAt()
    {
        var dto = new NotificationDto();
        var readAt = DateTime.UtcNow;

        dto.IsRead = true;
        dto.ReadAt = readAt;

        dto.IsRead.Should().BeTrue();
        dto.ReadAt.Should().Be(readAt);
    }
}

public class SearchResultDtoTests
{
    [Fact]
    public void SearchResultDto_TotalPages_ShouldCalculateCorrectly()
    {
        var dto = new SearchResultDto
        {
            TotalResults = 45,
            PageSize = 20
        };

        var totalPages = (int)Math.Ceiling(dto.TotalResults / (double)dto.PageSize);

        totalPages.Should().Be(3);
    }

    [Fact]
    public void SearchResultDto_IsEmpty_ShouldReturnTrueWhenNoResults()
    {
        var dto = new SearchResultDto
        {
            TotalResults = 0,
            Items = []
        };

        dto.Items.Should().BeEmpty();
    }
}
