using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class ShippingApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ShippingApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CalculateShipping_ReturnsSuccessStatusCode()
    {
        var request = new
        {
            Country = "US",
            PostalCode = "10001",
            Weight = 2.5,
            ItemCount = 1,
            OrderTotal = 99.99m
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v2/shipping/calculate", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetShippingZones_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/shipping/zones");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetCarriers_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/shipping/carriers");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task TrackShipment_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/shipping/track/1Z999AA10123456784");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetShipmentsByOrder_ReturnsSuccessStatusCode()
    {
        var orderId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v2/shipping/orders/{orderId}/shipments");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetShippingAnalytics_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v2/shipping/analytics");

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
