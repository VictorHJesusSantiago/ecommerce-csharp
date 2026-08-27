namespace Ecommerce.Application.Strategies;

public interface IPricingStrategy
{
    decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context);
    string StrategyName { get; }
}

public class PricingContext
{
    public string? CustomerTier { get; set; }
    public bool IsBulkOrder { get; set; }
    public string? CouponCode { get; set; }
    public decimal? CouponDiscount { get; set; }
    public bool IsWholesale { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}

public class StandardPricingStrategy : IPricingStrategy
{
    public string StrategyName => "Standard";

    public decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context)
    {
        return basePrice * quantity;
    }
}

public class BulkPricingStrategy : IPricingStrategy
{
    public string StrategyName => "Bulk";

    public decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context)
    {
        if (quantity >= 100)
            return basePrice * quantity * 0.75m; // 25% discount
        if (quantity >= 50)
            return basePrice * quantity * 0.85m; // 15% discount
        if (quantity >= 20)
            return basePrice * quantity * 0.90m; // 10% discount
        if (quantity >= 10)
            return basePrice * quantity * 0.95m; // 5% discount

        return basePrice * quantity;
    }
}

public class MembershipPricingStrategy : IPricingStrategy
{
    public string StrategyName => "Membership";

    public decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context)
    {
        var discount = context.CustomerTier?.ToLowerInvariant() switch
        {
            "gold" => 0.15m,
            "silver" => 0.10m,
            "bronze" => 0.05m,
            _ => 0m
        };

        var total = basePrice * quantity;
        return total * (1 - discount);
    }
}

public class WholesalePricingStrategy : IPricingStrategy
{
    public string StrategyName => "Wholesale";

    public decimal CalculatePrice(decimal basePrice, int quantity, PricingContext context)
    {
        if (!context.IsWholesale) return basePrice * quantity;

        var wholesaleDiscount = 0.20m;
        return basePrice * quantity * (1 - wholesaleDiscount);
    }
}

public class PricingStrategyFactory
{
    private readonly Dictionary<string, IPricingStrategy> _strategies;

    public PricingStrategyFactory()
    {
        _strategies = new Dictionary<string, IPricingStrategy>(StringComparer.OrdinalIgnoreCase)
        {
            ["standard"] = new StandardPricingStrategy(),
            ["bulk"] = new BulkPricingStrategy(),
            ["membership"] = new MembershipPricingStrategy(),
            ["wholesale"] = new WholesalePricingStrategy()
        };
    }

    public IPricingStrategy GetStrategy(string strategyName)
    {
        return _strategies.TryGetValue(strategyName, out var strategy)
            ? strategy
            : _strategies["standard"];
    }

    public IPricingStrategy GetBestStrategy(PricingContext context, decimal basePrice, int quantity)
    {
        var prices = _strategies.Values
            .Select(s => new { Strategy = s, Price = s.CalculatePrice(basePrice, quantity, context) })
            .OrderBy(x => x.Price)
            .ToList();

        return prices.First().Strategy;
    }
}
