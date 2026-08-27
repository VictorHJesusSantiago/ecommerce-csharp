namespace Ecommerce.Infrastructure.Services;

public class ProductSearchService
{
    private readonly ICacheService _cacheService;

    public ProductSearchService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<List<ProductSearchResult>> SearchProductsAsync(string query, int page = 1, int pageSize = 20)
    {
        var cacheKey = $"search:{query}:p{page}:s{pageSize}";
        var cached = await _cacheService.GetAsync<List<ProductSearchResult>>(cacheKey);
        if (cached != null) return cached;

        var results = new List<ProductSearchResult>();
        await _cacheService.SetAsync(cacheKey, results, TimeSpan.FromMinutes(10));
        return results;
    }

    public async Task<List<string>> GetSuggestionsAsync(string query, int maxSuggestions = 10)
    {
        var cacheKey = $"suggestions:{query}";
        var cached = await _cacheService.GetAsync<List<string>>(cacheKey);
        if (cached != null) return cached;

        var suggestions = new List<string>();
        await _cacheService.SetAsync(cacheKey, suggestions, TimeSpan.FromMinutes(30));
        return suggestions;
    }

    public async Task<List<string>> GetTrendingSearchesAsync(int count = 10)
    {
        var cacheKey = $"trending:searches:{count}";
        var cached = await _cacheService.GetAsync<List<string>>(cacheKey);
        if (cached != null) return cached;

        return [];
    }

    public async Task IndexProductAsync(ProductSearchDocument document)
    {
        await Task.CompletedTask;
    }

    public async Task RemoveProductAsync(Guid productId)
    {
        await _cacheService.RemoveAsync($"product:search:{productId}");
    }

    public async Task UpdateProductIndexAsync(ProductSearchDocument document)
    {
        await IndexProductAsync(document);
    }

    public async Task ReindexAllProductsAsync()
    {
        await Task.CompletedTask;
    }
}

public class ProductSearchDocument
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public int StockQuantity { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int TotalSales { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ProductSearchResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string? MainImageUrl { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool InStock { get; set; }
    public double Score { get; set; }
    public string HighlightedName { get; set; } = string.Empty;
    public string? HighlightedDescription { get; set; }
}
