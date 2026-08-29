using FluentAssertions;
using Xunit;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.ArchitectureTests;

public class FactoryTests
{
    [Fact]
    public void ProductFactory_ShouldCreateProduct()
    {
        var product = ProductFactory.Create(
            "Test Product",
            "A test product",
            29.99m,
            "USD",
            "TST-001",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        product.Name.Should().Be("Test Product");
        product.Price.Amount.Should().Be(29.99m);
        product.Sku.Value.Should().Be("TST-001");
        product.IsActive.Should().BeTrue();
    }

    [Fact]
    public void ProductFactory_ShouldCreateFeaturedProduct()
    {
        var product = ProductFactory.CreateFeatured(
            "Featured Item",
            "Special product",
            49.99m,
            "USD",
            "FTR-001",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        product.IsFeatured.Should().BeTrue();
    }

    [Fact]
    public void CategoryFactory_ShouldCreateCategory()
    {
        var category = CategoryFactory.Create(
            "Electronics",
            "Electronic devices",
            null
        );

        category.Name.Should().Be("Electronics");
        category.Slug.Value.Should().Be("electronics");
        category.IsActive.Should().BeTrue();
    }

    [Fact]
    public void CategoryFactory_ShouldCreateSubCategory()
    {
        var parent = new Category { Id = Guid.NewGuid(), Name = "Electronics" };
        var child = CategoryFactory.CreateSubCategory(
            "Phones",
            "Mobile phones",
            parent.Id
        );

        child.ParentId.Should().Be(parent.Id);
    }

    [Fact]
    public void OrderFactory_ShouldCreateOrder()
    {
        var order = OrderFactory.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "John Doe",
            "john@example.com"
        );

        order.Status.Should().Be(OrderStatus.Pending);
        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void OrderFactory_ShouldCreateOrderWithItems()
    {
        var items = new List<(Guid ProductId, string ProductName, decimal Price, int Quantity)>
        {
            (Guid.NewGuid(), "Widget A", 19.99m, 2),
            (Guid.NewGuid(), "Widget B", 29.99m, 1)
        };

        var order = OrderFactory.CreateWithItems(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "John Doe",
            "john@example.com",
            items
        );

        order.Items.Should().HaveCount(2);
    }

    [Fact]
    public void CartFactory_ShouldCreateCart()
    {
        var cart = CartFactory.Create(Guid.NewGuid());

        cart.Items.Should().BeEmpty();
        cart.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void CartFactory_ShouldCreateCartItem()
    {
        var item = CartFactory.CreateCartItem(
            Guid.NewGuid(),
            Guid.NewGuid(),
            2,
            19.99m
        );

        item.Quantity.Should().Be(2);
        item.UnitPrice.Should().Be(19.99m);
    }
}
