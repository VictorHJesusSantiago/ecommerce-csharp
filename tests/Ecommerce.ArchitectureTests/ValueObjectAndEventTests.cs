using FluentAssertions;
using Xunit;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.ArchitectureTests;

public class ValueObjectAndEventTests
{
    [Fact]
    public void Money_ShouldCreateWithAmountAndCurrency()
    {
        var money = new Money(100m, "USD");
        money.Amount.Should().Be(100m);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Money_ShouldThrowOnNegativeAmount()
    {
        Action act = () => new Money(-1m, "USD");
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Money_ShouldAdd()
    {
        var m1 = new Money(10m, "USD");
        var m2 = new Money(20m, "USD");
        var result = m1 + m2;
        result.Amount.Should().Be(30m);
    }

    [Fact]
    public void Money_ShouldSubtract()
    {
        var m1 = new Money(30m, "USD");
        var m2 = new Money(10m, "USD");
        var result = m1 - m2;
        result.Amount.Should().Be(20m);
    }

    [Fact]
    public void Money_ShouldMultiply()
    {
        var money = new Money(10m, "USD");
        var result = money * 3;
        result.Amount.Should().Be(30m);
    }

    [Fact]
    public void Money_ShouldCompareEqual()
    {
        var m1 = new Money(10m, "USD");
        var m2 = new Money(10m, "USD");
        m1.Should().Be(m2);
    }

    [Fact]
    public void Money_ShouldFormatDisplay()
    {
        var money = new Money(1234.56m, "USD");
        money.Display.Should().Contain("1");
    }

    [Fact]
    public void EmailAddress_ShouldCreateValidEmail()
    {
        var email = new EmailAddress("test@example.com");
        email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void EmailAddress_ShouldThrowOnInvalid()
    {
        Action act = () => new EmailAddress("invalid");
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void PhoneNumber_ShouldCreateValidPhone()
    {
        var phone = new PhoneNumber("+1234567890");
        phone.Value.Should().Be("+1234567890");
    }

    [Fact]
    public void PhoneNumber_ShouldThrowOnInvalid()
    {
        Action act = () => new PhoneNumber("abc");
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Slug_ShouldCreateFromText()
    {
        var slug = new Slug("Hello World");
        slug.Value.Should().Be("hello-world");
    }

    [Fact]
    public void Sku_ShouldCreateValidSku()
    {
        var sku = new Sku("PRD-001");
        sku.Value.Should().Be("PRD-001");
    }

    [Fact]
    public void Barcode_ShouldCreateValidBarcode()
    {
        var barcode = new Barcode("1234567890128");
        barcode.Value.Should().Be("1234567890128");
    }

    [Fact]
    public void Address_ShouldCreateWithAllFields()
    {
        var address = new Address(
            "123 Main St",
            "New York",
            "NY",
            "10001",
            "US"
        );

        address.Street.Should().Be("123 Main St");
        address.City.Should().Be("New York");
    }

    [Fact]
    public void DateRange_ShouldCreateValidRange()
    {
        var range = new DateRange(
            new DateTime(2024, 1, 1),
            new DateTime(2024, 12, 31)
        );

        range.Days.Should().Be(365);
    }

    [Fact]
    public void DateRange_ShouldThrowOnInvalidRange()
    {
        Action act = () => new DateRange(
            new DateTime(2024, 12, 31),
            new DateTime(2024, 1, 1)
        );
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Percentage_ShouldCreateValidPercentage()
    {
        var pct = new Percentage(25m);
        pct.Value.Should().Be(25m);
    }

    [Fact]
    public void Percentage_ShouldThrowOnNegative()
    {
        Action act = () => new Percentage(-5m);
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Percentage_ShouldThrowOnOver100()
    {
        Action act = () => new Percentage(150m);
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void DomainEvent_ShouldHaveTimestamp()
    {
        var entity = new Product();
        var @event = new ProductCreatedEvent(entity);
        @event.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}
