using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Common;

namespace Ecommerce.ArchitectureTests;

public class ReportSearchCmsDtoTests
{
    [Fact]
    public void DashboardSummaryDto_ShouldHaveRequiredProperties()
    {
        var dto = new DashboardSummaryDto
        {
            TotalRevenue = 150000m,
            TotalOrders = 750,
            TotalCustomers = 500,
            TotalProducts = 200,
            RevenueGrowth = 12.5m,
            OrderGrowth = 8.3m,
            CustomerGrowth = 5.2m,
            AverageOrderValue = 200m,
            ConversionRate = 3.2m,
            LowStockProducts = 15,
            PendingOrders = 8,
            RecentOrders = new List<OrderDto>(),
            TopProducts = new List<TopProductDto>(),
            RevenueChart = new List<DailyRevenueDto>()
        };

        dto.AverageOrderValue.Should().Be(200m);
        dto.ConversionRate.Should().Be(3.2m);
    }

    [Fact]
    public void TopProductDto_ShouldHaveRequiredProperties()
    {
        var dto = new TopProductDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Laptop Pro",
            ImageUrl = "/images/laptop.jpg",
            TotalSold = 150,
            TotalRevenue = 149985m,
            AverageRating = 4.5m,
            ReviewCount = 42,
            StockQuantity = 25
        };

        dto.TotalRevenue.Should().Be(149985m);
    }

    [Fact]
    public void DailyRevenueDto_ShouldHaveRequiredProperties()
    {
        var dto = new DailyRevenueDto
        {
            Date = DateTime.UtcNow.Date,
            Revenue = 5000m,
            OrderCount = 25,
            AverageOrderValue = 200m
        };

        dto.Revenue.Should().Be(5000m);
    }

    [Fact]
    public void SalesReportDto_ShouldHaveRequiredProperties()
    {
        var dto = new SalesReportDto
        {
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            TotalSales = 45000m,
            TotalOrders = 225,
            AverageOrderValue = 200m,
            SalesByCategory = new Dictionary<string, decimal> { { "Electronics", 30000m }, { "Clothing", 15000m } },
            SalesByDay = new List<DailyRevenueDto>(),
            TopProducts = new List<TopProductDto>()
        };

        dto.SalesByCategory.Should().ContainKey("Electronics");
    }

    [Fact]
    public void SearchFilterDto_ShouldHaveRequiredProperties()
    {
        var dto = new SearchFilterDto
        {
            Categories = new List<SearchFacetDto> { new() { Name = "Electronics", Count = 100 } },
            Brands = new List<SearchFacetDto> { new() { Name = "Dell", Count = 30 } },
            PriceRanges = new List<SearchPriceRangeDto> { new() { Min = 0, Max = 500, Count = 50 } },
            Ratings = new List<SearchRatingFacetDto> { new() { Rating = 5, Count = 20 } },
            MinPrice = 0,
            MaxPrice = 5000,
            InStockOnly = false
        };

        dto.Categories.Should().HaveCount(1);
    }

    [Fact]
    public void SearchResultItemDto_ShouldHaveRequiredProperties()
    {
        var dto = new SearchResultItemDto
        {
            ProductId = Guid.NewGuid(),
            Name = "Laptop Pro",
            Description = "A powerful laptop",
            Price = 999.99m,
            OriginalPrice = 1299.99m,
            IsOnSale = true,
            DiscountPercentage = 23,
            Rating = 4.5m,
            ReviewCount = 42,
            ImageUrl = "/images/laptop.jpg",
            Url = "/products/laptop-pro",
            Brand = "TechBrand",
            Category = "Electronics",
            StockStatus = "In Stock",
            Attributes = new Dictionary<string, string> { { "RAM", "16GB" }, { "Storage", "512GB SSD" } }
        };

        dto.IsOnSale.Should().BeTrue();
        dto.Attributes.Should().HaveCount(2);
    }

    [Fact]
    public void CmsPageDto_ShouldHaveRequiredProperties()
    {
        var dto = new CmsPageDto
        {
            Id = Guid.NewGuid(),
            Title = "About Us",
            Slug = "about-us",
            Content = "<p>Learn about us</p>",
            Excerpt = "About our company",
            FeaturedImage = "/images/about.jpg",
            MetaTitle = "About Us",
            MetaDescription = "Learn about our company",
            IsPublished = true,
            PublishedAt = DateTime.UtcNow,
            Author = "Admin",
            ViewCount = 1500,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Slug.Should().Be("about-us");
    }

    [Fact]
    public void NavigationMenuDto_ShouldHaveRequiredProperties()
    {
        var dto = new NavigationMenuDto
        {
            Id = Guid.NewGuid(),
            Name = "Main Menu",
            Location = "Header",
            IsActive = true,
            Items = new List<NavigationMenuItemDto>
            {
                new() { Id = Guid.NewGuid(), Label = "Home", Url = "/", SortOrder = 0 },
                new() { Id = Guid.NewGuid(), Label = "Shop", Url = "/products", SortOrder = 1 }
            }
        };

        dto.Items.Should().HaveCount(2);
    }

    [Fact]
    public void SiteSettingDto_ShouldHaveRequiredProperties()
    {
        var dto = new SiteSettingDto
        {
            Key = "SiteName",
            Value = "ECommerce Store",
            Description = "The site name",
            Group = "General",
            DataType = "String",
            IsEncrypted = false
        };

        dto.Key.Should().Be("SiteName");
    }

    [Fact]
    public void MediaFileDto_ShouldHaveRequiredProperties()
    {
        var dto = new MediaFileDto
        {
            Id = Guid.NewGuid(),
            FileName = "product-image.jpg",
            OriginalFileName = "photo.jpg",
            Url = "/uploads/product-image.jpg",
            ThumbnailUrl = "/uploads/thumbs/product-image.jpg",
            FileSize = 1048576,
            ContentType = "image/jpeg",
            Alt = "Product image",
            Title = "Product Image",
            UploadedAt = DateTime.UtcNow,
            UploadedBy = "admin@example.com"
        };

        dto.FileSize.Should().Be(1048576);
    }
}
