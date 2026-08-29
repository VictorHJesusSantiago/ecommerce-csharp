using FluentAssertions;
using Xunit;
using Ecommerce.Application.Common;
using Ecommerce.Application.Extensions;

namespace Ecommerce.ArchitectureTests;

public class BehaviorAndFactoryTests
{
    [Fact]
    public void StringExtension_ShouldTrimAndLower_ForSlug()
    {
        "  Hello World  ".ToSlug().Should().Be("hello-world");
    }

    [Fact]
    public void StringExtension_ShouldHandleSpecialChars_ForSlug()
    {
        "Hello & World! @#$%".ToSlug().Should().NotContain("&");
    }

    [Fact]
    public void StringExtension_ShouldTruncateWithEllipsis()
    {
        var result = "Short";
        result.Truncate(100).Should().Be("Short");
    }

    [Fact]
    public void StringExtension_ShouldTruncateLongText()
    {
        var longText = "This is a very long text that should be truncated at some point";
        var result = longText.Truncate(20);
        result.Should().HaveLength(23);
        result.Should().EndWith("...");
    }

    [Fact]
    public void StringExtension_IsValidEmail_ShouldReturnTrue_WhenValid()
    {
        "test@example.com".IsValidEmail().Should().BeTrue();
    }

    [Fact]
    public void StringExtension_IsValidEmail_ShouldReturnFalse_WhenInvalid()
    {
        "not-an-email".IsValidEmail().Should().BeFalse();
    }

    [Fact]
    public void StringExtension_IsValidUrl_ShouldReturnTrue_WhenValid()
    {
        "https://example.com".IsValidUrl().Should().BeTrue();
    }

    [Fact]
    public void DecimalExtension_ToCurrency_ShouldFormat()
    {
        1234.5m.ToCurrency().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void DateTimeExtension_IsRecent_ShouldReturnTrue_WhenRecent()
    {
        DateTime.UtcNow.AddMinutes(-5).IsRecent(TimeSpan.FromMinutes(10)).Should().BeTrue();
    }

    [Fact]
    public void DateTimeExtension_IsRecent_ShouldReturnFalse_WhenOld()
    {
        DateTime.UtcNow.AddHours(-2).IsRecent(TimeSpan.FromMinutes(10)).Should().BeFalse();
    }

    [Fact]
    public void CollectionExtension_ShouldPaginate()
    {
        var items = Enumerable.Range(1, 50).ToList();
        var result = items.ToPagedResult(1, 10);
        result.Should().HaveCount(10);
        result.First().Should().Be(1);
        result.Last().Should().Be(10);
    }

    [Fact]
    public void CollectionExtension_ShouldReturnEmpty_WhenPageExceedsTotal()
    {
        var items = Enumerable.Range(1, 5).ToList();
        var result = items.ToPagedResult(10, 10);
        result.Should().BeEmpty();
    }

    [Fact]
    public void Guard_ShouldThrowOnNegative()
    {
        Action act = () => Guard.GreaterOrEqual(-1, 0, "value");
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Guard_ShouldNotThrowOnPositive()
    {
        Action act = () => Guard.GreaterOrEqual(5, 0, "value");
        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_ShouldThrowOnInvalidEnum()
    {
        Action act = () => Guard.IsValidEnum<OrderStatus>((OrderStatus)999);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_ShouldNotThrowOnValidEnum()
    {
        Action act = () => Guard.IsValidEnum(OrderStatus.Pending);
        act.Should().NotThrow();
    }
}
