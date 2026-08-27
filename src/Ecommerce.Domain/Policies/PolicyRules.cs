using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.User;

namespace Ecommerce.Domain.Policies;

public interface IPricingPolicy
{
    decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context);
    string PolicyName { get; }
}

public interface IStockPolicy
{
    bool CanAddToCart(Product product, int requestedQuantity, int currentCartQuantity);
    int GetMaxAllowedQuantity(Product product);
    string PolicyName { get; }
}

public interface IOrderPolicy
{
    bool CanPlaceOrder(Order order);
    bool CanCancelOrder(Order order);
    bool CanRefundOrder(Order order);
    string PolicyName { get; }
}

public interface IShippingPolicy
{
    decimal CalculateShippingCost(decimal subtotal, decimal weight, string destination);
    int GetEstimatedDeliveryDays(string method, string destination);
    bool IsEligibleForFreeShipping(decimal subtotal);
    string PolicyName { get; }
}

public interface ICouponPolicy
{
    bool CanApplyCoupon(Entities.Marketing.Coupon coupon, Order order);
    decimal CalculateDiscount(Entities.Marketing.Coupon coupon, decimal orderTotal);
    string PolicyName { get; }
}

public interface IReturnPolicy
{
    bool CanReturnOrder(Order order, int daysSinceDelivery);
    bool CanExchangeOrder(Order order, int daysSinceDelivery);
    int GetReturnWindowDays();
    string PolicyName { get; }
}

public class StandardPricingPolicy : IPricingPolicy
{
    public string PolicyName => "Standard";
    public decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context) => basePrice * quantity;
}

public class MemberDiscountPricingPolicy : IPricingPolicy
{
    public string PolicyName => "MemberDiscount";
    public decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context)
    {
        var discount = context.CustomerTier?.ToLowerInvariant() switch
        {
            "gold" => 0.15m,
            "silver" => 0.10m,
            "bronze" => 0.05m,
            _ => 0m
        };
        return basePrice * quantity * (1 - discount);
    }
}

public class StandardStockPolicy : IStockPolicy
{
    public string PolicyName => "Standard";
    public bool CanAddToCart(Product product, int requestedQuantity, int currentCartQuantity)
    {
        return product.StockQuantity >= (requestedQuantity + currentCartQuantity);
    }
    public int GetMaxAllowedQuantity(Product product) => product.StockQuantity;
}

public class PerOrderStockPolicy : IStockPolicy
{
    public string PolicyName => "PerOrder";
    public bool CanAddToCart(Product product, int requestedQuantity, int currentCartQuantity)
    {
        return requestedQuantity <= 10 && product.StockQuantity >= requestedQuantity;
    }
    public int GetMaxAllowedQuantity(Product product) => Math.Min(product.StockQuantity, 10);
}

public class StandardOrderPolicy : IOrderPolicy
{
    public string PolicyName => "Standard";
    public bool CanPlaceOrder(Order order) => order.Items.Any() && order.TotalAmount > 0;
    public bool CanCancelOrder(Order order) => order.Status == OrderStatus.Pending || order.Status == OrderStatus.Processing;
    public bool CanRefundOrder(Order order) => order.PaymentStatus == PaymentStatus.Paid && order.Status != OrderStatus.Cancelled;
}

public class StandardShippingPolicy : IShippingPolicy
{
    public string PolicyName => "Standard";
    public decimal CalculateShippingCost(decimal subtotal, decimal weight, string destination)
    {
        if (subtotal >= 50) return 0;
        return weight > 5 ? 14.99m : 9.99m;
    }
    public int GetEstimatedDeliveryDays(string method, string destination) => method.ToLowerInvariant() switch
    {
        "express" => 2,
        "overnight" => 1,
        _ => 5
    };
    public bool IsEligibleForFreeShipping(decimal subtotal) => subtotal >= 50;
}

public class StandardCouponPolicy : ICouponPolicy
{
    public string PolicyName => "Standard";
    public bool CanApplyCoupon(Entities.Marketing.Coupon coupon, Order order)
    {
        return coupon.IsValid &&
               (!coupon.MinimumOrderAmount.HasValue || order.SubTotal >= coupon.MinimumOrderAmount.Value);
    }
    public decimal CalculateDiscount(Entities.Marketing.Coupon coupon, decimal orderTotal)
    {
        return coupon.DiscountType switch
        {
            Entities.Marketing.DiscountType.Percentage => Math.Min(orderTotal * coupon.DiscountValue / 100, coupon.MaximumDiscountAmount ?? decimal.MaxValue),
            Entities.Marketing.DiscountType.FixedAmount => Math.Min(coupon.DiscountValue, orderTotal),
            _ => 0
        };
    }
}

public class StandardReturnPolicy : IReturnPolicy
{
    public string PolicyName => "Standard";
    public int GetReturnWindowDays() => 30;
    public bool CanReturnOrder(Order order, int daysSinceDelivery)
    {
        return order.Status == OrderStatus.Delivered && daysSinceDelivery <= GetReturnWindowDays();
    }
    public bool CanExchangeOrder(Order order, int daysSinceDelivery)
    {
        return order.Status == OrderStatus.Delivered && daysSinceDelivery <= GetReturnWindowDays();
    }
}
