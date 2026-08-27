using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Search;
using Ecommerce.Application.Wrappers;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Common;
using Ecommerce.Application.DTOs.Catalog;
using System.Linq.Expressions;

namespace Ecommerce.Application.Services;

public partial class ProductService : IProductService
{
    private readonly IRepository<Ecommerce.Domain.Entities.Catalog.Product> _productRepo;
    private readonly ICacheService _cache;
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IRepository<Ecommerce.Domain.Entities.Catalog.Product> productRepo,
        ICacheService cache,
        IEventBus eventBus,
        ILogger<ProductService> logger)
    {
        _productRepo = productRepo;
        _cache = cache;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<ApiResponse<ProductDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var product = await _productRepo.GetByIdAsync(id, ct);
        if (product is null)
            return ApiResponse<ProductDto>.FailResponse("Product not found", 404);

        return ApiResponse<ProductDto>.SuccessResponse(MapToDto(product));
    }

    public async Task<ApiResponse<ProductDto>> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        var cacheKey = $"product_slug_{slug}";
        var cached = await _cache.GetAsync<ProductDto>(cacheKey, ct);
        if (cached is not null)
            return ApiResponse<ProductDto>.SuccessResponse(cached);

        var product = await _productRepo.GetBySlugAsync(slug, ct);
        if (product is null)
            return ApiResponse<ProductDto>.FailResponse("Product not found", 404);

        var dto = MapToDto(product);
        await _cache.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(30), ct);
        return ApiResponse<ProductDto>.SuccessResponse(dto);
    }

    public async Task<ApiResponse<PagedResponse<ProductListDto>>> SearchProductsAsync(ProductSearchRequest request, CancellationToken ct = default)
    {
        var cacheKey = $"products_search_{request.GetHashCode()}";
        var cached = await _cache.GetAsync<PagedResponse<ProductListDto>>(cacheKey, ct);
        if (cached is not null)
            return ApiResponse<PagedResponse<ProductListDto>>.SuccessResponse(cached);

        Expression<Func<Ecommerce.Domain.Entities.Catalog.Product, bool>> predicate = p =>
            p.IsActive &&
            (!request.CategoryId.HasValue || p.CategoryId == request.CategoryId) &&
            (!request.BrandId.HasValue || p.BrandId == request.BrandId) &&
            (!request.MinPrice.HasValue || p.Price >= request.MinPrice) &&
            (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice) &&
            (string.IsNullOrEmpty(request.SearchQuery) || p.Name.Contains(request.SearchQuery) || (p.Description != null && p.Description.Contains(request.SearchQuery)));

        var allProducts = await _productRepo.FindAsync(predicate, ct);
        var sortedProducts = request.SortBy switch
        {
            "price_asc" => allProducts.OrderBy(p => p.Price),
            "price_desc" => allProducts.OrderByDescending(p => p.Price),
            "newest" => allProducts.OrderByDescending(p => p.CreatedAt),
            "name" => allProducts.OrderBy(p => p.Name),
            _ => allProducts.OrderByDescending(p => p.StockQuantity > 0).ThenByDescending(p => p.AverageRating)
        };

        var totalCount = sortedProducts.Count();
        var pagedProducts = sortedProducts
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(MapToListDto)
            .ToList();

        var response = new PagedResponse<ProductListDto>
        {
            Data = pagedProducts,
            PageNumber = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            TotalRecords = totalCount
        };

        await _cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5), ct);
        return ApiResponse<PagedResponse<ProductListDto>>.SuccessResponse(response);
    }

    public async Task<ApiResponse<ProductDto>> CreateProductAsync(CreateProductRequest request, CancellationToken ct = default)
    {
        var product = new Ecommerce.Domain.Entities.Catalog.Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Sku = request.Sku,
            Slug = GenerateSlug(request.Name),
            CategoryId = request.CategoryId,
            BrandId = request.BrandId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _productRepo.AddAsync(product, ct);
        _logger.LogInformation("Created product: {ProductName} ({ProductId})", product.Name, product.Id);

        return ApiResponse<ProductDto>.SuccessResponse(MapToDto(product));
    }

    public async Task<ApiResponse<ProductDto>> UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken ct = default)
    {
        var product = await _productRepo.GetByIdAsync(id, ct);
        if (product is null)
            return ApiResponse<ProductDto>.FailResponse("Product not found", 404);

        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        product.Price = request.Price ?? product.Price;
        product.StockQuantity = request.StockQuantity ?? product.StockQuantity;

        await _productRepo.UpdateAsync(product, ct);
        await _cache.RemoveByPatternAsync("products_search_", ct);
        _logger.LogInformation("Updated product: {ProductId}", id);

        return ApiResponse<ProductDto>.SuccessResponse(MapToDto(product));
    }

    public async Task<ApiResponse> DeleteProductAsync(Guid id, CancellationToken ct = default)
    {
        var product = await _productRepo.GetByIdAsync(id, ct);
        if (product is null)
            return ApiResponse.FailResponse("Product not found", 404);

        product.IsActive = false;
        await _productRepo.UpdateAsync(product, ct);
        await _cache.RemoveByPatternAsync("products_search_", ct);
        _logger.LogInformation("Soft deleted product: {ProductId}", id);

        return ApiResponse.SuccessResponse("Product deleted successfully.");
    }

    public async Task<ApiResponse> PublishProductAsync(Guid id, CancellationToken ct = default)
    {
        var product = await _productRepo.GetByIdAsync(id, ct);
        if (product is null)
            return ApiResponse.FailResponse("Product not found", 404);

        product.IsActive = true;
        await _productRepo.UpdateAsync(product, ct);
        _logger.LogInformation("Published product: {ProductId}", id);

        return ApiResponse.SuccessResponse("Product published successfully.");
    }

    public async Task<ApiResponse<List<ProductDto>>> GetFeaturedProductsAsync(int count = 10, CancellationToken ct = default)
    {
        var cacheKey = $"featured_products_{count}";
        var cached = await _cache.GetAsync<List<ProductDto>>(cacheKey, ct);
        if (cached is not null)
            return ApiResponse<List<ProductDto>>.SuccessResponse(cached);

        var products = await _productRepo.FindAsync(p => p.IsActive && p.IsFeatured, ct);
        var result = products.Take(count).Select(MapToDto).ToList();

        await _cache.SetAsync(cacheKey, result, TimeSpan.FromHours(1), ct);
        return ApiResponse<List<ProductDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<ProductDto>>> GetNewArrivalsAsync(int count = 10, CancellationToken ct = default)
    {
        var products = await _productRepo.FindAsync(p => p.IsActive, ct);
        var result = products.OrderByDescending(p => p.CreatedAt).Take(count).Select(MapToDto).ToList();
        return ApiResponse<List<ProductDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<List<ProductDto>>> GetBestSellersAsync(int count = 10, CancellationToken ct = default)
    {
        var products = await _productRepo.FindAsync(p => p.IsActive, ct);
        var result = products.OrderByDescending(p => p.ReviewCount).ThenByDescending(p => p.AverageRating).Take(count).Select(MapToDto).ToList();
        return ApiResponse<List<ProductDto>>.SuccessResponse(result);
    }

    public async Task<ApiResponse<PagedResponse<ProductListDto>>> GetByCategoryAsync(Guid categoryId, int page, int pageSize, CancellationToken ct = default)
    {
        var products = await _productRepo.FindAsync(p => p.IsActive && p.CategoryId == categoryId, ct);
        var paged = products.Skip((page - 1) * pageSize).Take(pageSize).Select(MapToListDto).ToList();
        var response = new PagedResponse<ProductListDto>
        {
            Data = paged,
            PageNumber = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(products.Count() / (double)pageSize),
            TotalRecords = products.Count()
        };
        return ApiResponse<PagedResponse<ProductListDto>>.SuccessResponse(response);
    }

    public async Task<ApiResponse<PagedResponse<ProductListDto>>> GetByBrandAsync(Guid brandId, int page, int pageSize, CancellationToken ct = default)
    {
        var products = await _productRepo.FindAsync(p => p.IsActive && p.BrandId == brandId, ct);
        var paged = products.Skip((page - 1) * pageSize).Take(pageSize).Select(MapToListDto).ToList();
        var response = new PagedResponse<ProductListDto>
        {
            Data = paged,
            PageNumber = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(products.Count() / (double)pageSize),
            TotalRecords = products.Count()
        };
        return ApiResponse<PagedResponse<ProductListDto>>.SuccessResponse(response);
    }

    private static ProductDto MapToDto(Ecommerce.Domain.Entities.Catalog.Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        Sku = p.Sku,
        Slug = p.Slug,
        StockQuantity = p.StockQuantity,
        AverageRating = p.AverageRating,
        ReviewCount = p.ReviewCount,
        ImageUrl = p.ImageUrl,
        CategoryName = p.Category?.Name
    };

    private static ProductListDto MapToListDto(Ecommerce.Domain.Entities.Catalog.Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        Sku = p.Sku,
        Slug = p.Slug,
        ImageUrl = p.ImageUrl,
        InStock = p.StockQuantity > 0,
        AverageRating = p.AverageRating,
        ReviewCount = p.ReviewCount,
        CategoryName = p.Category?.Name
    };

    private static string GenerateSlug(string name)
    {
        return name.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("'", "")
            .Replace("\"", "")
            .Replace("&", "and")
            .Replace(",", "")
            .Replace(".", "");
    }
}
