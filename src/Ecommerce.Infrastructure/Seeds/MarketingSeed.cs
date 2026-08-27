using Ecommerce.Domain.Entities.Marketing;

namespace Ecommerce.Infrastructure.Seeds;

public static class BannerSeed
{
    public static List<Banner> GetBanners()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Summer Sale",
                Subtitle = "Up to 50% off on selected items",
                ImageUrl = "/images/banners/summer-sale.jpg",
                LinkUrl = "/products?sale=true",
                Position = BannerPosition.HomeTop,
                SortOrder = 1,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddDays(30),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "New Arrivals",
                Subtitle = "Check out our latest collection",
                ImageUrl = "/images/banners/new-arrivals.jpg",
                LinkUrl = "/products?sort=newest",
                Position = BannerPosition.HomeMiddle,
                SortOrder = 2,
                StartDate = DateTime.UtcNow.AddDays(-15),
                EndDate = DateTime.UtcNow.AddDays(45),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Free Shipping",
                Subtitle = "On orders over $50",
                ImageUrl = "/images/banners/free-shipping.jpg",
                LinkUrl = "/shipping-info",
                Position = BannerPosition.HomeBottom,
                SortOrder = 3,
                StartDate = DateTime.UtcNow.AddDays(-60),
                EndDate = DateTime.UtcNow.AddDays(90),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Electronics Deals",
                Subtitle = "Save big on tech",
                ImageUrl = "/images/banners/electronics.jpg",
                LinkUrl = "/category/electronics",
                Position = BannerPosition.CategoryTop,
                SortOrder = 1,
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(50),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Flash Sale - 24 Hours Only!",
                Subtitle = "Limited time offers",
                ImageUrl = "/images/banners/flash-sale.jpg",
                LinkUrl = "/flash-sale",
                Position = BannerPosition.HomeTop,
                SortOrder = 0,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                IsActive = true
            }
        ];
    }
}

public static class PromotionSeed
{
    public static List<Promotion> GetPromotions()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Buy 2 Get 1 Free",
                Description = "Buy any 2 items and get the 3rd free (lowest priced)",
                DiscountType = DiscountType.BuyOneGetOne,
                DiscountValue = 100,
                MinimumQuantity = 3,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddDays(23),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Clearance Sale",
                Description = "Clearance items at reduced prices",
                DiscountType = DiscountType.Percentage,
                DiscountValue = 40,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddDays(30),
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "New Customer Bonus",
                Description = "Extra 15% off for new customers",
                DiscountType = DiscountType.Percentage,
                DiscountValue = 15,
                MinimumOrderAmount = 30,
                UsageLimitPerCustomer = 1,
                StartDate = DateTime.UtcNow.AddDays(-60),
                EndDate = DateTime.UtcNow.AddDays(180),
                IsActive = true
            }
        ];
    }
}
