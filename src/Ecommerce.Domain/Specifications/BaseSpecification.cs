using System.Linq.Expressions;

namespace Ecommerce.Domain.Specifications;

public class BaseSpecification<T> where T : class
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; }
    public Expression<Func<T, object>>? OrderByDescending { get; }
    public Expression<Func<T, object>>? ThenBy { get; }
    public Expression<Func<T, object>>? ThenByDescending { get; }
    public int? Take { get; }
    public int? Skip { get; }
    public bool IsPagingEnabled { get; }
    public bool AsNoTracking { get; }
    public bool AsSplitQuery { get; }
    public List<string> IncludeStrings { get; } = [];
    public Expression<Func<T, bool>>? Filter { get; }
    public List<Expression<Func<T, bool>>> Filters { get; } = [];

    public BaseSpecification() { }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyFilter(Expression<Func<T, bool>> filter)
    {
        Filter = filter;
    }

    protected void ApplyNoTracking()
    {
        AsNoTracking = true;
    }

    protected void ApplySplitQuery()
    {
        AsSplitQuery = true;
    }
}

public class ProductSpecification : BaseSpecification<Ecommerce.Domain.Entities.Catalog.Product>
{
    public ProductSpecification(ProductSpecificationParams @params)
    {
        if (!string.IsNullOrEmpty(@params.SearchQuery))
        {
            Filter = p => p.Name.Contains(@params.SearchQuery) ||
                          (p.Description != null && p.Description.Contains(@params.SearchQuery));
        }

        if (@params.CategoryId.HasValue)
        {
            Filter = p => p.CategoryId == @params.CategoryId;
        }

        if (@params.BrandId.HasValue)
        {
            Filter = p => p.BrandId == @params.BrandId;
        }

        if (@params.MinPrice.HasValue)
        {
            Filter = p => p.Price >= @params.MinPrice;
        }

        if (@params.MaxPrice.HasValue)
        {
            Filter = p => p.Price <= @params.MaxPrice;
        }

        if (@params.InStockOnly)
        {
            Filter = p => p.StockQuantity > 0;
        }

        Filter = p => p.IsActive;

        switch (@params.SortBy?.ToLower())
        {
            case "price_asc":
                ApplyOrderBy(p => p.Price);
                break;
            case "price_desc":
                ApplyOrderByDescending(p => p.Price);
                break;
            case "newest":
                ApplyOrderByDescending(p => p.CreatedAt);
                break;
            case "name":
                ApplyOrderBy(p => p.Name);
                break;
            default:
                ApplyOrderByDescending(p => p.AverageRating);
                break;
        }

        ApplyPaging((@params.Page - 1) * @params.PageSize, @params.PageSize);
        ApplyNoTracking();
    }
}

public class ProductSpecificationParams
{
    public string? SearchQuery { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public bool InStockOnly { get; set; }
}

public class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _oldParam;
    private readonly ParameterExpression _newParam;

    public ParameterReplacer(ParameterExpression oldParam, ParameterExpression newParam)
    {
        _oldParam = oldParam;
        _newParam = newParam;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _oldParam ? _newParam : base.VisitParameter(node);
    }
}
