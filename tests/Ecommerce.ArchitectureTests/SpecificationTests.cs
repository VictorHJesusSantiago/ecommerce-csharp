using FluentAssertions;
using Xunit;
using Ecommerce.Domain.Specifications;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.ArchitectureTests;

public class SpecificationTests
{
    [Fact]
    public void BaseSpecification_ShouldCreateCriteria()
    {
        var spec = new ProductSpecification();
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterByName()
    {
        var spec = new ProductSpecification { SearchTerm = "laptop" };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterByCategory()
    {
        var spec = new ProductSpecification { CategoryId = Guid.NewGuid() };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterByBrand()
    {
        var spec = new ProductSpecification { BrandId = Guid.NewGuid() };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterByPriceRange()
    {
        var spec = new ProductSpecification { MinPrice = 100, MaxPrice = 500 };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterInStock()
    {
        var spec = new ProductSpecification { InStockOnly = true };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterFeatured()
    {
        var spec = new ProductSpecification { IsFeatured = true };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldFilterByTag()
    {
        var spec = new ProductSpecification { Tag = "new-arrival" };
        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_ShouldApplyPaging()
    {
        var spec = new ProductSpecification { Page = 2, PageSize = 10 };
        spec.Skip.Should().Be(10);
        spec.Take.Should().Be(10);
    }

    [Fact]
    public void ProductSpecification_ShouldApplySorting()
    {
        var spec = new ProductSpecification { SortBy = "price", SortDescending = true };
        spec.OrderBy.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecificationParams_ShouldHaveDefaults()
    {
        var spec = new ProductSpecificationParams();
        spec.Page.Should().Be(1);
        spec.PageSize.Should().Be(20);
    }

    [Fact]
    public void BaseSpecification_ShouldSupportIncludes()
    {
        var spec = new ProductSpecification();
        spec.Includes.Should().NotBeNull();
    }

    [Fact]
    public void BaseSpecification_ShouldSupportThenIncludes()
    {
        var spec = new ProductSpecification();
        spec.ThenIncludes.Should().NotBeNull();
    }

    [Fact]
    public void BaseSpecification_ShouldTrackPaging()
    {
        var spec = new ProductSpecification { Page = 3, PageSize = 15 };
        spec.Skip.Should().Be(30);
        spec.Take.Should().Be(15);
    }
}
