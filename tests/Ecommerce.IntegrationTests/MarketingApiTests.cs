using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class MarketingApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public MarketingApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCoupons_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/marketing/coupons");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task CreateCoupon_ReturnsCreatedStatusCode()
    {
        var request = new
        {
            Code = "SAVE20",
            Description = "20% off",
            DiscountType = "Percentage",
            DiscountValue = 20m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1)
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/marketing/coupons", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task ValidateCoupon_ReturnsOkResult()
    {
        var request = new
        {
            Code = "SAVE20",
            OrderAmount = 100m
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/marketing/coupons/validate", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetBanners_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/marketing/banners");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task SubscribeNewsletter_ReturnsOkResult()
    {
        var request = new { Email = "test@example.com" };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/marketing/newsletter/subscribe", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
