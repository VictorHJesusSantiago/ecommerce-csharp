using FluentAssertions;
using Xunit;
using Ecommerce.Domain.Policies;
using Ecommerce.Domain.Entities.Ordering;
using Ecommerce.Domain.Entities.Catalog;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.ArchitectureTests;

public class PolicyTests
{
    private readonly StandardPricingPolicy _pricingPolicy = new();
    private readonly StandardStockPolicy _stockPolicy = new();
    private readonly StandardOrderPolicy _orderPolicy = new();
    private readonly StandardShippingPolicy _shippingPolicy = new();
    private readonly StandardCouponPolicy _couponPolicy = new();
    private readonly StandardReturnPolicy _returnPolicy = new();

    [Fact]
    public void PricingPolicy_CalculatePrice_ShouldApplyDiscount()
    {
        var originalPrice = new Money(100m, "USD");
        var result = _pricingPolicy.CalculatePrice(originalPrice, 0.1m);
        result.Amount.Should().Be(90m);
    }

    [Fact]
    public void PricingPolicy_CalculatePrice_ZeroDiscount_ShouldReturnSamePrice()
    {
        var price = new Money(50m, "USD");
        var result = _pricingPolicy.CalculatePrice(price, 0m);
        result.Amount.Should().Be(50m);
    }

    [Fact]
    public void PricingPolicy_CalculatePrice_FullDiscount_ShouldReturnZero()
    {
        var price = new Money(100m, "USD");
        var result = _pricingPolicy.CalculatePrice(price, 1m);
        result.Amount.Should().Be(0m);
    }

    [Fact]
    public void StockPolicy_IsInStock_ShouldReturnTrue_WhenQuantityPositive()
    {
        _stockPolicy.IsInStock(10).Should().BeTrue();
    }

    [Fact]
    public void StockPolicy_IsInStock_ShouldReturnFalse_WhenQuantityZero()
    {
        _stockPolicy.IsInStock(0).Should().BeFalse();
    }

    [Fact]
    public void StockPolicy_IsLowStock_ShouldReturnTrue_WhenBelowThreshold()
    {
        _stockPolicy.IsLowStock(3, 10).Should().BeTrue();
    }

    [Fact]
    public void StockPolicy_IsLowStock_ShouldReturnFalse_WhenAboveThreshold()
    {
        _stockPolicy.IsLowStock(15, 10).Should().BeFalse();
    }

    [Fact]
    public void StockPolicy_CanFulfillOrder_ShouldReturnTrue_WhenSufficientStock()
    {
        _stockPolicy.CanFulfillOrder(10, 5).Should().BeTrue();
    }

    [Fact]
    public void StockPolicy_CanFulfillOrder_ShouldReturnFalse_WhenInsufficientStock()
    {
        _stockPolicy.CanFulfillOrder(3, 5).Should().BeFalse();
    }

    [Fact]
    public void OrderPolicy_CanCancel_ShouldReturnTrue_WhenPending()
    {
        _orderPolicy.CanCancel(OrderStatus.Pending).Should().BeTrue();
    }

    [Fact]
    public void OrderPolicy_CanCancel_ShouldReturnFalse_WhenShipped()
    {
        _orderPolicy.CanCancel(OrderStatus.Shipped).Should().BeFalse();
    }

    [Fact]
    public void OrderPolicy_CanReturn_ShouldReturnTrue_WhenDelivered()
    {
        _orderPolicy.CanReturn(OrderStatus.Delivered, DateTime.UtcNow.AddDays(-5)).Should().BeTrue();
    }

    [Fact]
    public void OrderPolicy_CanReturn_ShouldReturnFalse_WhenTooLate()
    {
        _orderPolicy.CanReturn(OrderStatus.Delivered, DateTime.UtcNow.AddDays(-40)).Should().BeFalse();
    }

    [Fact]
    public void OrderPolicy_CanModify_ShouldReturnTrue_WhenProcessing()
    {
        _orderPolicy.CanModify(OrderStatus.Processing).Should().BeTrue();
    }

    [Fact]
    public void OrderPolicy_CanModify_ShouldReturnFalse_WhenShipped()
    {
        _orderPolicy.CanModify(OrderStatus.Shipped).Should().BeFalse();
    }

    [Fact]
    public void ShippingPolicy_GetEstimatedDays_ShouldReturnCorrectRange()
    {
        var (min, max) = _shippingPolicy.GetEstimatedDays("standard");
        min.Should().BeGreaterThan(0);
        max.Should().BeGreaterOrEqualTo(min);
    }

    [Fact]
    public void CouponPolicy_IsValid_ShouldReturnTrue_WhenAllConditionsMet()
    {
        var coupon = new Coupon
        {
            Code = "SAVE10",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 10,
            MinimumOrderAmount = 50,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(1),
            UsageLimit = 100,
            UsedCount = 5,
            IsActive = true
        };
        _couponPolicy.IsValid(coupon, 100m, 3).Should().BeTrue();
    }

    [Fact]
    public void CouponPolicy_IsValid_ShouldReturnFalse_WhenExpired()
    {
        var coupon = new Coupon
        {
            Code = "EXPIRED",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 10,
            EndDate = DateTime.UtcNow.AddDays(-1),
            IsActive = true
        };
        _couponPolicy.IsValid(coupon, 100m, 1).Should().BeFalse();
    }

    [Fact]
    public void CouponPolicy_IsValid_ShouldReturnFalse_WhenUsageLimitExceeded()
    {
        var coupon = new Coupon
        {
            Code = "USED",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 10,
            UsageLimit = 5,
            UsedCount = 5,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(1),
            IsActive = true
        };
        _couponPolicy.IsValid(coupon, 100m, 1).Should().BeFalse();
    }

    [Fact]
    public void CouponPolicy_IsValid_ShouldReturnFalse_WhenBelowMinimum()
    {
        var coupon = new Coupon
        {
            Code = "MIN100",
            DiscountType = DiscountType.Percentage,
            DiscountValue = 10,
            MinimumOrderAmount = 100,
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(1),
            UsageLimit = 100,
            UsedCount = 0,
            IsActive = true
        };
        _couponPolicy.IsValid(coupon, 50m, 1).Should().BeFalse();
    }

    [Fact]
    public void ReturnPolicy_IsEligible_ShouldReturnTrue_Within30Days()
    {
        _returnPolicy.IsEligible(DateTime.UtcNow.AddDays(-15)).Should().BeTrue();
    }

    [Fact]
    public void ReturnPolicy_IsEligible_ShouldReturnFalse_After30Days()
    {
        _returnPolicy.IsEligible(DateTime.UtcNow.AddDays(-45)).Should().BeFalse();
    }
}
