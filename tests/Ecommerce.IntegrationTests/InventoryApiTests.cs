using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class InventoryApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public InventoryApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWarehouses_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/inventory/warehouses");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetWarehouseById_ReturnsNotFound_WhenWarehouseDoesNotExist()
    {
        var id = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v1/inventory/warehouses/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task AdjustStock_ReturnsOkResult()
    {
        var request = new
        {
            ProductId = Guid.NewGuid(),
            WarehouseId = Guid.NewGuid(),
            Quantity = 10,
            Reason = "Restock"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/inventory/adjust", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task TransferStock_ReturnsOkResult()
    {
        var request = new
        {
            ProductId = Guid.NewGuid(),
            SourceWarehouseId = Guid.NewGuid(),
            DestinationWarehouseId = Guid.NewGuid(),
            Quantity = 5
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/inventory/transfer", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetInventoryReport_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/inventory/report");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetSuppliers_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/inventory/suppliers");

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
