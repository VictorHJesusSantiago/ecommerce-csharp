using FluentAssertions;
using Xunit;
using Ecommerce.Application.Common;
using Ecommerce.Application.Factories;

namespace Ecommerce.ArchitectureTests;

public class GuardAndExtensionTests
{
    [Fact]
    public void Guard_NotNull_ShouldNotThrow_WhenValueIsNotNull()
    {
        Action act = () => Guard.NotNull("test", "value");
        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNull_ShouldThrow_WhenValueIsNull()
    {
        Action act = () => Guard.NotNull(null!, "value");
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldThrow_WhenEmpty()
    {
        Action act = () => Guard.NotNullOrEmpty("", "value");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldNotThrow_WhenValid()
    {
        Action act = () => Guard.NotNullOrEmpty("hello", "value");
        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotEmpty_ShouldThrow_WhenGuidEmpty()
    {
        Action act = () => Guard.NotEmpty(Guid.Empty, "id");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotEmpty_ShouldNotThrow_WhenGuidValid()
    {
        Action act = () => Guard.NotEmpty(Guid.NewGuid(), "id");
        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldThrow_WhenLessThan()
    {
        Action act = () => Guard.GreaterThan(5, 10, "value");
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldNotThrow_WhenGreater()
    {
        Action act = () => Guard.GreaterThan(15, 10, "value");
        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_MaxLength_ShouldThrow_WhenExceeded()
    {
        Action act = () => Guard.MaxLength("hello world", 5, "value");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void StringExtensions_ToSlug_ShouldCreateSlug()
    {
        "Hello World".ToSlug().Should().Be("hello-world");
    }

    [Fact]
    public void StringExtensions_Truncate_ShouldTruncateLongString()
    {
        "This is a long string".Truncate(10).Should().HaveLength(13); // 10 + "..."
    }

    [Fact]
    public void StringExtensions_ToCurrency_ShouldFormatCorrectly()
    {
        1234.5m.ToCurrency().Should().Contain("1");
    }

    [Fact]
    public void CollectionExtensions_AddIfNotExists_ShouldAddNew()
    {
        var list = new List<string> { "a", "b" };
        list.AddIfNotExists("c");
        list.Should().HaveCount(3);
    }

    [Fact]
    public void CollectionExtensions_AddIfNotExists_ShouldNotAddDuplicate()
    {
        var list = new List<string> { "a", "b" };
        list.AddIfNotExists("a");
        list.Should().HaveCount(2);
    }

    [Fact]
    public void CollectionExtensions_RemoveAll_ShouldRemoveMatching()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        list.RemoveAll(x => x % 2 == 0);
        list.Should().BeEquivalentTo(new[] { 1, 3, 5 });
    }

    [Fact]
    public void DateTimeExtensions_IsExpired_ShouldReturnTrue_WhenPastDate()
    {
        DateTime.UtcNow.AddDays(-1).IsExpired().Should().BeTrue();
    }

    [Fact]
    public void DateTimeExtensions_IsExpired_ShouldReturnFalse_WhenFutureDate()
    {
        DateTime.UtcNow.AddDays(1).IsExpired().Should().BeFalse();
    }

    [Fact]
    public void DateTimeExtensions_ToRelative_ShouldReturnCorrectString()
    {
        var result = DateTime.UtcNow.AddMinutes(-5).ToRelative();
        result.Should().Contain("minutes ago");
    }

    [Fact]
    public void ResponseFactory_Success_ShouldCreateSuccessResponse()
    {
        var response = ResponseFactory.Success("Done");
        response.Success.Should().BeTrue();
        response.Message.Should().Be("Done");
    }

    [Fact]
    public void ResponseFactory_Error_ShouldCreateErrorResponse()
    {
        var response = ResponseFactory.Error("Failed");
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Failed");
    }

    [Fact]
    public void ResponseFactory_Paginated_ShouldCreatePaginatedResponse()
    {
        var items = new List<int> { 1, 2, 3 };
        var response = ResponseFactory.Paginated(items, 10, 1, 5);
        response.TotalCount.Should().Be(10);
        response.CurrentPage.Should().Be(1);
    }
}
