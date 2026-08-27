using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Domain.Specifications;

public abstract class Specification<T> where T : class
{
    public abstract Expression<Func<T, bool>> ToExpression();
    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }
}

public class ProductSpecification : Specification<Product>
{
    private readonly List<Expression<Func<Product, bool>>> _criteria = [];

    public ProductSpecification ByCategory(Guid categoryId)
    {
        _criteria.Add(p => p.CategoryId == categoryId);
        return this;
    }

    public ProductSpecification ByBrand(Guid brandId)
    {
        _criteria.Add(p => p.BrandId == brandId);
        return this;
    }

    public ProductSpecification ByStatus(ProductStatus status)
    {
        _criteria.Add(p => p.Status == status);
        return this;
    }

    public ProductSpecification PriceRange(decimal? min, decimal? max)
    {
        if (min.HasValue) _criteria.Add(p => p.Price >= min.Value);
        if (max.HasValue) _criteria.Add(p => p.Price <= max.Value);
        return this;
    }

    public ProductSpecification InStock()
    {
        _criteria.Add(p => p.Status == ProductStatus.Active);
        return this;
    }

    public ProductSpecification Featured()
    {
        _criteria.Add(p => p.IsFeatured && p.Status == ProductStatus.Active);
        return this;
    }

    public ProductSpecification NewArrivals()
    {
        _criteria.Add(p => p.IsNewArrival && p.Status == ProductStatus.Active);
        return this;
    }

    public ProductSpecification BestSellers()
    {
        _criteria.Add(p => p.IsBestSeller && p.Status == ProductStatus.Active);
        return this;
    }

    public ProductSpecification Search(string term)
    {
        if (!string.IsNullOrWhiteSpace(term))
        {
            _criteria.Add(p => p.Name.Contains(term) ||
                              (p.Description != null && p.Description.Contains(term)) ||
                              (p.Tags != null && p.Tags.Contains(term)));
        }
        return this;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (_criteria.Count == 0)
            return p => true;

        var parameter = Expression.Parameter(typeof(Product));
        Expression? body = null;

        foreach (var criterion in _criteria)
        {
            var replaced = new ParameterReplacer(parameter).Visit(criterion.Body);
            body = body is null ? replaced : Expression.AndAlso(body, replaced);
        }

        return Expression.Lambda<Func<Product, bool>>(body!, parameter);
    }
}

internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _newParameter;

    public ParameterReplacer(ParameterExpression newParameter)
    {
        _newParameter = newParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return _newParameter;
    }
}

public abstract class BaseSpecification<T> where T : class
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public List<string> IncludeStrings { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; }
    public Expression<Func<T, object>>? OrderByDescending { get; }
    public Expression<Func<T, object>>? ThenBy { get; }
    public Expression<Func<T, object>>? ThenByDescending { get; }
    public int? Take { get; }
    public int? Skip { get; }
    public bool IsPagingEnabled { get; }
    public bool IsDistinct { get; }

    protected BaseSpecification(Expression<Func<T, bool>>? criteria)
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

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyThenBy(Expression<Func<T, object>> thenByExpression)
    {
        ThenBy = thenByExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    public virtual IQueryable<T> ApplySpecification(IQueryable<T> query)
    {
        if (Criteria != null)
            query = query.Where(Criteria);

        foreach (var include in Includes)
            query = query.Include(include);

        foreach (var includeString in IncludeStrings)
            query = query.Include(includeString);

        if (OrderBy != null)
        {
            var ordered = query.OrderBy(OrderBy);
            query = ThenBy != null ? ordered.ThenBy(ThenBy) : ordered;
        }
        else if (OrderByDescending != null)
        {
            var ordered = query.OrderByDescending(OrderByDescending);
            query = ThenByDescending != null ? ordered.ThenByDescending(ThenByDescending) : ordered;
        }

        if (IsPagingEnabled && Skip.HasValue && Take.HasValue)
            query = query.Skip(Skip.Value).Take(Take.Value);

        return IsDistinct ? query.Distinct() : query;
    }
}
