using Xunit;
using FluentAssertions;

namespace Ecommerce.UnitTests;

public class GuardTests
{
    [Fact]
    public void NotNull_ShouldNotThrow_WhenValueIsNotNull()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotNull("test", "value");
        act.Should().NotThrow();
    }

    [Fact]
    public void NotNull_ShouldThrow_WhenValueIsNull()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotNull(null, "value");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void NotEmpty_ShouldNotThrow_WhenGuidIsNotEmpty()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotEmpty(Guid.NewGuid(), "id");
        act.Should().NotThrow();
    }

    [Fact]
    public void NotEmpty_ShouldThrow_WhenGuidIsEmpty()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotEmpty(Guid.Empty, "id");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void NotNullOrWhiteSpace_ShouldNotThrow_WhenValueIsNotEmpty()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotNullOrWhiteSpace("hello", "name");
        act.Should().NotThrow();
    }

    [Fact]
    public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsNull()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotNullOrWhiteSpace(null, "name");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsEmpty()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotNullOrWhiteSpace("", "name");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void NotNullOrWhiteSpace_ShouldThrow_WhenValueIsWhitespace()
    {
        Action act = () => Ecommerce.Application.Common.Guard.NotNullOrWhiteSpace("   ", "name");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void GreaterThan_ShouldNotThrow_WhenValueIsGreaterThanMin()
    {
        Action act = () => Ecommerce.Application.Common.Guard.GreaterThan(10, 5, "amount");
        act.Should().NotThrow();
    }

    [Fact]
    public void GreaterThan_ShouldThrow_WhenValueIsLessThanMin()
    {
        Action act = () => Ecommerce.Application.Common.Guard.GreaterThan(3, 5, "amount");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void GreaterThanOrEqualTo_ShouldNotThrow_WhenValueIsEqualToMin()
    {
        Action act = () => Ecommerce.Application.Common.Guard.GreaterThanOrEqualTo(5, 5, "amount");
        act.Should().NotThrow();
    }

    [Fact]
    public void Positive_ShouldNotThrow_WhenValueIsPositive()
    {
        Action act = () => Ecommerce.Application.Common.Guard.Positive(10, "amount");
        act.Should().NotThrow();
    }

    [Fact]
    public void Positive_ShouldThrow_WhenValueIsZero()
    {
        Action act = () => Ecommerce.Application.Common.Guard.Positive(0, "amount");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void Positive_ShouldThrow_WhenValueIsNegative()
    {
        Action act = () => Ecommerce.Application.Common.Guard.Positive(-5, "amount");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void PositiveOrZero_ShouldNotThrow_WhenValueIsZero()
    {
        Action act = () => Ecommerce.Application.Common.Guard.PositiveOrZero(0, "amount");
        act.Should().NotThrow();
    }

    [Fact]
    public void MustBeTrue_ShouldNotThrow_WhenConditionIsTrue()
    {
        Action act = () => Ecommerce.Application.Common.Guard.MustBeTrue(true, "condition");
        act.Should().NotThrow();
    }

    [Fact]
    public void MustBeTrue_ShouldThrow_WhenConditionIsFalse()
    {
        Action act = () => Ecommerce.Application.Common.Guard.MustBeTrue(false, "condition");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void MaxLength_ShouldNotThrow_WhenWithinLimit()
    {
        Action act = () => Ecommerce.Application.Common.Guard.MaxLength("hello", 10, "name");
        act.Should().NotThrow();
    }

    [Fact]
    public void MaxLength_ShouldThrow_WhenExceedsLimit()
    {
        Action act = () => Ecommerce.Application.Common.Guard.MaxLength("hello world this is a long string", 10, "name");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void ValidEnum_ShouldNotThrow_WhenValidEnumValue()
    {
        Action act = () => Ecommerce.Application.Common.Guard.ValidEnum<Ecommerce.Domain.Enums.OrderStatus>(Ecommerce.Domain.Enums.OrderStatus.Pending);
        act.Should().NotThrow();
    }

    [Fact]
    public void CollectionNotEmpty_ShouldNotThrow_WhenHasItems()
    {
        Action act = () => Ecommerce.Application.Common.Guard.CollectionNotEmpty(new[] { 1, 2, 3 }, "items");
        act.Should().NotThrow();
    }

    [Fact]
    public void CollectionNotEmpty_ShouldThrow_WhenEmpty()
    {
        Action act = () => Ecommerce.Application.Common.Guard.CollectionNotEmpty(Array.Empty<int>(), "items");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }
}

public class StringExtensionTests
{
    [Fact]
    public void ToSlug_ShouldConvertCorrectly()
    {
        "Hello World".ToSlug().Should().Be("hello-world");
    }

    [Fact]
    public void ToSlug_ShouldRemoveSpecialCharacters()
    {
        "Hello @#$% World!".ToSlug().Should().Be("hello-world");
    }

    [Fact]
    public void Truncate_ShouldTruncateToMaxLength()
    {
        "Hello World".Truncate(5).Should().Be("Hello");
    }

    [Fact]
    public void Truncate_ShouldNotTruncate_WhenWithinLimit()
    {
        "Hi".Truncate(5).Should().Be("Hi");
    }

    [Fact]
    public void Truncate_ShouldAddEllipsis()
    {
        "Hello World".Truncate(5, "...").Should().Should().Be("Hello...");
    }

    [Fact]
    public void IsEmail_ShouldReturnTrueForValidEmail()
    {
        "test@example.com".IsEmail().Should().BeTrue();
    }

    [Fact]
    public void IsEmail_ShouldReturnFalseForInvalidEmail()
    {
        "invalid".IsEmail().Should().BeFalse();
    }

    [Fact]
    public void ContainsIgnoreCase_ShouldBeCaseInsensitive()
    {
        "Hello World".ContainsIgnoreCase("hello").Should().BeTrue();
    }

    [Fact]
    public void ToTitleCase_ShouldConvertCorrectly()
    {
        "hello world".ToTitleCase().Should().Be("Hello World");
    }
}

public class CollectionExtensionTests
{
    [Fact]
    public void AddIfNotExists_ShouldAdd_WhenNotExists()
    {
        var list = new List<int> { 1, 2, 3 };
        list.AddIfNotExists(4);
        list.Should().HaveCount(4);
    }

    [Fact]
    public void AddIfNotExists_ShouldNotAdd_WhenExists()
    {
        var list = new List<int> { 1, 2, 3 };
        list.AddIfNotExists(2);
        list.Should().HaveCount(3);
    }

    [Fact]
    public void ForEach_ShouldIterateAll()
    {
        var list = new List<int> { 1, 2, 3 };
        var sum = 0;
        list.ForEach(item => sum += item);
        sum.Should().Be(6);
    }

    [Fact]
    public void ToPagedList_ShouldReturnCorrectPage()
    {
        var list = Enumerable.Range(1, 100).ToList();
        var page = list.ToPagedList(2, 10);
        page.Should().HaveCount(10);
        page.First().Should().Be(11);
    }
}

public class DateTimeExtensionTests
{
    [Fact]
    public void ToRelativeTime_ShouldReturnCorrectString()
    {
        var date = DateTime.UtcNow.AddMinutes(-5);
        date.ToRelativeTime().Should().Contain("5");
    }

    [Fact]
    public void ToRelativeTime_ShouldReturnHoursString()
    {
        var date = DateTime.UtcNow.AddHours(-3);
        date.ToRelativeTime().Should().Contain("3");
    }

    [Fact]
    public void ToRelativeTime_ShouldReturnDaysString()
    {
        var date = DateTime.UtcNow.AddDays(-2);
        date.ToRelativeTime().Should().Contain("2");
    }
}

public class DecimalExtensionTests
{
    [Fact]
    public void ToCurrency_ShouldFormatCorrectly()
    {
        49.99m.ToCurrency().Should().Contain("49.99");
    }

    [Fact]
    public void ToPercentage_ShouldFormatCorrectly()
    {
        0.5m.ToPercentage().Should().Be("50.00%");
    }

    [Fact]
    public void RoundTo_ShouldRoundCorrectly()
    {
        49.999m.RoundTo(2).Should().Be(50.00m);
    }
}

public class GuidExtensionTests
{
    [Fact]
    public void IsEmpty_ShouldReturnTrueForEmptyGuid()
    {
        Guid.Empty.IsEmpty().Should().BeTrue();
    }

    [Fact]
    public void IsEmpty_ShouldReturnFalseForNonEmptyGuid()
    {
        Guid.NewGuid().IsEmpty().Should().BeFalse();
    }
}
