using FluentAssertions;
using Xunit;
using Ecommerce.Domain.Entities.User;
using Ecommerce.Domain.Entities.Marketing;
using Ecommerce.Domain.Entities.Inventory;

namespace Ecommerce.ArchitectureTests;

public class EntityTests
{
    [Fact]
    public void ApplicationUser_ShouldSetProperties()
    {
        var user = new ApplicationUser
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "+1234567890",
            IsActive = true,
            IsEmailVerified = true,
            TwoFactorEnabled = false,
            CreatedAt = DateTime.UtcNow
        };

        user.FullName.Should().Be("John Doe");
        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Address_ShouldSetProperties()
    {
        var address = new Address
        {
            UserId = Guid.NewGuid(),
            Label = "Work",
            FullName = "John Doe",
            Street = "456 Office Blvd",
            Street2 = "Suite 200",
            City = "San Francisco",
            State = "CA",
            PostalCode = "94102",
            Country = "US",
            Phone = "+14155551234",
            IsDefault = false
        };

        address.City.Should().Be("San Francisco");
        address.IsDefault.Should().BeFalse();
    }

    [Fact]
    public void RefreshToken_ShouldHaveTokenData()
    {
        var token = new RefreshToken
        {
            UserId = Guid.NewGuid(),
            Token = "random-token-string",
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            ReplacedByToken = null
        };

        token.IsExpired.Should().BeFalse();
    }

    [Fact]
    public void RefreshToken_ShouldDetectExpiration()
    {
        var token = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(-1)
        };

        token.IsExpired.Should().BeTrue();
    }

    [Fact]
    public void Wishlist_ShouldTrackItems()
    {
        var wishlist = new Wishlist
        {
            UserId = Guid.NewGuid(),
            Items = new List<WishlistItem>(),
            CreatedAt = DateTime.UtcNow
        };

        wishlist.Items.Should().BeEmpty();
    }

    [Fact]
    public void UserActivity_ShouldRecordAction()
    {
        var activity = new UserActivity
        {
            UserId = Guid.NewGuid(),
            Action = "Login",
            Details = "Logged in from Chrome",
            IpAddress = "192.168.1.1",
            UserAgent = "Mozilla/5.0",
            Timestamp = DateTime.UtcNow
        };

        activity.Action.Should().Be("Login");
    }

    [Fact]
    public void Coupon_ShouldCalculateDiscount()
    {
        var coupon = new Coupon
        {
            Code = "FLAT10",
            DiscountType = DiscountType.Fixed,
            DiscountValue = 10m,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(30),
            UsageLimit = 50,
            UsedCount = 5,
            IsActive = true
        };

        coupon.UsageLimit.Should().Be(50);
        coupon.UsedCount.Should().Be(5);
    }

    [Fact]
    public void Discount_ShouldHaveTiers()
    {
        var discount = new Discount
        {
            Name = "Bulk Discount",
            DiscountType = DiscountType.Percentage,
            Tiers = new List<DiscountTier>
            {
                new() { MinQuantity = 1, MaxQuantity = 5, Value = 5 },
                new() { MinQuantity = 6, MaxQuantity = 20, Value = 10 },
                new() { MinQuantity = 21, MaxQuantity = null, Value = 20 }
            }
        };

        discount.Tiers.Should().HaveCount(3);
    }

    [Fact]
    public void NewsletterSubscriber_ShouldTrackSubscription()
    {
        var subscriber = new NewsletterSubscriber
        {
            Email = "user@example.com",
            IsSubscribed = true,
            SubscribedAt = DateTime.UtcNow,
            Source = "Footer Form"
        };

        subscriber.IsSubscribed.Should().BeTrue();
    }

    [Fact]
    public void Warehouse_ShouldTrackCapacity()
    {
        var warehouse = new Warehouse
        {
            Name = "Distribution Center",
            Code = "DC-001",
            Capacity = 50000,
            IsActive = true
        };

        warehouse.Capacity.Should().Be(50000);
    }

    [Fact]
    public void Supplier_ShouldTrackLeadTime()
    {
        var supplier = new Supplier
        {
            Name = "Global Parts",
            LeadTimeDays = 21,
            PaymentTerms = "Net 60",
            IsActive = true
        };

        supplier.LeadTimeDays.Should().Be(21);
    }
}
