namespace Ecommerce.Application.Strategies;

public interface IPricingStrategy
{
    decimal CalculatePrice(Product product, int quantity, Dictionary<string, string>? options = null);
    decimal CalculateDiscountedPrice(Product product, int quantity, decimal discountPercentage);
    bool IsStrategyApplicable(Product product);
}

public class StandardPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(Product product, int quantity, Dictionary<string, string>? options = null)
    {
        return product.Price * quantity;
    }

    public decimal CalculateDiscountedPrice(Product product, int quantity, decimal discountPercentage)
    {
        var basePrice = CalculatePrice(product, quantity);
        return basePrice - (basePrice * discountPercentage / 100);
    }

    public bool IsStrategyApplicable(Product product) => true;
}

public class BulkPricingStrategy : IPricingStrategy
{
    private readonly Dictionary<int, decimal> _tierDiscounts = new()
    {
        [10] = 5m,
        [25] = 10m,
        [50] = 15m,
        [100] = 20m,
        [500] = 25m
    };

    public decimal CalculatePrice(Product product, int quantity, Dictionary<string, string>? options = null)
    {
        var basePrice = product.Price * quantity;
        var discountPercentage = GetBulkDiscount(quantity);
        return basePrice - (basePrice * discountPercentage / 100);
    }

    public decimal CalculateDiscountedPrice(Product product, int quantity, decimal discountPercentage)
    {
        var basePrice = CalculatePrice(product, quantity);
        return basePrice - (basePrice * discountPercentage / 100);
    }

    public bool IsStrategyApplicable(Product product) => product.AllowBackorder || product.TotalSold > 100;

    private decimal GetBulkDiscount(int quantity)
    {
        var applicableTier = _tierDiscounts
            .Where(t => quantity >= t.Key)
            .OrderByDescending(t => t.Key)
            .FirstOrDefault();
        return applicableTier.Value;
    }
}

public class MembershipPricingStrategy : IPricingStrategy
{
    private readonly Dictionary<string, decimal> _memberDiscounts = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Bronze"] = 2m,
        ["Silver"] = 5m,
        ["Gold"] = 10m,
        ["Platinum"] = 15m,
        ["Diamond"] = 20m
    };

    private readonly string _memberTier;

    public MembershipPricingStrategy(string memberTier = "Silver")
    {
        _memberTier = memberTier;
    }

    public decimal CalculatePrice(Product product, int quantity, Dictionary<string, string>? options = null)
    {
        var basePrice = product.Price * quantity;
        var discount = _memberDiscounts.GetValueOrDefault(_memberTier, 0m);
        return basePrice - (basePrice * discount / 100);
    }

    public decimal CalculateDiscountedPrice(Product product, int quantity, decimal discountPercentage)
    {
        var basePrice = CalculatePrice(product, quantity);
        return basePrice - (basePrice * discountPercentage / 100);
    }

    public bool IsStrategyApplicable(Product product) => true;
}

public class PricingStrategyFactory
{
    private readonly Dictionary<string, IPricingStrategy> _strategies = new(StringComparer.OrdinalIgnoreCase);

    public PricingStrategyFactory()
    {
        _strategies["Standard"] = new StandardPricingStrategy();
        _strategies["Bulk"] = new BulkPricingStrategy();
        _strategies["Member_Bronze"] = new MembershipPricingStrategy("Bronze");
        _strategies["Member_Silver"] = new MembershipPricingStrategy("Silver");
        _strategies["Member_Gold"] = new MembershipPricingStrategy("Gold");
        _strategies["Member_Platinum"] = new MembershipPricingStrategy("Platinum");
        _strategies["Member_Diamond"] = new MembershipPricingStrategy("Diamond");
    }

    public IPricingStrategy GetStrategy(string strategyName)
    {
        return _strategies.TryGetValue(strategyName, out var strategy)
            ? strategy
            : _strategies["Standard"];
    }

    public IPricingStrategy GetBestStrategy(Product product, int quantity, string? memberTier = null)
    {
        var strategies = _strategies.Values.Where(s => s.IsStrategyApplicable(product));
        var bestPrice = decimal.MaxValue;
        IPricingStrategy bestStrategy = new StandardPricingStrategy();

        foreach (var strategy in strategies)
        {
            var price = strategy.CalculatePrice(product, quantity);
            if (price < bestPrice)
            {
                bestPrice = price;
                bestStrategy = strategy;
            }
        }

        return bestStrategy;
    }
}
