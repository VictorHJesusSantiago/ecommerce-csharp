using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Aggregates;

public class ProductAggregate
{
    public Product Product { get; private set; }
    public Category? Category { get; private set; }
    public Category? SubCategory { get; private set; }
    public Brand? Brand { get; private set; }
    public IReadOnlyList<ProductVariant> Variants => Product.Variants.ToList();
    public IReadOnlyList<ProductImage> Images => Product.Images.ToList();
    public IReadOnlyList<ProductReview> Reviews { get; private set; }
    public double AverageRating { get; private set; }
    public int ReviewCount { get; private set; }
    public int TotalStock { get; private set; }
    public bool IsInStock { get; private set; }
    public decimal LowestPrice { get; private set; }
    public decimal HighestPrice { get; private set; }
    public bool HasVariants => Variants.Count > 0;
    public bool OnSale => Product.CompareAtPrice.HasValue && Product.CompareAtPrice > Product.Price;
    public decimal? DiscountPercentage => OnSale
        ? Math.Round((1 - Product.Price / Product.CompareAtPrice!.Value) * 100, 1)
        : null;

    public ProductAggregate(Product product)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Reviews = new List<ProductReview>();
    }

    public void SetCategory(Category category)
    {
        Category = category;
    }

    public void SetSubCategory(Category subCategory)
    {
        SubCategory = subCategory;
    }

    public void SetBrand(Brand brand)
    {
        Brand = brand;
    }

    public void SetReviews(IReadOnlyList<ProductReview> reviews)
    {
        Reviews = reviews;
        ReviewCount = reviews.Count;
        AverageRating = reviews.Count > 0
            ? Math.Round(reviews.Average(r => r.Rating), 2)
            : 0;
    }

    public void SetStock(int totalStock)
    {
        TotalStock = totalStock;
        IsInStock = totalStock > 0;
    }

    public void SetPriceRange(decimal lowest, decimal highest)
    {
        LowestPrice = lowest;
        HighestPrice = highest;
    }

    public bool IsAvailable()
    {
        return Product.IsAvailableForPurchase() && IsInStock;
    }

    public ProductVariant? GetVariantByOptions(string? option1 = null, string? option2 = null, string? option3 = null)
    {
        return Variants.FirstOrDefault(v =>
            (option1 == null || v.Option1 == option1) &&
            (option2 == null || v.Option2 == option2) &&
            (option3 == null || v.Option3 == option3));
    }

    public IReadOnlyList<string> GetOptionValues(string optionNumber)
    {
        return optionNumber switch
        {
            "1" => Variants.Where(v => v.Option1 != null).Select(v => v.Option1!).Distinct().ToList(),
            "2" => Variants.Where(v => v.Option2 != null).Select(v => v.Option2!).Distinct().ToList(),
            "3" => Variants.Where(v => v.Option3 != null).Select(v => v.Option3!).Distinct().ToList(),
            _ => new List<string>()
        };
    }

    public IReadOnlyList<ProductReview> GetApprovedReviews()
    {
        return Reviews.Where(r => r.Status == ReviewStatus.Approved).ToList();
    }

    public IReadOnlyDictionary<int, int> GetRatingDistribution()
    {
        var distribution = new Dictionary<int, int> { [1] = 0, [2] = 0, [3] = 0, [4] = 0, [5] = 0 };
        foreach (var review in Reviews.Where(r => r.Status == ReviewStatus.Approved))
        {
            if (distribution.ContainsKey(review.Rating))
                distribution[review.Rating]++;
        }
        return distribution;
    }
}

public class OrderAggregate
{
    public Entities.Ordering.Order Order { get; private set; }
    public IReadOnlyList<Entities.Ordering.OrderItem> Items => Order.Items.ToList();
    public IReadOnlyList<Entities.Ordering.OrderHistory> History => Order.History.ToList();
    public Entities.Identity.UserAddress? ShippingAddress { get; private set; }
    public Entities.Identity.UserAddress? BillingAddress { get; private set; }
    public IReadOnlyList<Entities.Payment.PaymentRecord> Payments { get; private set; }
    public IReadOnlyList<Entities.Shipping.Shipment> Shipments { get; private set; }
    public IReadOnlyList<Entities.Payment.RefundRecord> Refunds { get; private set; }

    public OrderAggregate(Entities.Ordering.Order order)
    {
        Order = order ?? throw new ArgumentNullException(nameof(order));
        Payments = new List<Entities.Payment.PaymentRecord>();
        Shipments = new List<Entities.Shipping.Shipment>();
        Refunds = new List<Entities.Payment.RefundRecord>();
    }

    public void SetShippingAddress(Entities.Identity.UserAddress address) => ShippingAddress = address;
    public void SetBillingAddress(Entities.Identity.UserAddress address) => BillingAddress = address;

    public void SetPayments(IReadOnlyList<Entities.Payment.PaymentRecord> payments) => Payments = payments;
    public void SetShipments(IReadOnlyList<Entities.Shipping.Shipment> shipments) => Shipments = shipments;
    public void SetRefunds(IReadOnlyList<Entities.Payment.RefundRecord> refunds) => Refunds = refunds;

    public decimal TotalPaid => Payments.Where(p => p.Status == PaymentStatus.Captured).Sum(p => p.Amount);
    public decimal TotalRefunded => Refunds.Where(r => r.Status == RefundStatus.Completed).Sum(r => r.Amount);
    public decimal OutstandingBalance => Order.GrandTotal - TotalPaid + TotalRefunded;
    public bool IsFullyPaid => TotalPaid >= Order.GrandTotal;
    public bool HasRefunds => Refunds.Any(r => r.Status == RefundStatus.Completed);
    public bool IsFullyShipped => Items.All(i => i.Status == OrderItemStatus.Shipped || i.Status == OrderItemStatus.Delivered);
    public bool IsFullyDelivered => Items.All(i => i.Status == OrderItemStatus.Delivered);
    public string LatestTrackingNumber => Shipments.OrderByDescending(s => s.CreatedAt).FirstOrDefault()?.TrackingNumber ?? string.Empty;

    public Entities.Shipping.Shipment? GetActiveShipment()
    {
        return Shipments.FirstOrDefault(s =>
            s.Status != ShippingStatus.Delivered &&
            s.Status != ShippingStatus.Returned &&
            s.Status != ShippingStatus.Failed);
    }
}

public class ShoppingCartAggregate
{
    public Entities.Cart.ShoppingCart Cart { get; private set; }
    public IReadOnlyList<Entities.Cart.CartItem> Items => Cart.Items.ToList();
    public Entities.Identity.UserAddress? ShippingAddress { get; private set; }
    public Entities.Marketing.Coupon? AppliedCoupon { get; private set; }
    public IReadOnlyList<Entities.Shipping.ShippingRate> AvailableShippingRates { get; private set; }

    public ShoppingCartAggregate(Entities.Cart.ShoppingCart cart)
    {
        Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        AvailableShippingRates = new List<Entities.Shipping.ShippingRate>();
    }

    public void SetShippingAddress(Entities.Identity.UserAddress address) => ShippingAddress = address;
    public void SetAppliedCoupon(Entities.Marketing.Coupon coupon) => AppliedCoupon = coupon;

    public void SetAvailableShippingRates(IReadOnlyList<Entities.Shipping.ShippingRate> rates)
    {
        AvailableShippingRates = rates;
    }

    public decimal Subtotal => Items.Where(i => i.IsAvailable).Sum(i => i.LineTotal);
    public decimal Discount => AppliedCoupon?.CalculateDiscount(Subtotal) ?? 0;
    public decimal TotalWeight => Items.Where(i => i.IsAvailable && i.Weight.HasValue).Sum(i => i.Weight!.Value * i.Quantity);
    public int TotalItems => Items.Sum(i => i.Quantity);
    public bool IsValid => Items.Count > 0 && Items.All(i => i.IsAvailable);
}
