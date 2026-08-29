using Xunit;
using FluentAssertions;

namespace Ecommerce.UnitTests;

public class PolicyTests
{
    [Fact]
    public void StandardPricingPolicy_ShouldCalculateCorrectly()
    {
        var policy = new Ecommerce.Domain.Policies.StandardPricingPolicy();
        var price = policy.CalculatePrice(100m, 1);
        price.Should().Be(100m);
    }

    [Fact]
    public void StandardPricingPolicy_ShouldApplyQuantityDiscount()
    {
        var policy = new Ecommerce.Domain.Policies.StandardPricingPolicy();
        var price = policy.CalculatePrice(100m, 10);
        price.Should().BeLessThan(1000m);
    }

    [Fact]
    public void StandardStockPolicy_ShouldAllowPurchaseWhenInStock()
    {
        var policy = new Ecommerce.Domain.Policies.StandardStockPolicy();
        var result = policy.CanPurchase(100, 5);
        result.Should().BeTrue();
    }

    [Fact]
    public void StandardStockPolicy_ShouldDenyPurchaseWhenOutOfStock()
    {
        var policy = new Ecommerce.Domain.Policies.StandardStockPolicy();
        var result = policy.CanPurchase(0, 1);
        result.Should().BeFalse();
    }

    [Fact]
    public void StandardStockPolicy_ShouldDenyPurchaseWhenExceedingStock()
    {
        var policy = new Ecommerce.Domain.Policies.StandardStockPolicy();
        var result = policy.CanPurchase(5, 10);
        result.Should().BeFalse();
    }

    [Fact]
    public void StandardOrderPolicy_ShouldAllowCancelWhenPending()
    {
        var policy = new Ecommerce.Domain.Policies.StandardOrderPolicy();
        var result = policy.CanCancel(Ecommerce.Domain.Enums.OrderStatus.Pending);
        result.Should().BeTrue();
    }

    [Fact]
    public void StandardOrderPolicy_ShouldAllowCancelWhenProcessing()
    {
        var policy = new Ecommerce.Domain.Policies.StandardOrderPolicy();
        var result = policy.CanCancel(Ecommerce.Domain.Enums.OrderStatus.Processing);
        result.Should().BeTrue();
    }

    [Fact]
    public void StandardOrderPolicy_ShouldDenyCancelWhenShipped()
    {
        var policy = new Ecommerce.Domain.Policies.StandardOrderPolicy();
        var result = policy.CanCancel(Ecommerce.Domain.Enums.OrderStatus.Shipped);
        result.Should().BeFalse();
    }

    [Fact]
    public void StandardOrderPolicy_ShouldDenyCancelWhenDelivered()
    {
        var policy = new Ecommerce.Domain.Policies.StandardOrderPolicy();
        var result = policy.CanCancel(Ecommerce.Domain.Enums.OrderStatus.Delivered);
        result.Should().BeFalse();
    }

    [Fact]
    public void StandardOrderPolicy_ShouldAllowRefundWhenDelivered()
    {
        var policy = new Ecommerce.Domain.Policies.StandardOrderPolicy();
        var result = policy.CanRefund(Ecommerce.Domain.Enums.OrderStatus.Delivered);
        result.Should().BeTrue();
    }

    [Fact]
    public void StandardCouponPolicy_ShouldValidateActiveCoupon()
    {
        var policy = new Ecommerce.Domain.Policies.StandardCouponPolicy();
        var result = policy.IsValid(true, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30), 50m, 100);
        result.Should().BeTrue();
    }

    [Fact]
    public void StandardCouponPolicy_ShouldRejectExpiredCoupon()
    {
        var policy = new Ecommerce.Domain.Policies.StandardCouponPolicy();
        var result = policy.IsValid(true, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(-7), 50m, 100);
        result.Should().BeFalse();
    }

    [Fact]
    public void StandardCouponPolicy_ShouldRejectUsageExceeded()
    {
        var policy = new Ecommerce.Domain.Policies.StandardCouponPolicy();
        var result = policy.IsValid(true, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30), 50m, 100);
        result.Should().BeTrue();
        var resultExceeded = policy.IsValid(true, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30), 50m, 50, 50);
        resultExceeded.Should().BeFalse();
    }

    [Fact]
    public void StandardShippingPolicy_ShouldCalculateStandardShipping()
    {
        var policy = new Ecommerce.Domain.Policies.StandardShippingPolicy();
        var cost = policy.CalculateShipping(5m, "US", 50m);
        cost.Should().BeGreaterThan(0);
    }

    [Fact]
    public void StandardShippingPolicy_ShouldOfferFreeShippingAboveThreshold()
    {
        var policy = new Ecommerce.Domain.Policies.StandardShippingPolicy();
        var cost = policy.CalculateShipping(5m, "US", 100m);
        cost.Should().Be(0m);
    }

    [Fact]
    public void StandardReturnPolicy_ShouldAllowReturnWithin30Days()
    {
        var policy = new Ecommerce.Domain.Policies.StandardReturnPolicy();
        var orderDate = DateTime.UtcNow.AddDays(-15);
        var result = policy.CanReturn(orderDate);
        result.Should().BeTrue();
    }

    [Fact]
    public void StandardReturnPolicy_ShouldDenyReturnAfter30Days()
    {
        var policy = new Ecommerce.Domain.Policies.StandardReturnPolicy();
        var orderDate = DateTime.UtcNow.AddDays(-45);
        var result = policy.CanReturn(orderDate);
        result.Should().BeFalse();
    }
}
