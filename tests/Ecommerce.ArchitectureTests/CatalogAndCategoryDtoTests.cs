using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Common;

namespace Ecommerce.ArchitectureTests;

public class CatalogAndCategoryDtoTests
{
    [Fact]
    public void CategoryDto_ShouldHaveRequiredProperties()
    {
        var dto = new CategoryDto
        {
            Id = Guid.NewGuid(),
            Name = "Electronics",
            Slug = "electronics",
            Description = "Electronic devices and accessories",
            ImageUrl = "/images/electronics.jpg",
            IconClass = "fas fa-laptop",
            ParentId = null,
            ParentName = null,
            IsActive = true,
            SortOrder = 0,
            ProductCount = 150,
            SubCategories = new List<CategoryDto>
            {
                new() { Name = "Phones", Slug = "phones", ProductCount = 50 }
            },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.SubCategories.Should().HaveCount(1);
        dto.ProductCount.Should().Be(150);
    }

    [Fact]
    public void CreateCategoryDto_ShouldHaveRequiredProperties()
    {
        var dto = new CreateCategoryDto
        {
            Name = "Books",
            Description = "All kinds of books",
            ParentId = null,
            ImageUrl = "/images/books.jpg",
            SortOrder = 5,
            IsActive = true
        };

        dto.Name.Should().Be("Books");
        dto.SortOrder.Should().Be(5);
    }

    [Fact]
    public void BrandDto_ShouldHaveRequiredProperties()
    {
        var dto = new BrandDto
        {
            Id = Guid.NewGuid(),
            Name = "Apple",
            Slug = "apple",
            Description = "Apple Inc.",
            LogoUrl = "/images/apple-logo.png",
            Website = "https://apple.com",
            IsActive = true,
            ProductCount = 45,
            AverageRating = 4.7m,
            CreatedAt = DateTime.UtcNow
        };

        dto.Name.Should().Be("Apple");
        dto.AverageRating.Should().Be(4.7m);
    }

    [Fact]
    public void ProductCollectionDto_ShouldHaveRequiredProperties()
    {
        var dto = new ProductCollectionDto
        {
            Id = Guid.NewGuid(),
            Name = "Summer Collection",
            Slug = "summer-collection",
            Description = "Our summer 2024 collection",
            ImageUrl = "/images/summer.jpg",
            IsActive = true,
            ProductIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
            SortOrder = 1,
            CreatedAt = DateTime.UtcNow
        };

        dto.ProductIds.Should().HaveCount(2);
    }

    [Fact]
    public void ProductListDto_ShouldTrackPagination()
    {
        var dto = new ProductListDto
        {
            Products = new List<ProductDto>(),
            TotalCount = 100,
            CurrentPage = 3,
            PageSize = 20,
            TotalPages = 5,
            SearchTerm = "laptop",
            CategoryFilter = "Electronics",
            BrandFilter = "Dell",
            MinPrice = 500,
            MaxPrice = 2000,
            SortBy = "price",
            SortOrder = "asc",
            InStockOnly = true
        };

        dto.TotalPages.Should().Be(5);
        dto.SearchTerm.Should().Be("laptop");
    }

    [Fact]
    public void CategoryBreadcrumbDto_ShouldHaveRequiredProperties()
    {
        var dto = new CategoryBreadcrumbDto
        {
            Id = Guid.NewGuid(),
            Name = "Phones",
            Slug = "phones",
            ParentId = null,
            Parent = null,
            Level = 0
        };

        dto.Level.Should().Be(0);
    }
}
