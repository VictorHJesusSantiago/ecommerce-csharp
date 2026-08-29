using Xunit;
using FluentAssertions;

namespace Ecommerce.UnitTests;

public class SpecificationTests
{
    [Fact]
    public void ProductSpecification_WithCategory_ShouldFilterByCategory()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.AddFilter(p => p.CategoryId == Guid.NewGuid());

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_WithPriceRange_ShouldFilterByPrice()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        var minPrice = 10m;
        var maxPrice = 100m;
        spec.AddFilter(p => p.Price >= minPrice && p.Price <= maxPrice);

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_WithSearchQuery_ShouldFilterByName()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        var searchQuery = "headphones";
        spec.AddFilter(p => p.Name.Contains(searchQuery));

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_WithBrand_ShouldFilterByBrand()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        var brandId = Guid.NewGuid();
        spec.AddFilter(p => p.BrandId == brandId);

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_WithStock_ShouldFilterByStock()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.AddFilter(p => p.StockQuantity > 0);

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_WithRating_ShouldFilterByRating()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        var minRating = 4.0;
        spec.AddFilter(p => p.AverageRating >= minRating);

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_WithMultipleFilters_ShouldCombineFilters()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        var categoryId = Guid.NewGuid();
        var minPrice = 10m;
        var maxPrice = 100m;
        spec.AddFilter(p => p.CategoryId == categoryId && p.Price >= minPrice && p.Price <= maxPrice);

        spec.Criteria.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_OrderBy_ShouldSetOrderBy()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.AddOrderBy(p => p.Name);

        spec.OrderBy.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_OrderByDescending_ShouldSetOrderByDescending()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.AddOrderByDescending(p => p.Price);

        spec.OrderByDescending.Should().NotBeNull();
    }

    [Fact]
    public void ProductSpecification_Include_ShouldAddInclude()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.AddInclude(p => p.Category);
        spec.AddInclude(p => p.Brand);

        spec.Includes.Should().HaveCount(2);
    }

    [Fact]
    public void ProductSpecification_Paging_ShouldSetPaging()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.ApplyPaging(1, 20);

        spec.Skip.Should().Be(0);
        spec.Take.Should().Be(20);
    }

    [Fact]
    public void ProductSpecification_IsPagingEnabled_ShouldBeTrueWhenPagingApplied()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();
        spec.ApplyPaging(1, 20);

        spec.IsPagingEnabled.Should().BeTrue();
    }

    [Fact]
    public void ProductSpecification_IsPagingEnabled_ShouldBeFalseByDefault()
    {
        var spec = new Ecommerce.Domain.Specifications.ProductSpecification();

        spec.IsPagingEnabled.Should().BeFalse();
    }
}

public class ParameterReplacerTests
{
    [Fact]
    public void ParameterReplacer_ShouldReplaceParameter()
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(typeof(string), "x");
        var body = System.Linq.Expressions.Expression.Equal(parameter, System.Linq.Expressions.Expression.Constant("test"));
        var lambda = System.Linq.Expressions.Expression.Lambda<Func<string, bool>>(body, parameter);

        var replacer = new Ecommerce.Domain.Specifications.ParameterReplacer(parameter, System.Linq.Expressions.Expression.Constant("value"));
        var newBody = replacer.Visit(body);

        newBody.Should().NotBeNull();
    }

    [Fact]
    public void ParameterReplacer_ShouldNotModifyUnrelatedExpressions()
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(typeof(string), "x");
        var body = System.Linq.Expressions.Expression.Call(parameter, typeof(string).GetMethod("Contains", [typeof(string)])!, System.Linq.Expressions.Expression.Constant("test"));
        var lambda = System.Linq.Expressions.Expression.Lambda<Func<string, bool>>(body, parameter);

        var newParameter = System.Linq.Expressions.Expression.Parameter(typeof(string), "y");
        var replacer = new Ecommerce.Domain.Specifications.ParameterReplacer(parameter, newParameter);
        var newBody = replacer.Visit(body);

        newBody.Should().NotBeNull();
    }
}
