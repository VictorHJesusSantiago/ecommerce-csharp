using Ecommerce.Domain.Entities.Cms;

namespace Ecommerce.Infrastructure.Seeds;

public static class SiteSettingSeed
{
    public static List<SiteSetting> GetSettings()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Key = "SiteName",
                Value = "E-Commerce Store",
                Group = "General",
                Description = "The name of the website"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "SiteDescription",
                Value = "Your one-stop shop for all your needs",
                Group = "General",
                Description = "Site meta description"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "ContactEmail",
                Value = "support@ecommerce.com",
                Group = "Contact",
                Description = "Customer support email"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "ContactPhone",
                Value = "+1-800-555-0199",
                Group = "Contact",
                Description = "Customer support phone"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "CurrencyCode",
                Value = "USD",
                Group = "Store",
                Description = "Default currency code"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "TaxRate",
                Value = "8.0",
                Group = "Store",
                Description = "Default tax rate percentage"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "FreeShippingThreshold",
                Value = "50.00",
                Group = "Shipping",
                Description = "Minimum order amount for free shipping"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "DefaultShippingRate",
                Value = "9.99",
                Group = "Shipping",
                Description = "Default shipping cost"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "AllowGuestCheckout",
                Value = "true",
                Group = "Checkout",
                Description = "Allow guest checkout"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "MaxCartItems",
                Value = "50",
                Group = "Cart",
                Description = "Maximum items per cart"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "ReviewsEnabled",
                Value = "true",
                Group = "Products",
                Description = "Enable product reviews"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Key = "MinimumPasswordLength",
                Value = "8",
                Group = "Security",
                Description = "Minimum password length"
            }
        ];
    }
}

public static class NavigationMenuSeed
{
    public static List<NavigationMenu> GetMenus()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Main Navigation",
                Position = "Header",
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Footer Navigation",
                Position = "Footer",
                IsActive = true
            }
        ];
    }
}
