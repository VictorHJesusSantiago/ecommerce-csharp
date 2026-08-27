using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Infrastructure.Seeds;

public static class CategorySeed
{
    public static List<Category> GetCategories()
    {
        return
        [
            new() { Id = Guid.NewGuid(), Name = "Electronics", Slug = "electronics", Description = "Electronic devices and accessories", SortOrder = 1, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Clothing", Slug = "clothing", Description = "Fashion and apparel", SortOrder = 2, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Home & Garden", Slug = "home-garden", Description = "Home decor and garden supplies", SortOrder = 3, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Sports", Slug = "sports", Description = "Sports equipment and gear", SortOrder = 4, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Books", Slug = "books", Description = "Books and publications", SortOrder = 5, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Toys & Games", Slug = "toys-games", Description = "Toys and board games", SortOrder = 6, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Health & Beauty", Slug = "health-beauty", Description = "Health and beauty products", SortOrder = 7, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Automotive", Slug = "automotive", Description = "Automotive parts and accessories", SortOrder = 8, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Pet Supplies", Slug = "pet-supplies", Description = "Pet food and accessories", SortOrder = 9, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Office", Slug = "office", Description = "Office supplies and equipment", SortOrder = 10, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Musical Instruments", Slug = "musical-instruments", Description = "Instruments and accessories", SortOrder = 11, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Baby", Slug = "baby", Description = "Baby products and essentials", SortOrder = 12, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Jewelry", Slug = "jewelry", Description = "Fine jewelry and watches", SortOrder = 13, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Food & Beverages", Slug = "food-beverages", Description = "Gourmet food and drinks", SortOrder = 14, IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "Outdoor", Slug = "outdoor", Description = "Camping and outdoor gear", SortOrder = 15, IsActive = true }
        ];
    }
}

public static class BrandSeed
{
    public static List<Brand> GetBrands()
    {
        return
        [
            new() { Id = Guid.NewGuid(), Name = "TechPro", Slug = "techpro", Description = "Premium electronics", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "FashionHub", Slug = "fashionhub", Description = "Trendy clothing", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "HomeStyle", Slug = "homestyle", Description = "Home furnishings", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "SportMax", Slug = "sportmax", Description = "Sports equipment", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "BookWorld", Slug = "bookworld", Description = "Books and media", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "PlayTime", Slug = "playtime", Description = "Toys and games", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "PureLife", Slug = "purelife", Description = "Health products", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "AutoGear", Slug = "autogear", Description = "Auto accessories", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "PetJoy", Slug = "petjoy", Description = "Pet products", IsActive = true },
            new() { Id = Guid.NewGuid(), Name = "OfficePro", Slug = "officepro", Description = "Office supplies", IsActive = true }
        ];
    }
}

public static class ProductSeed
{
    public static List<Product> GetProducts(List<Guid> categoryIds)
    {
        var products = new List<Product>();
        var random = new Random(42);

        var productNames = new[]
        {
            "Wireless Bluetooth Headphones", "USB-C Charging Cable", "Laptop Stand Aluminum",
            "Mechanical Gaming Keyboard", "Wireless Mouse Ergonomic", "4K Webcam HD",
            "Portable SSD 1TB", "Smart Watch Fitness", "Bluetooth Speaker Waterproof",
            "LED Desk Lamp Dimmable", "Cotton T-Shirt Classic", "Denim Jeans Slim Fit",
            "Winter Jacket Waterproof", "Running Shoes Sport", "Yoga Mat Non-Slip",
            "Stainless Steel Water Bottle", "Indoor Plant Pot Ceramic", "LED String Lights",
            "Garden Tool Set", "BBQ Grill Portable", "Dumbbell Set Adjustable",
            "Resistance Bands Set", "Jump Rope Speed", "Tennis Racket Professional",
            "Cookbook Italian Recipes", "Programming Book C#", "Journal Leather Bound",
            "Board Game Strategy", "Puzzle 1000 Pieces", "RC Car Off-Road",
            "Board Game Classic", "Action Figure Collectible", "Building Blocks Set",
            "Vitamin C Supplement", "Essential Oil Diffuser", "Face Moisturizer SPF30",
            "Car Phone Mount", "Dash Cam 4K", "Car Air Freshener",
            "Dog Bed Orthopedic", "Cat Scratching Post", "Bird Feeder Wooden",
            "Desk Organizer Wood", "Ergonomic Chair Mesh", "Monitor Arm Dual",
            "Guitar Acoustic Starter", "Ukulele Concert", "Headphone Stand Desktop",
            "Baby Stroller Lightweight", "Diaper Bag Backpack", "Baby Monitor WiFi",
            "Gold Necklace Pendant", "Silver Bracelet Tennis", "Diamond Earrings Stud",
            "Organic Coffee Beans", "Green Tea Matcha", "Dark Chocolate Truffles",
            "Camping Tent 4-Person", "Sleeping Bag Cold Weather", "Hiking Backpack 50L",
            "Wireless Charger Pad", "Power Bank 20000mAh", "Screen Protector Glass",
            "Phone Case Leather", "Tablet Stand Adjustable", "Cable Management Box",
            "Wall Art Canvas Print", "Throw Pillow Velvet", "Scented Candle Soy",
            "Coffee Maker Drip", "Blender Personal Size", "Toaster Oven Compact",
            "Air Purifier HEPA", "Vacuum Cordless Stick", "Robot Vacuum Smart",
            "Air Fryer Large", "Instant Pot Pressure", "Juicer Cold Press",
            "Espresso Machine Semi", "Kettle Electric Gooseneck", "Milk Frother Electric"
        };

        for (int i = 0; i < productNames.Length; i++)
        {
            var price = Math.Round((decimal)(random.NextDouble() * 200 + 5), 2);
            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = productNames[i],
                Slug = productNames[i].ToLower().Replace(" ", "-").Replace("'", ""),
                Description = $"High quality {productNames[i].ToLower()} for everyday use. Made with premium materials.",
                Price = price,
                CompareAtPrice = price * 1.2m,
                Sku = $"PROD-{i + 1:D4}",
                Barcode = $"123456789{i:D3}",
                StockQuantity = random.Next(0, 500),
                LowStockThreshold = 10,
                Weight = random.Next(100, 5000),
                CategoryId = categoryIds[random.Next(categoryIds.Count)],
                IsActive = true,
                IsFeatured = random.Next(3) == 0,
                IsDigital = false,
                AverageRating = Math.Round((double)(decimal)(decimal)(random.Next(100, 500) / 100.0m), 1),
                ReviewCount = random.Next(0, 500),
                ViewCount = random.Next(0, 10000)
            });
        }

        return products;
    }
}
