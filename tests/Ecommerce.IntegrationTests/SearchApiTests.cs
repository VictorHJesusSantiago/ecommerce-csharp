using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class SearchApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SearchApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Search_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/search?q=headphones");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Search_WithFilters_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/v2/search?q=headphones&categoryId=1&minPrice=20&maxPrice=100&sortBy=price&sortDescending=true&page=1&pageSize=10");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Autocomplete_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/search/autocomplete?q=hea");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task TrendingSearches_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/search/trending?count=10");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task SearchAnalytics_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/search/analytics");

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
