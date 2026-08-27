using MediatR;

namespace Ecommerce.Application.DTOs.Product;

public class CreateProductCommand : IRequest<ApiResponse<ProductDto>>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Sku { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid? BrandId { get; set; }
}

public class UpdateProductCommand : IRequest<ApiResponse<ProductDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? StockQuantity { get; set; }
}

public class DeleteProductCommand : IRequest<ApiResponse>
{
    public Guid Id { get; set; }
}

public class GetProductByIdQuery : IRequest<ApiResponse<ProductDto>>
{
    public Guid Id { get; set; }
}

public class GetProductBySlugQuery : IRequest<ApiResponse<ProductDto>>
{
    public string Slug { get; set; } = string.Empty;
}

public class SearchProductsQuery : IRequest<ApiResponse<PagedResponse<ProductListDto>>>
{
    public string? SearchQuery { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
}

public class GetFeaturedProductsQuery : IRequest<ApiResponse<List<ProductDto>>>
{
    public int Count { get; set; } = 10;
}

public class GetNewArrivalsQuery : IRequest<ApiResponse<List<ProductDto>>>
{
    public int Count { get; set; } = 10;
}

public class GetBestSellersQuery : IRequest<ApiResponse<List<ProductDto>>>
{
    public int Count { get; set; } = 10;
}
