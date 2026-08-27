using Ecommerce.Domain.Entities.Catalog;

namespace Ecommerce.Domain.Factories;

public static class ProductFactory
{
    public static Product CreateElectronics(
        string name, string sku, decimal price, Guid categoryId,
        decimal weight, string? brand = null)
    {
        var product = Product.Create(name, sku, price, categoryId,
            productType: Enums.ProductType.Physical,
            isTaxable: true,
            isShippingRequired: true,
            weight: weight,
            weightUnit: "kg",
            lowStockThreshold: 10);

        if (!string.IsNullOrEmpty(brand))
            product.Update(name, tags: brand);

        return product;
    }

    public static Product CreateDigital(
        string name, string sku, decimal price, Guid categoryId,
        string? downloadUrl = null, int? maxDownloads = null)
    {
        return Product.Create(name, sku, price, categoryId,
            productType: Enums.ProductType.Digital,
            isTaxable: false,
            isShippingRequired: false);
    }

    public static Product CreateBundle(
        string name, string sku, decimal price, Guid categoryId,
        IEnumerable<Guid>? includedProductIds = null)
    {
        return Product.Create(name, sku, price, categoryId,
            productType: Enums.ProductType.Bundle,
            isTaxable: true,
            isShippingRequired: true,
            tags: string.Join(",", includedProductIds ?? Enumerable.Empty<Guid>()));
    }

    public static Product CreateService(
        string name, string sku, decimal price, Guid categoryId)
    {
        return Product.Create(name, sku, price, categoryId,
            productType: Enums.ProductType.Service,
            isTaxable: true,
            isShippingRequired: false);
    }

    public static ProductVariant CreateVariant(
        Guid productId, string name, string sku, decimal price,
        string? size = null, string? color = null, string? material = null,
        int stockQuantity = 0, bool isDefault = false)
    {
        return ProductVariant.Create(
            name, sku, price, productId,
            stockQuantity: stockQuantity,
            isDefault: isDefault,
            option1: size,
            option2: color,
            option3: material);
    }

    public static ProductImage CreateImage(
        Guid productId, string url, bool isPrimary = false,
        string? altText = null, int displayOrder = 0)
    {
        return ProductImage.Create(url, productId,
            altText: altText ?? altText,
            displayOrder: displayOrder,
            isPrimary: isPrimary,
            imageType: isPrimary ? Enums.ImageType.Original : Enums.ImageType.Gallery);
    }

    public static Product WithVariants(this Product product, params ProductVariant[] variants)
    {
        foreach (var variant in variants)
            product.AddVariant(variant);
        return product;
    }

    public static Product WithImages(this Product product, params ProductImage[] images)
    {
        foreach (var image in images)
            product.AddImage(image);
        return product;
    }
}

public static class CategoryFactory
{
    public static Category CreateElectronicsCategory()
    {
        return Category.Create("Electronics", "electronics",
            "Electronic devices and accessories");
    }

    public static Category CreateClothingCategory()
    {
        return Category.Create("Clothing", "clothing",
            "Fashion and apparel");
    }

    public static Category CreateHomeGardenCategory()
    {
        return Category.Create("Home & Garden", "home-garden",
            "Home and garden products");
    }

    public static Category CreateWithSubcategories(string name, params string[] subcategoryNames)
    {
        var parent = Category.Create(name, Slug.Create(name).Value);
        var displayOrder = 0;
        foreach (var subName in subcategoryNames)
        {
            var sub = Category.Create(subName, Slug.Create(subName).Value, parentCategoryId: parent.Id, displayOrder: displayOrder++);
            parent.AddSubcategory(sub);
        }
        return parent;
    }
}

public static class OrderFactory
{
    public static Entities.Ordering.Order CreateOrder(
        Guid customerId, string currency = "USD")
    {
        return Entities.Ordering.Order.Create(
            customerId,
            Entities.Ordering.Order.GenerateOrderNumber(),
            currency);
    }

    public static Entities.Ordering.OrderItem CreateOrderItem(
        Guid orderId, Guid productId, string productName, string productSlug,
        string sku, int quantity, decimal unitPrice, string? imageUrl = null)
    {
        return Entities.Ordering.OrderItem.Create(
            orderId, productId, productName, productSlug, sku,
            quantity, unitPrice, productImageUrl: imageUrl);
    }
}

public static class CartFactory
{
    public static Entities.Cart.ShoppingCart CreateCartForUser(Guid userId, string currency = "USD")
    {
        return Entities.Cart.ShoppingCart.CreateForUser(userId, currency);
    }

    public static Entities.Cart.ShoppingCart CreateCartForSession(string sessionId, string currency = "USD")
    {
        return Entities.Cart.ShoppingCart.CreateForSession(sessionId, currency);
    }
}
