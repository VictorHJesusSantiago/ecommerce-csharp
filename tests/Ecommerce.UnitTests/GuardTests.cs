using FluentAssertions;
using Ecommerce.Application.Common;

namespace Ecommerce.UnitTests.CommonTests;

public class GuardTests
{
    [Fact]
    public void Guard_NotNull_ShouldNotThrow()
    {
        var action = () => Guard.NotNull("value", "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNull_ShouldThrow()
    {
        var action = () => Guard.NotNull(null, "test");
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Guard_NotEmpty_ShouldNotThrow()
    {
        var action = () => Guard.NotEmpty(Guid.NewGuid(), "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotEmpty_ShouldThrow()
    {
        var action = () => Guard.NotEmpty(Guid.Empty, "test");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotNullOrWhiteSpace_ShouldNotThrow()
    {
        var action = () => Guard.NotNullOrWhiteSpace("hello", "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNullOrWhiteSpace_ShouldThrow()
    {
        var action = () => Guard.NotNullOrWhiteSpace("", "test");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_MinLength_ShouldNotThrow()
    {
        var action = () => Guard.MinLength("hello", 3, "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_MinLength_ShouldThrow()
    {
        var action = () => Guard.MinLength("hi", 5, "test");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_MaxLength_ShouldNotThrow()
    {
        var action = () => Guard.MaxLength("hello", 10, "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_MaxLength_ShouldThrow()
    {
        var action = () => Guard.MaxLength("hello world", 5, "test");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldNotThrow()
    {
        var action = () => Guard.GreaterThan(10, 5, "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldThrow()
    {
        var action = () => Guard.GreaterThan(3, 5, "test");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_GreaterThanOrEqualTo_ShouldNotThrow()
    {
        var action = () => Guard.GreaterThanOrEqualTo(5, 5, "test");
        action.Should().NotThrow();
    }

    [Fact]
    public void Guard_InvalidEnum_ShouldThrow()
    {
        var action = () => Guard.InvalidEnum((TestEnum)99, "test");
        action.Should().Throw<ArgumentException>();
    }
}

public enum TestEnum { Value1, Value2, Value3 }

public class ResultTests
{
    [Fact]
    public void Result_Success_ShouldReturnSuccess()
    {
        var result = Result<string>.Success("hello");
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("hello");
    }

    [Fact]
    public void Result_Failure_ShouldReturnError()
    {
        var result = Result<string>.Failure("error message");
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("error message");
    }

    [Fact]
    public void Result_SuccessWithoutValue_ShouldWork()
    {
        var result = Result.Success();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Result_FailureWithoutValue_ShouldWork()
    {
        var result = Result.Failure("something went wrong");
        result.IsSuccess.Should().BeFalse();
    }
}

public class PaginatedListTests
{
    [Fact]
    public void PaginatedList_ShouldCreateCorrectly()
    {
        var items = new List<int> { 1, 2, 3, 4, 5 };
        var paginated = new PaginatedList<int>(items, 10, 1, 5);

        paginated.Items.Should().HaveCount(5);
        paginated.TotalCount.Should().Be(10);
        paginated.PageIndex.Should().Be(1);
        paginated.TotalPages.Should().Be(2);
    }

    [Fact]
    public void PaginatedList_HasPreviousPage_ShouldBeFalse()
    {
        var paginated = new PaginatedList<int>([], 10, 1, 5);
        paginated.HasPreviousPage.Should().BeFalse();
    }

    [Fact]
    public void PaginatedList_HasNextPage_ShouldBeTrue()
    {
        var paginated = new PaginatedList<int>([], 10, 1, 5);
        paginated.HasNextPage.Should().BeTrue();
    }
}
