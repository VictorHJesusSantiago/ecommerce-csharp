using Xunit;
using FluentAssertions;

namespace Ecommerce.IntegrationTests;

public class NotificationApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public NotificationApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUserNotifications_ReturnsSuccessStatusCode()
    {
        var userId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/v1/notifications/user/{userId}");

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task SendNotification_ReturnsOkResult()
    {
        var request = new
        {
            RecipientId = Guid.NewGuid(),
            Title = "Test Notification",
            Message = "This is a test notification",
            Type = "Info"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/notifications/send", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task MarkAsRead_ReturnsOkResult()
    {
        var notificationId = Guid.NewGuid();
        var response = await _client.PutAsync($"/api/v1/notifications/{notificationId}/read", null);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task MarkAllAsRead_ReturnsOkResult()
    {
        var userId = Guid.NewGuid();
        var response = await _client.PutAsync($"/api/v1/notifications/user/{userId}/read-all", null);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task SendEmail_ReturnsOkResult()
    {
        var request = new
        {
            To = "test@example.com",
            Subject = "Test Email",
            Body = "<h1>Test</h1>",
            IsHtml = true
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("/api/v1/notifications/email", content);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
