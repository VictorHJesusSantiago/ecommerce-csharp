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

public class SearchDtoComprehensiveTests
{
    [Fact]
    public void SearchRequestDto_DefaultValues_ShouldBeCorrect()
    {
        var dto = new SearchRequestDto();

        dto.Query.Should().BeNull();
        dto.Page.Should().Be(1);
        dto.PageSize.Should().Be(20);
        dto.SortBy.Should().BeNull();
        dto.SortDescending.Should().BeTrue();
        dto.CategoryId.Should().BeNull();
        dto.BrandId.Should().BeNull();
        dto.MinPrice.Should().BeNull();
        dto.MaxPrice.Should().BeNull();
    }

    [Fact]
    public void SearchRequestDto_WithFilters_ShouldSetFilters()
    {
        var dto = new SearchRequestDto
        {
            Query = "headphones",
            Page = 2,
            PageSize = 10,
            SortBy = "price",
            SortDescending = false,
            CategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            MinPrice = 20m,
            MaxPrice = 100m,
            InStockOnly = true,
            MinRating = 4.0,
            Tags = ["wireless", "bluetooth"]
        };

        dto.Query.Should().Be("headphones");
        dto.Page.Should().Be(2);
        dto.PageSize.Should().Be(10);
        dto.SortBy.Should().Be("price");
        dto.SortDescending.Should().BeFalse();
        dto.Tags.Should().HaveCount(2);
    }

    [Fact]
    public void SearchRequestDto_IsEmpty_ShouldReturnTrueWhenNoQuery()
    {
        var dto = new SearchRequestDto { Query = null };

        dto.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void SearchRequestDto_IsEmpty_ShouldReturnFalseWhenHasQuery()
    {
        var dto = new SearchRequestDto { Query = "headphones" };

        dto.IsEmpty.Should().BeFalse();
    }

    [Fact]
    public void SearchRequestDto_IsEmpty_ShouldReturnTrueWhenEmptyQuery()
    {
        var dto = new SearchRequestDto { Query = "" };

        dto.IsEmpty.Should().BeTrue();
    }
}

public class SearchResultDtoComprehensiveTests
{
    [Fact]
    public void SearchResultDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchResultDto
        {
            Query = "headphones",
            TotalResults = 100,
            TotalPages = 5,
            CurrentPage = 1,
            PageSize = 20,
            ExecutionTimeMs = 50,
            Products =
            [
                new() { Id = Guid.NewGuid(), Name = "Headphones 1", Price = 99.99m, Slug = "headphones-1" },
                new() { Id = Guid.NewGuid(), Name = "Headphones 2", Price = 149.99m, Slug = "headphones-2" }
            ],
            Categories =
            [
                new() { Id = Guid.NewGuid(), Name = "Electronics", ProductCount = 50 },
                new() { Id = Guid.NewGuid(), Name = "Audio", ProductCount = 25 }
            ],
            Brands =
            [
                new() { Id = Guid.NewGuid(), Name = "Sony", ProductCount = 30 },
                new() { Id = Guid.NewGuid(), Name = "Bose", ProductCount = 20 }
            ],
            Suggestions = ["headphones wireless", "headphones bluetooth", "noise cancelling headphones"],
            Facets =
            {
                ["Category"] = ["Electronics", "Audio"],
                ["Brand"] = ["Sony", "Bose"],
                ["Price"] = ["0-50", "50-100", "100-200", "200-500"]
            }
        };

        dto.Query.Should().Be("headphones");
        dto.TotalResults.Should().Be(100);
        dto.TotalPages.Should().Be(5);
        dto.Products.Should().HaveCount(2);
        dto.Categories.Should().HaveCount(2);
        dto.Brands.Should().HaveCount(2);
        dto.Suggestions.Should().HaveCount(3);
        dto.Facets.Should().HaveCount(3);
    }

    [Fact]
    public void SearchResultDto_HasResults_ShouldReturnTrueWhenHasProducts()
    {
        var dto = new SearchResultDto
        {
            Products =
            [
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 99.99m }
            ]
        };

        dto.HasResults.Should().BeTrue();
    }

    [Fact]
    public void SearchResultDto_HasResults_ShouldReturnFalseWhenNoProducts()
    {
        var dto = new SearchResultDto
        {
            Products = []
        };

        dto.HasResults.Should().BeFalse();
    }

    [Fact]
    public void SearchResultDto_HasSuggestions_ShouldReturnTrueWhenHasSuggestions()
    {
        var dto = new SearchResultDto
        {
            Suggestions = ["suggestion 1", "suggestion 2"]
        };

        dto.HasSuggestions.Should().BeTrue();
    }

    [Fact]
    public void SearchResultDto_HasSuggestions_ShouldReturnFalseWhenNoSuggestions()
    {
        var dto = new SearchResultDto
        {
            Suggestions = []
        };

        dto.HasSuggestions.Should().BeFalse();
    }

    [Fact]
    public void SearchResultDto_FromCache_ShouldReturnTrueWhenCached()
    {
        var dto = new SearchResultDto { FromCache = true };

        dto.FromCache.Should().BeTrue();
    }
}

public class SearchProductResultDtoComprehensiveTests
{
    [Fact]
    public void SearchProductResultDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchProductResultDto
        {
            Id = Guid.NewGuid(),
            Name = "Headphones",
            Slug = "headphones",
            Price = 99.99m,
            CompareAtPrice = 149.99m,
            MainImageUrl = "https://example.com/headphones.jpg",
            CategoryName = "Electronics",
            BrandName = "Sony",
            AverageRating = 4.5,
            ReviewCount = 128,
            InStock = true,
            IsFeatured = true,
            IsOnSale = true,
            RelevanceScore = 95.5,
            HighlightedName = "<strong>Headphones</strong>"
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Headphones");
        dto.Price.Should().Be(99.99m);
        dto.RelevanceScore.Should().Be(95.5);
        dto.HighlightedName.Should().Contain("<strong>");
    }

    [Fact]
    public void SearchProductResultDto_DiscountPercentage_ShouldCalculateCorrectly()
    {
        var dto = new SearchProductResultDto
        {
            Price = 99.99m,
            CompareAtPrice = 149.99m
        };

        dto.DiscountPercentage.Should().Be(33.34m);
    }
}

public class SearchCategoryResultDtoComprehensiveTests
{
    [Fact]
    public void SearchCategoryResultDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchCategoryResultDto
        {
            Id = Guid.NewGuid(),
            Name = "Electronics",
            Slug = "electronics",
            ProductCount = 500,
            ImageUrl = "https://example.com/electronics.jpg",
            RelevanceScore = 90.0
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Electronics");
        dto.ProductCount.Should().Be(500);
        dto.RelevanceScore.Should().Be(90.0);
    }
}

public class SearchBrandResultDtoComprehensiveTests
{
    [Fact]
    public void SearchBrandResultDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchBrandResultDto
        {
            Id = Guid.NewGuid(),
            Name = "Sony",
            Slug = "sony",
            ProductCount = 300,
            LogoUrl = "https://example.com/sony-logo.png",
            RelevanceScore = 85.0
        };

        dto.Id.Should().NotBeEmpty();
        dto.Name.Should().Be("Sony");
        dto.ProductCount.Should().Be(300);
        dto.RelevanceScore.Should().Be(85.0);
    }
}

public class SearchSuggestionDtoComprehensiveTests
{
    [Fact]
    public void SearchSuggestionDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchSuggestionDto
        {
            Text = "headphones wireless",
            Type = "Product",
            ProductId = Guid.NewGuid(),
            ProductName = "Wireless Headphones",
            Score = 0.95
        };

        dto.Text.Should().Be("headphones wireless");
        dto.Type.Should().Be("Product");
        dto.ProductId.Should().NotBeNull();
        dto.ProductName.Should().Be("Wireless Headphones");
        dto.Score.Should().Be(0.95);
    }
}

public class SearchFacetDtoComprehensiveTests
{
    [Fact]
    public void SearchFacetDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchFacetDto
        {
            Name = "Category",
            Values =
            [
                new() { Value = "Electronics", Count = 500 },
                new() { Value = "Clothing", Count = 300 },
                new() { Value = "Home", Count = 200 }
            ]
        };

        dto.Name.Should().Be("Category");
        dto.Values.Should().HaveCount(3);
    }
}

public class SearchFacetValueDtoComprehensiveTests
{
    [Fact]
    public void SearchFacetValueDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchFacetValueDto
        {
            Value = "Electronics",
            Count = 500,
            IsSelected = true
        };

        dto.Value.Should().Be("Electronics");
        dto.Count.Should().Be(500);
        dto.IsSelected.Should().BeTrue();
    }
}

public class AdvancedSearchRequestDtoComprehensiveTests
{
    [Fact]
    public void AdvancedSearchRequestDto_AllProperties_ShouldBeSettable()
    {
        var dto = new AdvancedSearchRequestDto
        {
            Query = "wireless headphones",
            CategoryIds = [Guid.NewGuid(), Guid.NewGuid()],
            BrandIds = [Guid.NewGuid()],
            MinPrice = 20m,
            MaxPrice = 200m,
            MinRating = 4.0,
            InStockOnly = true,
            IsOnSale = false,
            IsFeatured = true,
            Tags = ["wireless", "bluetooth"],
            Attributes = new Dictionary<string, string> { ["Color"] = "Black", ["Type"] = "Over-ear" },
            SortBy = "relevance",
            SortDescending = true,
            Page = 1,
            PageSize = 20
        };

        dto.Query.Should().Be("wireless headphones");
        dto.CategoryIds.Should().HaveCount(2);
        dto.BrandIds.Should().HaveCount(1);
        dto.Tags.Should().HaveCount(2);
        dto.Attributes.Should().ContainKey("Color");
    }
}

public class SearchHistoryDtoComprehensiveTests
{
    [Fact]
    public void SearchHistoryDto_AllProperties_ShouldBeSettable()
    {
        var dto = new SearchHistoryDto
        {
            Id = Guid.NewGuid(),
            Query = "headphones",
            ResultsCount = 50,
            SearchedAt = DateTime.UtcNow
        };

        dto.Id.Should().NotBeEmpty();
        dto.Query.Should().Be("headphones");
        dto.ResultsCount.Should().Be(50);
    }
}

public class PopularSearchDtoComprehensiveTests
{
    [Fact]
    public void PopularSearchDto_AllProperties_ShouldBeSettable()
    {
        var dto = new PopularSearchDto
        {
            Query = "wireless headphones",
            SearchCount = 1500,
            Trending = true
        };

        dto.Query.Should().Be("wireless headphones");
        dto.SearchCount.Should().Be(1500);
        dto.Trending.Should().BeTrue();
    }
}
