using Xunit;
using FluentAssertions;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.User;

namespace Ecommerce.UnitTests.Domain;

public class CartTests
{
    [Fact]
    public void Cart_AddItem_ShouldAddItemToCart()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var item = new CartItem
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 49.99m,
            Quantity = 1
        };

        cart.Items.Add(item);

        cart.Items.Should().HaveCount(1);
        cart.Items.First().ProductName.Should().Be("Test Product");
    }

    [Fact]
    public void Cart_RemoveItem_ShouldRemoveItemFromCart()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var item = new CartItem
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 49.99m,
            Quantity = 1
        };
        cart.Items.Add(item);

        cart.Items.Remove(item);

        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public void Cart_UpdateQuantity_ShouldUpdateItemQuantity()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var item = new CartItem
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Test Product",
            UnitPrice = 49.99m,
            Quantity = 1
        };
        cart.Items.Add(item);

        item.Quantity = 5;

        item.Quantity.Should().Be(5);
    }

    [Fact]
    public void Cart_CalculateSubtotal_ShouldCalculateCorrectly()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        cart.Items.Add(new CartItem { UnitPrice = 49.99m, Quantity = 2 });
        cart.Items.Add(new CartItem { UnitPrice = 29.99m, Quantity = 1 });

        var subtotal = cart.Items.Sum(i => i.UnitPrice * i.Quantity);

        subtotal.Should().Be(129.97m);
    }

    [Fact]
    public void Cart_Clear_ShouldRemoveAllItems()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        cart.Items.Add(new CartItem { UnitPrice = 49.99m, Quantity = 1 });
        cart.Items.Add(new CartItem { UnitPrice = 29.99m, Quantity = 1 });

        cart.Items.Clear();

        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public void Cart_SetCoupon_ShouldSetCouponCode()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };

        cart.CouponCode = "SAVE20";

        cart.CouponCode.Should().Be("SAVE20");
    }

    [Fact]
    public void Cart_SetDiscount_ShouldSetDiscount()
    {
        var cart = new ShoppingCart { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };

        cart.Discount = 10.00m;

        cart.Discount.Should().Be(10.00m);
    }
}

public class WishlistTests
{
    [Fact]
    public void Wishlist_AddItem_ShouldAddItemToWishlist()
    {
        var wishlist = new Wishlist { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var productId = Guid.NewGuid();

        wishlist.ProductIds.Add(productId);

        wishlist.ProductIds.Should().HaveCount(1);
        wishlist.ProductIds.Should().Contain(productId);
    }

    [Fact]
    public void Wishlist_RemoveItem_ShouldRemoveItemFromWishlist()
    {
        var wishlist = new Wishlist { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var productId = Guid.NewGuid();
        wishlist.ProductIds.Add(productId);

        wishlist.ProductIds.Remove(productId);

        wishlist.ProductIds.Should().BeEmpty();
    }

    [Fact]
    public void Wishlist_Contains_ShouldReturnTrueForExistingProduct()
    {
        var wishlist = new Wishlist { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        var productId = Guid.NewGuid();
        wishlist.ProductIds.Add(productId);

        wishlist.ProductIds.Should().Contain(productId);
    }

    [Fact]
    public void Wishlist_Contains_ShouldReturnFalseForNonExistingProduct()
    {
        var wishlist = new Wishlist { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };

        wishlist.ProductIds.Should().NotContain(Guid.NewGuid());
    }

    [Fact]
    public void Wishlist_Clear_ShouldRemoveAllItems()
    {
        var wishlist = new Wishlist { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
        wishlist.ProductIds.Add(Guid.NewGuid());
        wishlist.ProductIds.Add(Guid.NewGuid());

        wishlist.ProductIds.Clear();

        wishlist.ProductIds.Should().BeEmpty();
    }
}

public class UserTests
{
    [Fact]
    public void User_Create_ShouldSetProperties()
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "johndoe",
            Email = "john@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        user.UserName.Should().Be("johndoe");
        user.Email.Should().Be("john@example.com");
        user.FirstName.Should().Be("John");
        user.LastName.Should().Be("Doe");
    }

    [Fact]
    public void User_FullName_ShouldReturnFullName()
    {
        var user = new ApplicationUser
        {
            FirstName = "John",
            LastName = "Doe"
        };

        var fullName = $"{user.FirstName} {user.LastName}";

        fullName.Should().Be("John Doe");
    }

    [Fact]
    public void User_IsActive_ShouldDefaultToTrue()
    {
        var user = new ApplicationUser();

        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void User_SetPhoneNumber_ShouldSetPhoneNumber()
    {
        var user = new ApplicationUser();

        user.PhoneNumber = "+1234567890";

        user.PhoneNumber.Should().Be("+1234567890");
    }

    [Fact]
    public void User_SetAvatarUrl_ShouldSetAvatarUrl()
    {
        var user = new ApplicationUser();

        user.AvatarUrl = "https://example.com/avatar.jpg";

        user.AvatarUrl.Should().Be("https://example.com/avatar.jpg");
    }
}

public class AddressEntityTests
{
    [Fact]
    public void Address_Create_ShouldSetProperties()
    {
        var address = new Ecommerce.Domain.Entities.User.Address
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States",
            IsDefault = true
        };

        address.Street.Should().Be("123 Main St");
        address.City.Should().Be("New York");
        address.State.Should().Be("NY");
        address.PostalCode.Should().Be("10001");
        address.Country.Should().Be("United States");
        address.IsDefault.Should().BeTrue();
    }

    [Fact]
    public void Address_SetLabel_ShouldSetLabel()
    {
        var address = new Ecommerce.Domain.Entities.User.Address();

        address.Label = "Home";

        address.Label.Should().Be("Home");
    }

    [Fact]
    public void Address_SetPhone_ShouldSetPhone()
    {
        var address = new Ecommerce.Domain.Entities.User.Address();

        address.Phone = "+1234567890";

        address.Phone.Should().Be("+1234567890");
    }

    [Fact]
    public void Address_FullAddress_ShouldFormatCorrectly()
    {
        var address = new Ecommerce.Domain.Entities.User.Address
        {
            Street = "123 Main St",
            Street2 = "Apt 4B",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "United States"
        };

        var fullAddress = $"{address.Street}, {address.Street2}, {address.City}, {address.State} {address.PostalCode}, {address.Country}";

        fullAddress.Should().Be("123 Main St, Apt 4B, New York, NY 10001, United States");
    }
}

public class PaymentRecordTests
{
    [Fact]
    public void PaymentRecord_Create_ShouldSetProperties()
    {
        var payment = new PaymentRecord
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Amount = 99.99m,
            Currency = "USD",
            PaymentMethod = "CreditCard",
            Status = "Pending"
        };

        payment.Amount.Should().Be(99.99m);
        payment.Currency.Should().Be("USD");
        payment.PaymentMethod.Should().Be("CreditCard");
        payment.Status.Should().Be("Pending");
    }

    [Fact]
    public void PaymentRecord_SetTransactionId_ShouldSetTransactionId()
    {
        var payment = new PaymentRecord();

        payment.TransactionId = "txn_123456";

        payment.TransactionId.Should().Be("txn_123456");
    }

    [Fact]
    public void PaymentRecord_SetProcessedAt_ShouldSetProcessedDate()
    {
        var payment = new PaymentRecord();
        var processedAt = DateTime.UtcNow;

        payment.ProcessedAt = processedAt;

        payment.ProcessedAt.Should().Be(processedAt);
    }
}

public class ReviewTests
{
    [Fact]
    public void Review_Create_ShouldSetProperties()
    {
        var review = new ProductReview
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Rating = 5,
            Title = "Great product!",
            Comment = "I love this product. Highly recommended!",
            IsVerifiedPurchase = true,
            IsApproved = true
        };

        review.Rating.Should().Be(5);
        review.Title.Should().Be("Great product!");
        review.Comment.Should().Be("I love this product. Highly recommended!");
        review.IsVerifiedPurchase.Should().BeTrue();
        review.IsApproved.Should().BeTrue();
    }

    [Fact]
    public void Review_SetRating_ShouldSetRating()
    {
        var review = new ProductReview();

        review.Rating = 4;

        review.Rating.Should().Be(4);
    }

    [Fact]
    public void Review_SetTitle_ShouldSetTitle()
    {
        var review = new ProductReview();

        review.Title = "Good quality";

        review.Title.Should().Be("Good quality");
    }

    [Fact]
    public void Review_SetComment_ShouldSetComment()
    {
        var review = new ProductReview();

        review.Comment = "Very satisfied with the purchase.";

        review.Comment.Should().Be("Very satisfied with the purchase.");
    }

    [Fact]
    public void Review_SetHelpfulCount_ShouldSetHelpfulCount()
    {
        var review = new ProductReview();

        review.HelpfulCount = 10;

        review.HelpfulCount.Should().Be(10);
    }

    [Fact]
    public void Review_SetNotHelpfulCount_ShouldSetNotHelpfulCount()
    {
        var review = new ProductReview();

        review.NotHelpfulCount = 2;

        review.NotHelpfulCount.Should().Be(2);
    }
}

public class BrandTests
{
    [Fact]
    public void Brand_Create_ShouldSetProperties()
    {
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = "Nike",
            Description = "Just Do It",
            LogoUrl = "https://example.com/nike.png",
            Website = "https://nike.com",
            IsActive = true
        };

        brand.Name.Should().Be("Nike");
        brand.Description.Should().Be("Just Do It");
        brand.LogoUrl.Should().Be("https://example.com/nike.png");
        brand.Website.Should().Be("https://nike.com");
        brand.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Brand_SetSlug_ShouldSetSlug()
    {
        var brand = new Brand();

        brand.Slug = "nike";

        brand.Slug.Should().Be("nike");
    }
}

public class WarehouseTests
{
    [Fact]
    public void Warehouse_Create_ShouldSetProperties()
    {
        var warehouse = new Ecommerce.Domain.Entities.Inventory.Warehouse
        {
            Id = Guid.NewGuid(),
            Name = "Main Warehouse",
            Code = "WH-001",
            City = "New York",
            Country = "United States",
            IsActive = true
        };

        warehouse.Name.Should().Be("Main Warehouse");
        warehouse.Code.Should().Be("WH-001");
        warehouse.City.Should().Be("New York");
        warehouse.Country.Should().Be("United States");
        warehouse.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Warehouse_SetAddress_ShouldSetAddress()
    {
        var warehouse = new Ecommerce.Domain.Entities.Inventory.Warehouse();

        warehouse.Street = "123 Warehouse St";
        warehouse.State = "NY";
        warehouse.PostalCode = "10001";

        warehouse.Street.Should().Be("123 Warehouse St");
        warehouse.State.Should().Be("NY");
        warehouse.PostalCode.Should().Be("10001");
    }
}

public class SupplierTests
{
    [Fact]
    public void Supplier_Create_ShouldSetProperties()
    {
        var supplier = new Ecommerce.Domain.Entities.Inventory.Supplier
        {
            Id = Guid.NewGuid(),
            Name = "Tech Supplies Inc",
            Email = "orders@techsupplies.com",
            Phone = "+1234567890",
            IsActive = true
        };

        supplier.Name.Should().Be("Tech Supplies Inc");
        supplier.Email.Should().Be("orders@techsupplies.com");
        supplier.Phone.Should().Be("+1234567890");
        supplier.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Supplier_SetContactPerson_ShouldSetContactPerson()
    {
        var supplier = new Ecommerce.Domain.Entities.Inventory.Supplier();

        supplier.ContactPerson = "John Smith";

        supplier.ContactPerson.Should().Be("John Smith");
    }
}
