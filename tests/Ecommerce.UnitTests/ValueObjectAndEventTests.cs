using Xunit;
using FluentAssertions;

namespace Ecommerce.UnitTests;

public class ValueObjectEqualityTests
{
    [Fact]
    public void Money_SameValues_ShouldBeEqual()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        m1.Should().Be(m2);
    }

    [Fact]
    public void Money_DifferentValues_ShouldNotBeEqual()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(200, "USD");
        m1.Should().NotBe(m2);
    }

    [Fact]
    public void Money_DifferentCurrency_ShouldNotBeEqual()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(100, "EUR");
        m1.Should().NotBe(m2);
    }

    [Fact]
    public void Money_SameValues_ShouldHaveSameHashCode()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        m1.GetHashCode().Should().Be(m2.GetHashCode());
    }

    [Fact]
    public void Money_Add_ShouldReturnCorrectResult()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(50, "USD");
        var result = m1 + m2;
        result.Amount.Should().Be(150);
    }

    [Fact]
    public void Money_Subtract_ShouldReturnCorrectResult()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(30, "USD");
        var result = m1 - m2;
        result.Amount.Should().Be(70);
    }

    [Fact]
    public void Money_Multiply_ShouldReturnCorrectResult()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        var result = m1 * 3;
        result.Amount.Should().Be(300);
    }

    [Fact]
    public void Money_GreaterThan_ShouldReturnTrue()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(200, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        (m1 > m2).Should().BeTrue();
    }

    [Fact]
    public void Money_LessThan_ShouldReturnTrue()
    {
        var m1 = new Ecommerce.Domain.ValueObjects.Money(50, "USD");
        var m2 = new Ecommerce.Domain.ValueObjects.Money(100, "USD");
        (m1 < m2).Should().BeTrue();
    }

    [Fact]
    public void Money_Zero_ShouldReturnZero()
    {
        var m = Ecommerce.Domain.ValueObjects.Money.Zero("USD");
        m.Amount.Should().Be(0);
        m.Currency.Should().Be("USD");
    }

    [Fact]
    public void EmailAddress_ValidEmail_ShouldCreate()
    {
        var email = new Ecommerce.Domain.ValueObjects.EmailAddress("test@example.com");
        email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void EmailAddress_InvalidEmail_ShouldThrow()
    {
        Action act = () => new Ecommerce.Domain.ValueObjects.EmailAddress("invalid");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void PhoneNumber_ValidPhone_ShouldCreate()
    {
        var phone = new Ecommerce.Domain.ValueObjects.PhoneNumber("+1234567890");
        phone.Value.Should().Be("+1234567890");
    }

    [Fact]
    public void PhoneNumber_InvalidPhone_ShouldThrow()
    {
        Action act = () => new Ecommerce.Domain.ValueObjects.PhoneNumber("abc");
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }

    [Fact]
    public void Address_SameValues_ShouldBeEqual()
    {
        var a1 = new Ecommerce.Domain.ValueObjects.Address("123 Main St", "New York", "NY", "10001", "US");
        var a2 = new Ecommerce.Domain.ValueObjects.Address("123 Main St", "New York", "NY", "10001", "US");
        a1.Should().Be(a2);
    }

    [Fact]
    public void Slug_FromName_ShouldCreateCorrectSlug()
    {
        var slug = Ecommerce.Domain.ValueObjects.Slug.Create("Hello World Product");
        slug.Value.Should().Be("hello-world-product");
    }

    [Fact]
    public void Slug_SpecialCharacters_ShouldBeRemoved()
    {
        var slug = Ecommerce.Domain.ValueObjects.Slug.Create("Product @#$% Name!");
        slug.Value.Should().Be("product-name");
    }

    [Fact]
    public void Sku_Valid_ShouldCreate()
    {
        var sku = new Ecommerce.Domain.ValueObjects.Sku("SKU-001");
        sku.Value.Should().Be("SKU-001");
    }

    [Fact]
    public void Percentage_Valid_ShouldCreate()
    {
        var pct = new Ecommerce.Domain.ValueObjects.Percentage(25.5m);
        pct.Value.Should().Be(25.5m);
    }

    [Fact]
    public void Percentage_Invalid_ShouldThrow()
    {
        Action act = () => new Ecommerce.Domain.ValueObjects.Percentage(101m);
        act.Should().Throw<Ecommerce.Domain.Exceptions.DomainException>();
    }
}

public class DomainEventTests
{
    [Fact]
    public void DomainEvent_ShouldHaveTimestamp()
    {
        var evt = new Ecommerce.Domain.Events.ProductCreatedEvent(Guid.NewGuid(), "Test Product");
        evt.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void DomainEvent_ShouldHaveEventId()
    {
        var evt = new Ecommerce.Domain.Events.ProductCreatedEvent(Guid.NewGuid(), "Test Product");
        evt.EventId.Should().NotBeEmpty();
    }

    [Fact]
    public void OrderPlacedEvent_ShouldContainOrderId()
    {
        var evt = new Ecommerce.Domain.Events.OrderPlacedEvent(Guid.NewGuid(), 100m, "USD");
        evt.OrderId.Should().NotBeEmpty();
    }

    [Fact]
    public void OrderPlacedEvent_ShouldContainTotalAmount()
    {
        var evt = new Ecommerce.Domain.Events.OrderPlacedEvent(Guid.NewGuid(), 100m, "USD");
        evt.TotalAmount.Should().Be(100m);
    }

    [Fact]
    public void PaymentProcessedEvent_ShouldContainPaymentId()
    {
        var evt = new Ecommerce.Domain.Events.PaymentProcessedEvent(Guid.NewGuid(), Guid.NewGuid(), 100m, "USD", "Completed");
        evt.PaymentId.Should().NotBeEmpty();
    }

    [Fact]
    public void UserRegisteredEvent_ShouldContainUserId()
    {
        var evt = new Ecommerce.Domain.Events.UserRegisteredEvent(Guid.NewGuid(), "john@example.com", "John Doe");
        evt.UserId.Should().NotBeEmpty();
    }

    [Fact]
    public void StockLevelChangedEvent_ShouldContainProductId()
    {
        var evt = new Ecommerce.Domain.Events.StockLevelChangedEvent(Guid.NewGuid(), Guid.NewGuid(), 50, 100);
        evt.ProductId.Should().NotBeEmpty();
    }
}
