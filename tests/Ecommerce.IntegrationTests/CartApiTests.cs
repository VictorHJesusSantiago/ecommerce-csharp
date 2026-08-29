using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class CartApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CartApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCart_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/cart");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task AddToCart_ReturnsOkResult()
    {
        var request = new
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/cart/items", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCartItem_ReturnsOkResult()
    {
        var request = new
        {
            CartItemId = Guid.NewGuid(),
            Quantity = 3
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PutAsync("/api/v1/cart/items", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task RemoveCartItem_ReturnsOkResult()
    {
        var id = Guid.NewGuid();
        var response = await _client.DeleteAsync($"/api/v1/cart/items/{id}");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task ClearCart_ReturnsOkResult()
    {
        var response = await _client.DeleteAsync("/api/v1/cart");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task ApplyCoupon_ReturnsOkResult()
    {
        var request = new { CouponCode = "SAVE20" };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/cart/coupon", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
