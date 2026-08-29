using Xunit;
using FluentAssertions;
using Ecommerce.Domain.Factories;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;

namespace Ecommerce.UnitTests.Domain;

public class ProductFactoryTests
{
    [Fact]
    public void CreateProduct_ShouldReturnProductWithCorrectProperties()
    {
        var product = ProductFactory.CreateProduct("Test Product", 49.99m, "SKU-001", "test-product");

        product.Should().NotBeNull();
        product.Name.Should().Be("Test Product");
        product.Price.Should().Be(49.99m);
        product.Sku.Should().Be("SKU-001");
        product.Slug.Should().Be("test-product");
    }

    [Fact]
    public void CreateProduct_ShouldGenerateId()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");

        product.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void CreateProduct_ShouldSetCreatedAtToUtcNow()
    {
        var before = DateTime.UtcNow;
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test");
        var after = DateTime.UtcNow;

        product.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void CreateProduct_WithDescription_ShouldSetDescription()
    {
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test", "A test product");

        product.Description.Should().Be("A test product");
    }

    [Fact]
    public void CreateProduct_WithCategoryId_ShouldSetCategoryId()
    {
        var categoryId = Guid.NewGuid();
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test", categoryId: categoryId);

        product.CategoryId.Should().Be(categoryId);
    }

    [Fact]
    public void CreateProduct_WithBrandId_ShouldSetBrandId()
    {
        var brandId = Guid.NewGuid();
        var product = ProductFactory.CreateProduct("Test", 49.99m, "SKU-001", "test", brandId: brandId);

        product.BrandId.Should().Be(brandId);
    }
}

public class CategoryFactoryTests
{
    [Fact]
    public void CreateCategory_ShouldReturnCategoryWithCorrectProperties()
    {
        var category = CategoryFactory.CreateCategory("Electronics");

        category.Should().NotBeNull();
        category.Name.Should().Be("Electronics");
    }

    [Fact]
    public void CreateCategory_ShouldGenerateId()
    {
        var category = CategoryFactory.CreateCategory("Electronics");

        category.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void CreateCategory_WithParent_ShouldSetParentId()
    {
        var parentId = Guid.NewGuid();
        var category = CategoryFactory.CreateCategory("Phones", parentId);

        category.ParentCategoryId.Should().Be(parentId);
    }

    [Fact]
    public void CreateCategory_WithDescription_ShouldSetDescription()
    {
        var category = CategoryFactory.CreateCategory("Electronics", description: "Electronic devices");

        category.Description.Should().Be("Electronic devices");
    }

    [Fact]
    public void CreateCategory_WithImageUrl_ShouldSetImageUrl()
    {
        var category = CategoryFactory.CreateCategory("Electronics", imageUrl: "https://example.com/electronics.jpg");

        category.ImageUrl.Should().Be("https://example.com/electronics.jpg");
    }

    [Fact]
    public void CreateCategory_WithDisplayOrder_ShouldSetDisplayOrder()
    {
        var category = CategoryFactory.CreateCategory("Electronics", displayOrder: 5);

        category.DisplayOrder.Should().Be(5);
    }

    [Fact]
    public void CreateCategory_IsActive_ShouldDefaultToTrue()
    {
        var category = CategoryFactory.CreateCategory("Electronics");

        category.IsActive.Should().BeTrue();
    }
}

public class OrderFactoryTests
{
    [Fact]
    public void CreateOrder_ShouldReturnOrderWithCorrectProperties()
    {
        var userId = Guid.NewGuid();
        var order = OrderFactory.CreateOrder(userId, "ORD-001");

        order.Should().NotBeNull();
        order.UserId.Should().Be(userId);
        order.OrderNumber.Should().Be("ORD-001");
    }

    [Fact]
    public void CreateOrder_ShouldGenerateId()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void CreateOrder_ShouldSetCreatedAtToUtcNow()
    {
        var before = DateTime.UtcNow;
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");
        var after = DateTime.UtcNow;

        order.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void CreateOrder_ShouldSetStatusToPending()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.Status.Should().Be("Pending");
    }

    [Fact]
    public void CreateOrder_ShouldInitializeEmptyItems()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001");

        order.Items.Should().NotBeNull();
        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void CreateOrder_WithNotes_ShouldSetNotes()
    {
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001", notes: "Gift wrap please");

        order.Notes.Should().Be("Gift wrap please");
    }

    [Fact]
    public void CreateOrder_WithShippingAddress_ShouldSetShippingAddress()
    {
        var addressId = Guid.NewGuid();
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001", shippingAddressId: addressId);

        order.ShippingAddressId.Should().Be(addressId);
    }

    [Fact]
    public void CreateOrder_WithBillingAddress_ShouldSetBillingAddress()
    {
        var addressId = Guid.NewGuid();
        var order = OrderFactory.CreateOrder(Guid.NewGuid(), "ORD-001", billingAddressId: addressId);

        order.BillingAddressId.Should().Be(addressId);
    }
}

public class CartFactoryTests
{
    [Fact]
    public void CreateCart_ShouldReturnCartWithCorrectProperties()
    {
        var userId = Guid.NewGuid();
        var cart = CartFactory.CreateCart(userId);

        cart.Should().NotBeNull();
        cart.UserId.Should().Be(userId);
    }

    [Fact]
    public void CreateCart_ShouldGenerateId()
    {
        var cart = CartFactory.CreateCart(Guid.NewGuid());

        cart.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void CreateCart_ShouldInitializeEmptyItems()
    {
        var cart = CartFactory.CreateCart(Guid.NewGuid());

        cart.Items.Should().NotBeNull();
        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public void CreateCart_WithSessionId_ShouldSetSessionId()
    {
        var cart = CartFactory.CreateCart(null, "session-123");

        cart.SessionId.Should().Be("session-123");
    }

    [Fact]
    public void CreateCart_ShouldSetCreatedAtToUtcNow()
    {
        var before = DateTime.UtcNow;
        var cart = CartFactory.CreateCart(Guid.NewGuid());
        var after = DateTime.UtcNow;

        cart.CreatedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void CreateCart_ShouldInitializeTotalsToZero()
    {
        var cart = CartFactory.CreateCart(Guid.NewGuid());

        cart.SubTotal.Should().Be(0);
        cart.Tax.Should().Be(0);
        cart.ShippingCost.Should().Be(0);
        cart.Total.Should().Be(0);
    }
}
