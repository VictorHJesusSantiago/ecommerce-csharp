namespace Ecommerce.Application.DTOs.Search;

public class SearchResultDto
{
    public string Query { get; set; } = string.Empty;
    public int TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalResults / (double)PageSize);
    public double ElapsedMilliseconds { get; set; }
    public List<SearchResultItemDto> Items { get; set; } = [];
    public SearchFiltersDto Filters { get; set; } = new();
    public List<SearchSuggestionDto> Suggestions { get; set; } = [];
    public List<string> RelatedSearches { get; set; } = [];
    public List<TrendingSearchDto> TrendingSearches { get; set; } = [];
}

public class SearchResultItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public string? PrimaryImageUrl { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public string? CategoryName { get; set; }
    public Guid? CategoryId { get; set; }
    public string? BrandName { get; set; }
    public Guid? BrandId { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int StockQuantity { get; set; }
    public bool IsInStock { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsOnSale { get; set; }
    public decimal RelevanceScore { get; set; }
    public string HighlightedName { get; set; } = string.Empty;
    public string? HighlightedDescription { get; set; }
    public List<string> MatchedAttributes { get; set; } = [];
}

public class SearchFiltersDto
{
    public List<CategoryFilterDto> Categories { get; set; } = [];
    public List<BrandFilterDto> Brands { get; set; } = [];
    public PriceRangeFilterDto PriceRange { get; set; } = new();
    public List<RatingFilterDto> Ratings { get; set; } = [];
    public List<AvailabilityFilterDto> AvailabilityFilters { get; set; } = [];
    public List<AttributeFilterDto> AttributeFilters { get; set; } = [];
}

public class CategoryFilterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    public bool IsSelected { get; set; }
    public List<CategoryFilterDto> SubCategories { get; set; } = [];
}

public class BrandFilterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    public bool IsSelected { get; set; }
}

public class PriceRangeFilterDto
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal? SelectedMinPrice { get; set; }
    public decimal? SelectedMaxPrice { get; set; }
    public List<PriceBucketDto> Buckets { get; set; } = [];
}

public class PriceBucketDto
{
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public int Count { get; set; }
    public string Label { get; set; } = string.Empty;
}

public class RatingFilterDto
{
    public int Rating { get; set; }
    public int Count { get; set; }
    public bool IsSelected { get; set; }
}

public class AvailabilityFilterDto
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int Count { get; set; }
    public bool IsSelected { get; set; }
}

public class AttributeFilterDto
{
    public string AttributeName { get; set; } = string.Empty;
    public string AttributeId { get; set; } = string.Empty;
    public List<AttributeValueDto> Values { get; set; } = [];
}

public class AttributeValueDto
{
    public string Value { get; set; } = string.Empty;
    public int Count { get; set; }
    public bool IsSelected { get; set; }
}

public class TrendingSearchDto
{
    public string Query { get; set; } = string.Empty;
    public int SearchCount { get; set; }
    public decimal? AverageResultCount { get; set; }
    public int TrendScore { get; set; }
    public DateTime? PeakDate { get; set; }
    public List<string> RelatedProducts { get; set; } = [];
}

public class SearchSuggestionDto
{
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Popularity { get; set; }
}

public class SearchAutocompleteDto
{
    public string Query { get; set; } = string.Empty;
    public List<SearchSuggestionDto> Suggestions { get; set; } = [];
    public List<TrendingSearchDto> Trending { get; set; } = [];
    public List<string> RecentSearches { get; set; } = [];
    public double ElapsedMilliseconds { get; set; }
}

public class SearchHistoryDto
{
    public Guid Id { get; set; }
    public string Query { get; set; } = string.Empty;
    public int ResultCount { get; set; }
    public Guid? UserId { get; set; }
    public string? SessionId { get; set; }
    public DateTime SearchedAt { get; set; }
}

public class SearchAnalyticsDto
{
    public int TotalSearches { get; set; }
    public int UniqueQueries { get; set; }
    public double AverageResultsPerQuery { get; set; }
    public double ZeroResultRate { get; set; }
    public List<TopSearchQueryDto> TopQueries { get; set; } = [];
    public List<ZeroResultQueryDto> ZeroResultQueries { get; set; } = [];
    public List<SearchTrendDto> DailyTrends { get; set; } = [];
}

public class TopSearchQueryDto
{
    public string Query { get; set; } = string.Empty;
    public int Count { get; set; }
    public double AverageResultCount { get; set; }
    public double ClickThroughRate { get; set; }
    public double ConversionRate { get; set; }
    public decimal Revenue { get; set; }
}

public class ZeroResultQueryDto
{
    public string Query { get; set; } = string.Empty;
    public int Count { get; set; }
    public List<string> Suggestions { get; set; } = [];
    public bool IsAutoCorrected { get; set; }
    public string? CorrectedQuery { get; set; }
}

public class SearchTrendDto
{
    public DateTime Date { get; set; }
    public int SearchCount { get; set; }
    public int UniqueQueryCount { get; set; }
    public double AverageResultCount { get; set; }
    public double ZeroResultRate { get; set; }
    public decimal SearchRevenue { get; set; }
}
