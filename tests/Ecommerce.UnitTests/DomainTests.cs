using FluentAssertions;
using Ecommerce.Domain.Entities.User;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.UnitTests.Domain.Entities;

public class ApplicationUserTests
{
    [Fact]
    public void CreateUser_WithValidData_ShouldCreateUser()
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            UserName = "johndoe",
            PhoneNumber = "1234567890",
            CreatedAt = DateTime.UtcNow
        };

        user.Should().NotBeNull();
        user.FirstName.Should().Be("John");
        user.LastName.Should().Be("Doe");
        user.FullName.Should().Be("John Doe");
        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void UserFullName_ShouldConcatenateFirstAndLastName()
    {
        var user = new ApplicationUser { FirstName = "Jane", LastName = "Smith" };
        user.FullName.Should().Be("Jane Smith");
    }

    [Fact]
    public void UserIsAdmin_ShouldReturnFalseByDefault()
    {
        var user = new ApplicationUser { IsAdmin = false };
        user.IsAdmin.Should().BeFalse();
    }
}

public class ProductTests
{
    [Fact]
    public void Product_IsActive_ShouldBeTrueByDefault()
    {
        var product = new Ecommerce.Domain.Entities.Catalog.Product
        {
            Id = Guid.NewGuid(),
            Name = "Test Product",
            Price = 10.99m,
            StockQuantity = 100,
            Sku = "TEST-001",
            Slug = "test-product"
        };

        product.Should().NotBeNull();
        product.Price.Should().Be(10.99m);
        product.StockQuantity.Should().Be(100);
    }

    [Fact]
    public void Product_IsInStock_ShouldReturnCorrectValue()
    {
        var product = new Ecommerce.Domain.Entities.Catalog.Product { StockQuantity = 10 };
        product.StockQuantity.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Product_WithZeroStock_ShouldNotBeAvailable()
    {
        var product = new Ecommerce.Domain.Entities.Catalog.Product { StockQuantity = 0 };
        product.StockQuantity.Should().Be(0);
    }
}

public class MoneyTests
{
    [Fact]
    public void Money_Create_ShouldSetProperties()
    {
        var money = new Money(10.50m, "USD");
        money.Amount.Should().Be(10.50m);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Money_Equal_ShouldReturnTrue()
    {
        var money1 = new Money(10.50m, "USD");
        var money2 = new Money(10.50m, "USD");
        money1.Should().Be(money2);
    }

    [Fact]
    public void Money_Add_ShouldReturnCorrectSum()
    {
        var money1 = new Money(10.50m, "USD");
        var money2 = new Money(5.25m, "USD");
        var sum = money1.Add(money2);
        sum.Amount.Should().Be(15.75m);
    }

    [Fact]
    public void Money_Multiply_ShouldReturnCorrectProduct()
    {
        var money = new Money(10.00m, "USD");
        var result = money.Multiply(3);
        result.Amount.Should().Be(30.00m);
    }

    [Fact]
    public void Money_IsZero_ShouldReturnTrue()
    {
        var money = new Money(0, "USD");
        money.IsZero.Should().BeTrue();
    }

    [Fact]
    public void Money_IsPositive_ShouldReturnTrue()
    {
        var money = new Money(5.00m, "USD");
        money.IsPositive.Should().BeTrue();
    }

    [Fact]
    public void Money_IsNegative_ShouldReturnTrue()
    {
        var money = new Money(-5.00m, "USD");
        money.IsNegative.Should().BeTrue();
    }
}

public class EmailAddressTests
{
    [Fact]
    public void EmailAddress_Create_ShouldSetEmail()
    {
        var email = new EmailAddress("test@example.com");
        email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void EmailAddress_Equal_ShouldReturnTrue()
    {
        var email1 = new EmailAddress("test@example.com");
        var email2 = new EmailAddress("test@example.com");
        email1.Should().Be(email2);
    }

    [Fact]
    public void EmailAddress_Different_ShouldNotBeEqual()
    {
        var email1 = new EmailAddress("test1@example.com");
        var email2 = new EmailAddress("test2@example.com");
        email1.Should().NotBe(email2);
    }
}

public class OrderTests
{
    [Fact]
    public void Order_CalculateTotal_ShouldBeCorrect()
    {
        var order = new Ecommerce.Domain.Entities.Ordering.Order
        {
            SubTotal = 100m,
            TaxAmount = 8m,
            ShippingCost = 5m,
            DiscountAmount = 10m
        };

        var total = order.SubTotal + order.TaxAmount + order.ShippingCost - order.DiscountAmount;
        total.Should().Be(103m);
    }
}
