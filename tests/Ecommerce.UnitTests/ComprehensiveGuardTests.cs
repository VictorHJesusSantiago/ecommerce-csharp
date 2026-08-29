using Xunit;
using FluentAssertions;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.DTOs.Review;
using Ecommerce.Application.DTOs.Marketing;
using Ecommerce.Application.DTOs.Catalog;
using Ecommerce.Application.DTOs.Inventory;
using Ecommerce.Application.DTOs.Notification;
using Ecommerce.Application.DTOs.CMS;
using Ecommerce.Application.DTOs.Report;
using Ecommerce.Application.DTOs.Search;

namespace Ecommerce.UnitTests;

public class GuardTests
{
    [Fact]
    public void Guard_NotEmpty_ShouldThrowForEmptyGuid()
    {
        Action act = () => Application.Common.Guard.NotEmpty(Guid.Empty, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotEmpty_ShouldNotThrowForValidGuid()
    {
        Action act = () => Application.Common.Guard.NotEmpty(Guid.NewGuid(), "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNull_ShouldThrowForNull()
    {
        Action act = () => Application.Common.Guard.NotNull(null, "test");

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Guard_NotNull_ShouldNotThrowForNonNull()
    {
        Action act = () => Application.Common.Guard.NotNull("value", "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldThrowForNull()
    {
        Action act = () => Application.Common.Guard.NotNullOrEmpty(null, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldThrowForEmptyString()
    {
        Action act = () => Application.Common.Guard.NotNullOrEmpty("", "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldNotThrowForValidString()
    {
        Action act = () => Application.Common.Guard.NotNullOrEmpty("value", "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_NotNullOrWhiteSpace_ShouldThrowForWhiteSpace()
    {
        Action act = () => Application.Common.Guard.NotNullOrWhiteSpace("   ", "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_NotNullOrWhiteSpace_ShouldNotThrowForValidString()
    {
        Action act = () => Application.Common.Guard.NotNullOrWhiteSpace("value", "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldThrowForSmallerValue()
    {
        Action act = () => Application.Common.Guard.GreaterThan(5, 10, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_GreaterThan_ShouldNotThrowForLargerValue()
    {
        Action act = () => Application.Common.Guard.GreaterThan(15, 10, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_GreaterThanOrEqualTo_ShouldThrowForSmallerValue()
    {
        Action act = () => Application.Common.Guard.GreaterThanOrEqualTo(5, 10, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_GreaterThanOrEqualTo_ShouldNotThrowForEqualValue()
    {
        Action act = () => Application.Common.Guard.GreaterThanOrEqualTo(10, 10, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_LessThan_ShouldThrowForLargerValue()
    {
        Action act = () => Application.Common.Guard.LessThan(15, 10, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_LessThan_ShouldNotThrowForSmallerValue()
    {
        Action act = () => Application.Common.Guard.LessThan(5, 10, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_LessThanOrEqualTo_ShouldThrowForLargerValue()
    {
        Action act = () => Application.Common.Guard.LessThanOrEqualTo(15, 10, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_LessThanOrEqualTo_ShouldNotThrowForEqualValue()
    {
        Action act = () => Application.Common.Guard.LessThanOrEqualTo(10, 10, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_InRange_ShouldThrowForValueOutOfRange()
    {
        Action act = () => Application.Common.Guard.InRange(5, 10, 20, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_InRange_ShouldNotThrowForValueInRange()
    {
        Action act = () => Application.Common.Guard.InRange(15, 10, 20, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_EnumValid_ShouldThrowForInvalidEnum()
    {
        Action act = () => Application.Common.Guard.EnumValid<DayOfWeek>((DayOfWeek)100, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_EnumValid_ShouldNotThrowForValidEnum()
    {
        Action act = () => Application.Common.Guard.EnumValid<DayOfWeek>(DayOfWeek.Monday, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_MinLength_ShouldThrowForTooShortString()
    {
        Action act = () => Application.Common.Guard.MinLength("hi", 5, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_MinLength_ShouldNotThrowForValidLength()
    {
        Action act = () => Application.Common.Guard.MinLength("hello", 5, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_MaxLength_ShouldThrowForTooLongString()
    {
        Action act = () => Application.Common.Guard.MaxLength("hello world", 5, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_MaxLength_ShouldNotThrowForValidLength()
    {
        Action act = () => Application.Common.Guard.MaxLength("hello", 10, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_IsTrue_ShouldThrowForFalse()
    {
        Action act = () => Application.Common.Guard.IsTrue(false, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_IsTrue_ShouldNotThrowForTrue()
    {
        Action act = () => Application.Common.Guard.IsTrue(true, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_IsFalse_ShouldThrowForTrue()
    {
        Action act = () => Application.Common.Guard.IsFalse(true, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_IsFalse_ShouldNotThrowForFalse()
    {
        Action act = () => Application.Common.Guard.IsFalse(false, "test");

        act.Should().NotThrow();
    }

    [Fact]
    public void Guard_HasNoNulls_ShouldThrowForCollectionWithNulls()
    {
        var list = new List<string?> { "a", null, "c" };

        Action act = () => Application.Common.Guard.HasNoNulls(list, "test");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Guard_HasNoNulls_ShouldNotThrowForCollectionWithoutNulls()
    {
        var list = new List<string> { "a", "b", "c" };

        Action act = () => Application.Common.Guard.HasNoNulls(list, "test");

        act.Should().NotThrow();
    }
}
