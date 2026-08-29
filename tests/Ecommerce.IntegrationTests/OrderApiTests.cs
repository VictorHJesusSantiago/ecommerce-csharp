using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class OrderApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrders_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/orders");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        var id = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v1/orders/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PlaceOrder_ReturnsCreatedStatusCode()
    {
        var request = new
        {
            ShippingAddressId = Guid.NewGuid(),
            BillingAddressId = Guid.NewGuid(),
            Items = new[]
            {
                new { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/orders", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task CancelOrder_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        var id = Guid.NewGuid();
        var response = await _client.PutAsync($"/api/v1/orders/{id}/cancel", null);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
