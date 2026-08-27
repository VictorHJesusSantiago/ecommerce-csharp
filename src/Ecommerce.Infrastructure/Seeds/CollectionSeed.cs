using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Infrastructure.Seeds;

public static class ProductCollectionSeed
{
    public static List<ProductCollection> GetCollections()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Best Sellers",
                Slug = "best-sellers",
                Description = "Our most popular products",
                ImageUrl = "/images/collections/best-sellers.jpg",
                SortOrder = 1,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "New Arrivals",
                Slug = "new-arrivals",
                Description = "Recently added products",
                ImageUrl = "/images/collections/new-arrivals.jpg",
                SortOrder = 2,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sale Items",
                Slug = "sale-items",
                Description = "Products on sale",
                ImageUrl = "/images/collections/sale.jpg",
                SortOrder = 3,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Featured Products",
                Slug = "featured",
                Description = "Hand-picked by our team",
                ImageUrl = "/images/collections/featured.jpg",
                SortOrder = 4,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Staff Picks",
                Slug = "staff-picks",
                Description = "Favorites from our staff",
                ImageUrl = "/images/collections/staff-picks.jpg",
                SortOrder = 5,
                IsActive = true
            }
        ];
    }
}
