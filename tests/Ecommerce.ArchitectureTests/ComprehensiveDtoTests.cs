using FluentAssertions;
using Xunit;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.DTOs.Order;
using Ecommerce.Application.DTOs.User;
using Ecommerce.Application.DTOs.Cart;

namespace Ecommerce.ArchitectureTests;

public class ComprehensiveDtoTests
{
    [Fact]
    public void ProductDto_AllProperties()
    {
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Slug = "test",
            Description = "Desc",
            Price = 10m,
            OriginalPrice = 15m,
            Sku = "SKU-1",
            Barcode = "123",
            Weight = 1m,
            Length = 10m,
            Width = 5m,
            Height = 3m,
            IsFeatured = true,
            IsActive = true,
            CategoryId = Guid.NewGuid(),
            BrandId = Guid.NewGuid(),
            StockQuantity = 100,
            LowStockThreshold = 10,
            AllowBackorder = false,
            MaxOrderQuantity = 10,
            TaxRate = 0.08m,
            MetaTitle = "Meta",
            MetaDescription = "Desc",
            ThumbnailUrl = "/img.jpg",
            CategoryName = "Cat",
            BrandName = "Brand",
            AverageRating = 4.5m,
            ReviewCount = 10,
            VariantCount = 2,
            ImageCount = 3,
            IsInStock = true,
            IsOnSale = true,
            DiscountPercentage = 20,
            Tags = new List<string> { "a" },
            Images = new List<ProductImageDto>(),
            Variants = new List<ProductVariantDto>(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Name.Should().Be("Test");
        dto.Tags.Should().HaveCount(1);
    }

    [Fact]
    public void OrderDto_AllProperties()
    {
        var dto = new OrderDto
        {
            Id = Guid.NewGuid(),
            OrderNumber = "ORD-001",
            UserId = Guid.NewGuid(),
            CustomerName = "John",
            CustomerEmail = "john@test.com",
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Processing,
            PaymentStatus = "Paid",
            SubTotal = 100m,
            ShippingCost = 10m,
            TaxAmount = 8m,
            DiscountAmount = 5m,
            TotalAmount = 113m,
            ShippingAddress = new Application.DTOs.Common.AddressDto { Street = "123", City = "NY", State = "NY", PostalCode = "10001", Country = "US" },
            BillingAddress = new Application.DTOs.Common.AddressDto { Street = "123", City = "NY", State = "NY", PostalCode = "10001", Country = "US" },
            Items = new List<OrderItemDto>(),
            TrackingNumber = "T123",
            Carrier = "UPS",
            CouponCode = "SAVE",
            Notes = "Note",
            PaymentMethod = "Credit Card",
            ShippingMethod = "Express",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.TotalAmount.Should().Be(113m);
    }

    [Fact]
    public void UserDto_AllProperties()
    {
        var dto = new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@test.com",
            PhoneNumber = "+1234",
            ProfileImageUrl = "/img.jpg",
            IsEmailVerified = true,
            IsPhoneVerified = false,
            TwoFactorEnabled = false,
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
            Roles = new List<string> { "Admin" },
            AddressCount = 2,
            OrderCount = 5,
            TotalSpent = 500m,
            IsActive = true
        };

        dto.FullName.Should().Be("Jane Doe");
    }

    [Fact]
    public void CartDto_AllProperties()
    {
        var dto = new CartDto
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Items = new List<CartItemDto>
            {
                new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), ProductName = "Widget", Quantity = 2, UnitPrice = 10m, TotalPrice = 20m, ImageUrl = "/w.jpg", IsAvailable = true }
            },
            CouponCode = "CODE",
            SubTotal = 20m,
            Discount = 2m,
            Total = 18m,
            ItemCount = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dto.Items.Should().HaveCount(1);
    }

    [Fact]
    public void CartItemDto_AllProperties()
    {
        var dto = new CartItemDto
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductVariantId = Guid.NewGuid(),
            ProductName = "Widget",
            ProductSlug = "widget",
            ImageUrl = "/widget.jpg",
            UnitPrice = 29.99m,
            Quantity = 3,
            TotalPrice = 89.97m,
            IsAvailable = true,
            StockQuantity = 50,
            MaxQuantity = 10,
            AddedAt = DateTime.UtcNow
        };

        dto.TotalPrice.Should().Be(89.97m);
    }
}
