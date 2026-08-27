using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.PolicyRules;

public interface IPricingPolicy
{
    decimal CalculatePrice(Product product, int quantity);
    decimal CalculateDiscountedPrice(Product product, int quantity, decimal discountPercentage);
    bool ValidatePrice(decimal price, string currency);
}

public class StandardPricingPolicy : IPricingPolicy
{
    public decimal CalculatePrice(Product product, int quantity)
    {
        if (quantity <= 0) return 0;
        return product.Price * quantity;
    }

    public decimal CalculateDiscountedPrice(Product product, int quantity, decimal discountPercentage)
    {
        var basePrice = CalculatePrice(product, quantity);
        var discount = basePrice * discountPercentage / 100;
        return Math.Max(0, basePrice - discount);
    }

    public bool ValidatePrice(decimal price, string currency)
    {
        return price >= 0 && !string.IsNullOrWhiteSpace(currency);
    }
}

public interface IStockPolicy
{
    bool CanOrder(ProductVariant variant, int requestedQuantity);
    int GetMaxOrderableQuantity(ProductVariant variant);
    bool ShouldNotifyLowStock(ProductVariant variant);
    bool ShouldNotifyOutOfStock(ProductVariant variant);
}

public class DefaultStockPolicy : IStockPolicy
{
    public int LowStockThreshold { get; set; } = 5;

    public bool CanOrder(ProductVariant variant, int requestedQuantity)
    {
        if (variant.Product?.AllowBackorder == true) return true;
        return variant.StockQuantity >= requestedQuantity;
    }

    public int GetMaxOrderableQuantity(ProductVariant variant)
    {
        if (variant.Product?.AllowBackorder == true) return int.MaxValue;
        return variant.StockQuantity;
    }

    public bool ShouldNotifyLowStock(ProductVariant variant)
    {
        return variant.StockQuantity > 0 && variant.StockQuantity <= LowStockThreshold;
    }

    public bool ShouldNotifyOutOfStock(ProductVariant variant)
    {
        return variant.StockQuantity <= 0;
    }
}

public interface IOrderPolicy
{
    bool CanCancelOrder(Order order);
    bool CanRefundOrder(Order order, decimal amount);
    bool CanModifyOrder(Order order);
    bool CanReturnOrder(Order order);
    TimeSpan GetCancellationWindow();
    TimeSpan GetReturnWindow();
}

public class DefaultOrderPolicy : IOrderPolicy
{
    public TimeSpan CancellationWindow { get; set; } = TimeSpan.FromHours(24);
    public TimeSpan ReturnWindow { get; set; } = TimeSpan.FromDays(30);

    public bool CanCancelOrder(Order order)
    {
        return order.Status is OrderStatus.Pending or OrderStatus.Confirmed;
    }

    public bool CanRefundOrder(Order order, decimal amount)
    {
        if (order.Status != OrderStatus.Delivered && order.Status != OrderStatus.Completed)
            return false;
        var totalRefunded = order.Items.Sum(i => i.RefundAmount ?? 0);
        return totalRefunded + amount <= order.GrandTotal;
    }

    public bool CanModifyOrder(Order order)
    {
        return order.Status == OrderStatus.Pending;
    }

    public bool CanReturnOrder(Order order)
    {
        if (order.Status != OrderStatus.Delivered) return false;
        if (!order.DeliveredAt.HasValue) return false;
        return DateTime.UtcNow - order.DeliveredAt.Value <= ReturnWindow;
    }

    public TimeSpan GetCancellationWindow() => CancellationWindow;
    public TimeSpan GetReturnWindow() => ReturnWindow;
}

public interface IShippingPolicy
{
    bool IsEligibleForFreeShipping(decimal orderTotal, string country);
    decimal CalculateShippingCost(decimal weight, string method, string country);
    bool SupportsCountry(string country);
    List<string> GetSupportedCountries();
}

public class DefaultShippingPolicy : IShippingPolicy
{
    public decimal FreeShippingThreshold { get; set; } = 50m;
    public decimal StandardShippingRate { get; set; } = 5.99m;
    public decimal ExpressShippingRate { get; set; } = 12.99m;
    public decimal PerKgRate { get; set; } = 1.50m;
    private readonly HashSet<string> _supportedCountries = ["US", "CA", "GB", "AU", "DE", "FR", "JP"];

    public bool IsEligibleForFreeShipping(decimal orderTotal, string country)
    {
        return orderTotal >= FreeShippingThreshold && _supportedCountries.Contains(country);
    }

    public decimal CalculateShippingCost(decimal weight, string method, string country)
    {
        if (IsEligigleForFreeShipping(weight * StandardShippingRate + StandardShippingRate, country))
            return 0;

        return method.ToLowerInvariant() switch
        {
            "express" => ExpressShippingRate + (weight * PerKgRate * 1.5m),
            "overnight" => ExpressShippingRate * 2 + (weight * PerKgRate * 2),
            _ => StandardShippingRate + (weight * PerKgRate)
        };
    }

    public bool SupportsCountry(string country) => _supportedCountries.Contains(country);
    public List<string> GetSupportedCountries() => _supportedCountries.ToList();
}

public interface ICouponPolicy
{
    bool CanBeCombined(Coupon coupon, Coupon existingCoupon);
    bool CanBeAppliedToOrder(Coupon coupon, decimal orderAmount, int customerOrderCount);
    decimal CalculateMaximumDiscount(Coupon coupon, decimal orderAmount);
}

public class DefaultCouponPolicy : ICouponPolicy
{
    public bool CanBeCombined(Coupon coupon, Coupon existingCoupon)
    {
        if (!coupon.CombineWithOtherCoupons) return false;
        if (!existingCoupon.CombineWithOtherCoupons) return false;
        return true;
    }

    public bool CanBeAppliedToOrder(Coupon coupon, decimal orderAmount, int customerOrderCount)
    {
        if (!coupon.IsValid()) return false;
        if (coupon.MinimumOrderAmount.HasValue && orderAmount < coupon.MinimumOrderAmount.Value)
            return false;
        if (coupon.UsageLimit.HasValue && coupon.TimesUsed >= coupon.UsageLimit.Value)
            return false;
        return true;
    }

    public decimal CalculateMaximumDiscount(Coupon coupon, decimal orderAmount)
    {
        var discount = coupon.CalculateDiscount(orderAmount);
        if (coupon.MaximumDiscountAmount.HasValue)
            return Math.Min(discount, coupon.MaximumDiscountAmount.Value);
        return discount;
    }
}

public interface IReturnPolicy
{
    bool IsEligibleForReturn(OrderItem item, DateTime deliveredDate);
    decimal CalculateRefundAmount(OrderItem item, int returnQuantity);
    bool RequiresReturnApproval(OrderItem item);
    List<string> GetReturnConditions();
}

public class DefaultReturnPolicy : IReturnPolicy
{
    public TimeSpan ReturnWindow { get; set; } = TimeSpan.FromDays(30);

    public bool IsEligibleForReturn(OrderItem item, DateTime deliveredDate)
    {
        if (item.Status != OrderItemStatus.Delivered) return false;
        if (DateTime.UtcNow - deliveredDate > ReturnWindow) return false;
        var returnedQuantity = item.QuantityRefunded;
        return returnedQuantity < item.Quantity;
    }

    public decimal CalculateRefundAmount(OrderItem item, int returnQuantity)
    {
        if (returnQuantity <= 0 || returnQuantity > item.Quantity - item.QuantityRefunded)
            return 0;
        return item.UnitPrice * returnQuantity;
    }

    public bool RequiresReturnApproval(OrderItem item)
    {
        return item.LineTotal > 100m;
    }

    public List<string> GetReturnConditions()
    {
        return
        [
            "Item must be returned within 30 days of delivery",
            "Item must be in original condition",
            "Item must include all original packaging and accessories",
            "Digital products are not eligible for return",
            "Custom or personalized items are not eligible for return",
            "Items marked as final sale are not eligible for return"
        ];
    }
}
