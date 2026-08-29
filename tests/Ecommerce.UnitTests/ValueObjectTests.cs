using Xunit;
using FluentAssertions;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Domain.Policies.Standard;

namespace Ecommerce.UnitTests.Domain;

public class MoneyTests
{
    [Fact]
    public void Money_Create_ShouldSetProperties()
    {
        var money = new Money(49.99m, "USD");

        money.Amount.Should().Be(49.99m);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Money_Add_ShouldAddAmounts()
    {
        var money1 = new Money(49.99m, "USD");
        var money2 = new Money(29.99m, "USD");

        var result = money1 + money2;

        result.Amount.Should().Be(79.98m);
        result.Currency.Should().Be("USD");
    }

    [Fact]
    public void Money_Subtract_ShouldSubtractAmounts()
    {
        var money1 = new Money(49.99m, "USD");
        var money2 = new Money(19.99m, "USD");

        var result = money1 - money2;

        result.Amount.Should().Be(30.00m);
    }

    [Fact]
    public void Money_Multiply_ShouldMultiplyAmount()
    {
        var money = new Money(49.99m, "USD");

        var result = money * 2;

        result.Amount.Should().Be(99.98m);
    }

    [Fact]
    public void Money_Equals_ShouldReturnTrueForEqualValues()
    {
        var money1 = new Money(49.99m, "USD");
        var money2 = new Money(49.99m, "USD");

        money1.Should().Be(money2);
    }

    [Fact]
    public void Money_Equals_ShouldReturnFalseForDifferentValues()
    {
        var money1 = new Money(49.99m, "USD");
        var money2 = new Money(59.99m, "USD");

        money1.Should().NotBe(money2);
    }

    [Fact]
    public void Money_GreaterThan_ShouldReturnTrue()
    {
        var money1 = new Money(49.99m, "USD");
        var money2 = new Money(29.99m, "USD");

        (money1 > money2).Should().BeTrue();
    }

    [Fact]
    public void Money_LessThan_ShouldReturnTrue()
    {
        var money1 = new Money(29.99m, "USD");
        var money2 = new Money(49.99m, "USD");

        (money1 < money2).Should().BeTrue();
    }

    [Fact]
    public void Money_IsZero_ShouldReturnTrueForZero()
    {
        var money = new Money(0m, "USD");

        money.IsZero.Should().BeTrue();
    }

    [Fact]
    public void Money_IsZero_ShouldReturnFalseForNonZero()
    {
        var money = new Money(49.99m, "USD");

        money.IsZero.Should().BeFalse();
    }

    [Fact]
    public void Money_ToString_ShouldFormatCorrectly()
    {
        var money = new Money(49.99m, "USD");

        money.ToString().Should().Be("$49.99");
    }

    [Fact]
    public void Money_DifferentCurrencies_ShouldThrowOnAdd()
    {
        var money1 = new Money(49.99m, "USD");
        var money2 = new Money(29.99m, "EUR");

        Action act = () => { var _ = money1 + money2; };

        act.Should().Throw<InvalidOperationException>();
    }
}

public class EmailAddressTests
{
    [Fact]
    public void EmailAddress_Create_ShouldSetProperties()
    {
        var email = new EmailAddress("test@example.com");

        email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void EmailAddress_Create_InvalidEmail_ShouldThrow()
    {
        Action act = () => new EmailAddress("invalid-email");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void EmailAddress_Equals_ShouldReturnTrueForEqualEmails()
    {
        var email1 = new EmailAddress("test@example.com");
        var email2 = new EmailAddress("test@example.com");

        email1.Should().Be(email2);
    }

    [Fact]
    public void EmailAddress_Equals_ShouldReturnFalseForDifferentEmails()
    {
        var email1 = new EmailAddress("test1@example.com");
        var email2 = new EmailAddress("test2@example.com");

        email1.Should().NotBe(email2);
    }

    [Fact]
    public void EmailAddress_ToString_ShouldReturnEmail()
    {
        var email = new EmailAddress("test@example.com");

        email.ToString().Should().Be("test@example.com");
    }
}

public class AddressTests
{
    [Fact]
    public void Address_Create_ShouldSetProperties()
    {
        var address = new Address
        {
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States"
        };

        address.Street.Should().Be("123 Main St");
        address.City.Should().Be("New York");
        address.State.Should().Be("NY");
        address.PostalCode.Should().Be("10001");
        address.Country.Should().Be("United States");
    }

    [Fact]
    public void Address_Equals_ShouldReturnTrueForEqualAddresses()
    {
        var address1 = new Address
        {
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States"
        };
        var address2 = new Address
        {
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States"
        };

        address1.Should().Be(address2);
    }

    [Fact]
    public void Address_FullAddress_ShouldFormatCorrectly()
    {
        var address = new Address
        {
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States"
        };

        var fullAddress = $"{address.Street}, {address.City}, {address.State} {address.PostalCode}, {address.Country}";

        fullAddress.Should().Be("123 Main St, New York, NY 10001, United States");
    }
}

public class SlugTests
{
    [Fact]
    public void Slug_Create_ShouldSetProperties()
    {
        var slug = new Slug("test-product");

        slug.Value.Should().Be("test-product");
    }

    [Fact]
    public void Slug_CreateFromText_ShouldGenerateSlug()
    {
        var slug = Slug.Create("Test Product Name");

        slug.Value.Should().Be("test-product-name");
    }

    [Fact]
    public void Slug_Equals_ShouldReturnTrueForEqualSlugs()
    {
        var slug1 = new Slug("test-product");
        var slug2 = new Slug("test-product");

        slug1.Should().Be(slug2);
    }

    [Fact]
    public void Slug_ToString_ShouldReturnSlug()
    {
        var slug = new Slug("test-product");

        slug.ToString().Should().Be("test-product");
    }
}

public class SkuTests
{
    [Fact]
    public void Sku_Create_ShouldSetProperties()
    {
        var sku = new Sku("SKU-001");

        sku.Value.Should().Be("SKU-001");
    }

    [Fact]
    public void Sku_Equals_ShouldReturnTrueForEqualSkus()
    {
        var sku1 = new Sku("SKU-001");
        var sku2 = new Sku("SKU-001");

        sku1.Should().Be(sku2);
    }

    [Fact]
    public void Sku_ToString_ShouldReturnSku()
    {
        var sku = new Sku("SKU-001");

        sku.ToString().Should().Be("SKU-001");
    }
}

public class PercentageTests
{
    [Fact]
    public void Percentage_Create_ShouldSetProperties()
    {
        var percentage = new Percentage(25.5m);

        percentage.Value.Should().Be(25.5m);
    }

    [Fact]
    public void Percentage_Create_InvalidPercentage_ShouldThrow()
    {
        Action act = () => new Percentage(150m);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Percentage_Apply_ShouldCalculateCorrectly()
    {
        var percentage = new Percentage(20m);
        var amount = 100m;

        var result = percentage.Apply(amount);

        result.Should().Be(20m);
    }

    [Fact]
    public void Percentage_Remove_ShouldCalculateCorrectly()
    {
        var percentage = new Percentage(20m);
        var amount = 100m;

        var result = percentage.Remove(amount);

        result.Should().Be(80m);
    }
}

public class DateRangeTests
{
    [Fact]
    public void DateRange_Create_ShouldSetProperties()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 12, 31);

        var dateRange = new DateRange(start, end);

        dateRange.Start.Should().Be(start);
        dateRange.End.Should().Be(end);
    }

    [Fact]
    public void DateRange_Create_InvalidRange_ShouldThrow()
    {
        var start = new DateTime(2024, 12, 31);
        var end = new DateTime(2024, 1, 1);

        Action act = () => new DateRange(start, end);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DateRange_Contains_ShouldReturnTrueForDateInRange()
    {
        var dateRange = new DateRange(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));
        var date = new DateTime(2024, 6, 15);

        dateRange.Contains(date).Should().BeTrue();
    }

    [Fact]
    public void DateRange_Contains_ShouldReturnFalseForDateOutOfRange()
    {
        var dateRange = new DateRange(new DateTime(2024, 1, 1), new DateTime(2024, 6, 30));
        var date = new DateTime(2024, 12, 15);

        dateRange.Contains(date).Should().BeFalse();
    }

    [Fact]
    public void DateRange_Duration_ShouldReturnCorrectDuration()
    {
        var dateRange = new DateRange(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));

        dateRange.Duration.Should().Be(TimeSpan.FromDays(364));
    }
}
