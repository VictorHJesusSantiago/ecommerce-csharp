using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class ReviewApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ReviewApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetReviewsByProduct_ReturnsSuccessStatusCode()
    {
        var productId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v1/products/{productId}/reviews");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task CreateReview_ReturnsCreatedStatusCode()
    {
        var request = new
        {
            ProductId = Guid.NewGuid(),
            Rating = 5,
            Title = "Great product!",
            Comment = "I love this product. Highly recommended!",
            OrderId = Guid.NewGuid()
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/reviews", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetReviewStats_ReturnsSuccessStatusCode()
    {
        var productId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v1/products/{productId}/reviews/stats");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task VoteReview_ReturnsOkResult()
    {
        var reviewId = Guid.NewGuid();
        var request = new { ReviewId = reviewId, IsHelpful = true };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync($"/api/v1/reviews/{reviewId}/vote", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
