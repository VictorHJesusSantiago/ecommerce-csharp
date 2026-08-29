using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class ProductApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/products");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/v1/products");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
    {
        var id = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v1/products/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedStatusCode()
    {
        var request = new
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 49.99m,
            Sku = "TEST-001",
            StockQuantity = 100,
            CategoryId = Guid.NewGuid()
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/products", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task SearchProducts_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/v1/products/search?q=headphones");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetFeaturedProducts_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/v1/products/featured?count=10");

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
