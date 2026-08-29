using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class PaymentApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PaymentApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessPayment_ReturnsOkResult()
    {
        var request = new
        {
            OrderId = Guid.NewGuid(),
            Amount = 99.99m,
            Currency = "USD",
            PaymentMethod = "CreditCard",
            PaymentMethodId = "pm_card_visa"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v2/payments/process", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetPaymentById_ReturnsNotFound_WhenPaymentDoesNotExist()
    {
        var id = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v2/payments/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ProcessRefund_ReturnsOkResult()
    {
        var request = new
        {
            PaymentId = Guid.NewGuid(),
            Amount = 50.00m,
            Reason = "Customer request"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v2/payments/refund", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetPaymentSettings_ReturnsOkResult()
    {
        var response = await _client.GetAsync("/api/v2/payments/settings");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task CreatePaymentIntent_ReturnsOkResult()
    {
        var request = new
        {
            Amount = 99.99m,
            Currency = "USD",
            PaymentMethod = "CreditCard"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v2/payments/intent", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
