using FluentAssertions;
using Xunit;
using Ecommerce.Domain.Entities.User;
using Ecommerce.Domain.Entities.Marketing;
using Ecommerce.Domain.Entities.Inventory;
using Ecommerce.Domain.Entities.Notification;
using Ecommerce.Domain.Entities.CMS;

namespace Ecommerce.ArchitectureTests;

public class UserMarketingInventoryEntityTests
{
    [Fact]
    public void ApplicationUser_ShouldHaveDefaults()
    {
        var user = new ApplicationUser();
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        user.IsActive.Should().BeTrue();
        user.IsEmailVerified.Should().BeFalse();
    }

    [Fact]
    public void Address_ShouldHaveRequiredProperties()
    {
        var address = new Address
        {
            UserId = Guid.NewGuid(),
            Label = "Home",
            FullName = "John Doe",
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            PostalCode = "10001",
            Country = "US",
            IsDefault = true
        };

        address.IsDefault.Should().BeTrue();
    }

    [Fact]
    public void Coupon_ShouldHaveRequiredProperties()
    {
        var coupon = new Coupon
        {
            Code = "SAVE20",
            Name = "20% Off",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 20,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            IsActive = true,
            UsageLimit = 100
        };

        coupon.Code.Should().Be("SAVE20");
        coupon.UsageLimit.Should().Be(100);
    }

    [Fact]
    public void Discount_ShouldTrackTiers()
    {
        var discount = new Discount
        {
            Name = "Volume Discount",
            DiscountType = DiscountType.Percentage,
            Tiers = new List<DiscountTier>
            {
                new() { MinQuantity = 1, MaxQuantity = 10, Value = 5 },
                new() { MinQuantity = 11, MaxQuantity = 50, Value = 10 },
                new() { MinQuantity = 51, MaxQuantity = null, Value = 20 }
            }
        };

        discount.Tiers.Should().HaveCount(3);
    }

    [Fact]
    public void Promotion_ShouldHaveRequiredProperties()
    {
        var promo = new Promotion
        {
            Name = "Black Friday",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 40,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(3),
            IsActive = true
        };

        promo.Name.Should().Be("Black Friday");
        promo.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Banner_ShouldHaveRequiredProperties()
    {
        var banner = new Banner
        {
            Title = "Holiday Sale",
            ImageUrl = "/images/banner.jpg",
            LinkUrl = "/products/sale",
            Position = BannerPosition.Homepage,
            SortOrder = 1,
            IsActive = true,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(14)
        };

        banner.Position.Should().Be(BannerPosition.Homepage);
    }

    [Fact]
    public void Warehouse_ShouldTrackInventory()
    {
        var warehouse = new Warehouse
        {
            Name = "Main Warehouse",
            Code = "WH-001",
            Capacity = 10000,
            IsActive = true,
            Inventory = new List<WarehouseInventory>()
        };

        warehouse.Inventory.Should().BeEmpty();
    }

    [Fact]
    public void WarehouseInventory_ShouldTrackStock()
    {
        var inventory = new WarehouseInventory
        {
            WarehouseId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 100,
            ReservedQuantity = 10,
            ReorderLevel = 25,
            MaxLevel = 500
        };

        inventory.AvailableQuantity.Should().Be(90);
    }

    [Fact]
    public void Supplier_ShouldHaveDefaults()
    {
        var supplier = new Supplier
        {
            Name = "Tech Supplies",
            IsActive = true,
            LeadTimeDays = 14,
            Products = new List<SupplierProduct>()
        };

        supplier.LeadTimeDays.Should().Be(14);
    }

    [Fact]
    public void NotificationRecord_ShouldTrackDelivery()
    {
        var notification = new NotificationRecord
        {
            UserId = Guid.NewGuid(),
            Title = "Order Shipped",
            Message = "Your order is on its way!",
            Type = "OrderUpdate",
            IsRead = false,
            IsSent = true,
            SentAt = DateTime.UtcNow
        };

        notification.IsSent.Should().BeTrue();
        notification.IsRead.Should().BeFalse();
    }

    [Fact]
    public void CmsPage_ShouldTrackContent()
    {
        var page = new CmsPage
        {
            Title = "About Us",
            Slug = "about-us",
            Content = "<p>We are a company...</p>",
            IsPublished = true,
            ViewCount = 0
        };

        page.Slug.Should().Be("about-us");
        page.ViewCount.Should().Be(0);
    }

    [Fact]
    public void SiteSetting_ShouldStoreKeyValue()
    {
        var setting = new SiteSetting
        {
            Key = "SiteName",
            Value = "ECommerce Store",
            Description = "The name of the site",
            Group = "General",
            DataType = "String"
        };

        setting.Key.Should().Be("SiteName");
    }
}
