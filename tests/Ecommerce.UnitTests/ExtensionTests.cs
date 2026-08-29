using Xunit;
using FluentAssertions;
using Ecommerce.Application.Extensions;

namespace Ecommerce.UnitTests;

public class StringExtensionTests
{
    [Theory]
    [InlineData("Hello World", "hello-world")]
    [InlineData("Test Product Name", "test-product-name")]
    [InlineData("Special Characters!@#", "special-characters")]
    [InlineData("  Trimmed  ", "trimmed")]
    [InlineData("Multiple   Spaces", "multiple-spaces")]
    [InlineData("CamelCase", "camelcase")]
    [InlineData("UPPERCASE", "uppercase")]
    public void ToSlug_ShouldConvertCorrectly(string input, string expected)
    {
        var result = input.ToSlug();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("hello world", "Hello World")]
    [InlineData("hello", "Hello")]
    [InlineData("HELLO WORLD", "Hello World")]
    public void ToTitleCase_ShouldConvertCorrectly(string input, string expected)
    {
        var result = input.ToTitleCase();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("helloWorld", "hello_world")]
    [InlineData("helloWorldTest", "hello_world_test")]
    public void ToSnakeCase_ShouldConvertCorrectly(string input, string expected)
    {
        var result = input.ToSnakeCase();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("hello_world", "helloWorld")]
    [InlineData("hello_world_test", "helloWorldTest")]
    public void ToCamelCase_ShouldConvertCorrectly(string input, string expected)
    {
        var result = input.ToCamelCase();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("hello", "hello")]
    public void NullIfEmpty_ShouldReturnCorrectly(string? input, string expected)
    {
        var result = input.NullIfEmpty();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("hello", "hello")]
    public void NullIfWhiteSpace_ShouldReturnCorrectly(string? input, string expected)
    {
        var result = input.NullIfWhiteSpace();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World", 5, "Hello")]
    [InlineData("Hello", 10, "Hello")]
    [InlineData("Hello World", 5, "...", "Hello...")]
    public void Truncate_ShouldTruncateCorrectly(string input, int maxLength, string? suffix, string expected)
    {
        var result = input.Truncate(maxLength, suffix);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World", true)]
    [InlineData("123", true)]
    [InlineData("!@#", false)]
    [InlineData("", false)]
    public void IsAlphaNumeric_ShouldReturnCorrectly(string input, bool expected)
    {
        var result = input.IsAlphaNumeric();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("", false)]
    public void IsValidEmail_ShouldReturnCorrectly(string input, bool expected)
    {
        var result = input.IsValidEmail();

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("hello", 3, "hel")]
    [InlineData("hello", 10, "hello")]
    [InlineData("", 5, "")]
    public void Left_ShouldReturnCorrectSubstring(string input, int count, string expected)
    {
        var result = input.Left(count);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("hello", 3, "llo")]
    [InlineData("hello", 10, "hello")]
    [InlineData("", 5, "")]
    public void Right_ShouldReturnCorrectSubstring(string input, int count, string expected)
    {
        var result = input.Right(count);

        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveSpecialCharacters_ShouldRemoveNonAlphanumeric()
    {
        var result = "Hello!@# World$%^".RemoveSpecialCharacters();

        result.Should().Be("Hello World");
    }

    [Fact]
    public void ToSlug_ShouldHandleEmptyString()
    {
        var result = "".ToSlug();

        result.Should().BeEmpty();
    }

    [Fact]
    public void ToSlug_ShouldHandleNull()
    {
        var result = ((string?)null).ToSlug();

        result.Should().BeEmpty();
    }

    [Fact]
    public void ContainsIgnoreCase_ShouldBeCaseInsensitive()
    {
        "Hello World".ContainsIgnoreCase("hello").Should().BeTrue();
        "Hello World".ContainsIgnoreCase("WORLD").Should().BeTrue();
        "Hello World".ContainsIgnoreCase("test").Should().BeFalse();
    }

    [Fact]
    public void EqualsIgnoreCase_ShouldBeCaseInsensitive()
    {
        "Hello".EqualsIgnoreCase("hello").Should().BeTrue();
        "Hello".EqualsIgnoreCase("HELLO").Should().BeTrue();
        "Hello".EqualsIgnoreCase("World").Should().BeFalse();
    }

    [Fact]
    public void RemoveAccents_ShouldRemoveAccents()
    {
        var result = "Café résumé".RemoveAccents();

        result.Should().Be("Cafe resume");
    }

    [Fact]
    public void ToWords_ShouldSplitCamelCase()
    {
        var result = "helloWorld".ToWords();

        result.Should().Be("hello world");
    }

    [Fact]
    public void ToWords_ShouldSplitPascalCase()
    {
        var result = "HelloWorld".ToWords();

        result.Should().Be("hello world");
    }

    [Fact]
    public void CountOccurrences_ShouldCountCorrectly()
    {
        var result = "hello world hello".CountOccurrences("hello");

        result.Should().Be(2);
    }

    [Fact]
    public void Reverse_ShouldReverseString()
    {
        var result = "hello".Reverse();

        result.Should().Be("olleh");
    }

    [Fact]
    public void ToInitials_ShouldReturnInitials()
    {
        var result = "John Doe".ToInitials();

        result.Should().Be("JD");
    }

    [Fact]
    public void ToInitials_SingleName_ShouldReturnFirstLetter()
    {
        var result = "John".ToInitials();

        result.Should().Be("J");
    }
}

public class CollectionExtensionTests
{
    [Fact]
    public void EmptyIfNull_ShouldReturnEmptyForNull()
    {
        List<int>? list = null;

        var result = list.EmptyIfNull();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void EmptyIfNull_ShouldReturnSameListForNonNull()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.EmptyIfNull();

        result.Should().BeSameAs(list);
    }

    [Fact]
    public void ToPagedList_ShouldPaginateCorrectly()
    {
        var list = Enumerable.Range(1, 100).ToList();

        var result = list.ToPagedList(1, 10);

        result.Should().HaveCount(10);
        result.First().Should().Be(1);
        result.Last().Should().Be(10);
    }

    [Fact]
    public void ToPagedList_SecondPage_ShouldReturnCorrectItems()
    {
        var list = Enumerable.Range(1, 100).ToList();

        var result = list.ToPagedList(2, 10);

        result.Should().HaveCount(10);
        result.First().Should().Be(11);
        result.Last().Should().Be(20);
    }

    [Fact]
    public void ToPagedList_LastPage_ShouldReturnRemainingItems()
    {
        var list = Enumerable.Range(1, 25).ToList();

        var result = list.ToPagedList(3, 10);

        result.Should().HaveCount(5);
        result.First().Should().Be(21);
        result.Last().Should().Be(25);
    }

    [Fact]
    public void DistinctBy_ShouldReturnDistinctByProperty()
    {
        var list = new List<(string Name, int Age)>
        {
            ("John", 25),
            ("Jane", 30),
            ("John", 35)
        };

        var result = list.DistinctBy(x => x.Name).ToList();

        result.Should().HaveCount(2);
    }

    [Fact]
    public void Shuffle_ShouldReturnSameCount()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };

        var result = list.Shuffle().ToList();

        result.Should().HaveCount(5);
    }

    [Fact]
    public void ForEach_ShouldExecuteActionForAllItems()
    {
        var list = new List<int> { 1, 2, 3 };
        var sum = 0;

        list.ForEach(item => sum += item);

        sum.Should().Be(6);
    }

    [Fact]
    public void AddIfNotExists_ShouldAddWhenNotExists()
    {
        var list = new List<int> { 1, 2, 3 };

        list.AddIfNotExists(4);

        list.Should().HaveCount(4);
        list.Should().Contain(4);
    }

    [Fact]
    public void AddIfNotExists_ShouldNotAddWhenExists()
    {
        var list = new List<int> { 1, 2, 3 };

        list.AddIfNotExists(2);

        list.Should().HaveCount(3);
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrueForNull()
    {
        List<int>? list = null;

        list.IsNullOrEmpty().Should().BeTrue();
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnTrueForEmpty()
    {
        var list = new List<int>();

        list.IsNullOrEmpty().Should().BeTrue();
    }

    [Fact]
    public void IsNullOrEmpty_ShouldReturnFalseForNonEmpty()
    {
        var list = new List<int> { 1 };

        list.IsNullOrEmpty().Should().BeFalse();
    }
}

public class DateTimeExtensionTests
{
    [Fact]
    public void ToRelativeTime_ShouldReturnNow()
    {
        var date = DateTime.UtcNow.AddSeconds(-30);

        var result = date.ToRelativeTime();

        result.Should().Be("just now");
    }

    [Fact]
    public void ToRelativeTime_ShouldReturnMinutesAgo()
    {
        var date = DateTime.UtcNow.AddMinutes(-5);

        var result = date.ToRelativeTime();

        result.Should().Contain("minutes ago");
    }

    [Fact]
    public void ToRelativeTime_ShouldReturnHoursAgo()
    {
        var date = DateTime.UtcNow.AddHours(-2);

        var result = date.ToRelativeTime();

        result.Should().Contain("hours ago");
    }

    [Fact]
    public void ToRelativeTime_ShouldReturnDaysAgo()
    {
        var date = DateTime.UtcNow.AddDays(-3);

        var result = date.ToRelativeTime();

        result.Should().Contain("days ago");
    }

    [Fact]
    public void IsToday_ShouldReturnTrueForToday()
    {
        DateTime.Today.IsToday().Should().BeTrue();
    }

    [Fact]
    public void IsToday_ShouldReturnFalseForYesterday()
    {
        DateTime.Today.AddDays(-1).IsToday().Should().BeFalse();
    }

    [Fact]
    public void IsYesterday_ShouldReturnTrueForYesterday()
    {
        DateTime.Today.AddDays(-1).IsYesterday().Should().BeTrue();
    }

    [Fact]
    public void IsTomorrow_ShouldReturnTrueForTomorrow()
    {
        DateTime.Today.AddDays(1).IsTomorrow().Should().BeTrue();
    }

    [Fact]
    public void IsWeekend_ShouldReturnTrueForSaturday()
    {
        var saturday = DateTime.Today;
        while (saturday.DayOfWeek != DayOfWeek.Saturday)
            saturday = saturday.AddDays(1);

        saturday.IsWeekend().Should().BeTrue();
    }

    [Fact]
    public void IsWeekend_ShouldReturnFalseForMonday()
    {
        var monday = DateTime.Today;
        while (monday.DayOfWeek != DayOfWeek.Monday)
            monday = monday.AddDays(1);

        monday.IsWeekend().Should().BeFalse();
    }

    [Fact]
    public void StartOfDay_ShouldReturnMidnight()
    {
        var date = new DateTime(2024, 1, 15, 14, 30, 0);

        var result = date.StartOfDay();

        result.Hour.Should().Be(0);
        result.Minute.Should().Be(0);
        result.Second.Should().Be(0);
    }

    [Fact]
    public void EndOfDay_ShouldReturnBeforeMidnight()
    {
        var date = new DateTime(2024, 1, 15, 14, 30, 0);

        var result = date.EndOfDay();

        result.Hour.Should().Be(23);
        result.Minute.Should().Be(59);
        result.Second.Should().Be(59);
    }

    [Fact]
    public void ToUnixTimestamp_ShouldReturnCorrectValue()
    {
        var date = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var result = date.ToUnixTimestamp();

        result.Should().BeGreaterThan(0);
    }
}

public class DecimalExtensionTests
{
    [Fact]
    public void ToCurrency_ShouldFormatCorrectly()
    {
        var amount = 1234.56m;

        var result = amount.ToCurrency();

        result.Should().Contain("1,234.56");
    }

    [Fact]
    public void ToPercentage_ShouldFormatCorrectly()
    {
        var value = 0.85m;

        var result = value.ToPercentage();

        result.Should().Be("85%");
    }

    [Fact]
    public void IsPositive_ShouldReturnTrueForPositive()
    {
        49.99m.IsPositive().Should().BeTrue();
    }

    [Fact]
    public void IsPositive_ShouldReturnFalseForNegative()
    {
        (-49.99m).IsPositive().Should().BeFalse();
    }

    [Fact]
    public void IsNegative_ShouldReturnTrueForNegative()
    {
        (-49.99m).IsNegative().Should().BeTrue();
    }

    [Fact]
    public void IsNegative_ShouldReturnFalseForPositive()
    {
        49.99m.IsNegative().Should().BeFalse();
    }

    [Fact]
    public void IsZero_ShouldReturnTrueForZero()
    {
        0m.IsZero().Should().BeTrue();
    }

    [Fact]
    public void IsZero_ShouldReturnFalseForNonZero()
    {
        49.99m.IsZero().Should().BeFalse();
    }
}

public class GuidExtensionTests
{
    [Fact]
    public void IsEmpty_ShouldReturnTrueForEmpty()
    {
        Guid.Empty.IsEmpty().Should().BeTrue();
    }

    [Fact]
    public void IsEmpty_ShouldReturnFalseForNonEmpty()
    {
        Guid.NewGuid().IsEmpty().Should().BeFalse();
    }

    [Fact]
    public void ToShortString_ShouldReturnShortenedGuid()
    {
        var guid = Guid.NewGuid();

        var result = guid.ToShortString();

        result.Should().HaveLength(8);
    }
}
