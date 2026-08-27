namespace Ecommerce.Infrastructure.Services;

public class ShippingCalculatorService : IShippingCalculatorService
{
    public async Task<ShippingCalculationResult> CalculateShippingAsync(CalculateShippingRequest request, CancellationToken ct = default)
    {
        var methods = new List<ShippingMethodDto>();

        if (request.OrderTotal >= 50)
        {
            methods.Add(new ShippingMethodDto
            {
                Id = "free-standard",
                Name = "Free Standard Shipping",
                Carrier = "USPS",
                Cost = 0,
                EstimatedDelivery = TimeSpan.FromDays(7),
                EstimatedDeliveryText = "5-7 business days",
                IsDefault = true
            });
        }

        methods.Add(new ShippingMethodDto
        {
            Id = "standard",
            Name = "Standard Shipping",
            Carrier = "USPS",
            Cost = 9.99m,
            EstimatedDelivery = TimeSpan.FromDays(5),
            EstimatedDeliveryText = "3-5 business days"
        });

        methods.Add(new ShippingMethodDto
        {
            Id = "express",
            Name = "Express Shipping",
            Carrier = "FedEx",
            Cost = 19.99m,
            EstimatedDelivery = TimeSpan.FromDays(2),
            EstimatedDeliveryText = "1-2 business days"
        });

        methods.Add(new ShippingMethodDto
        {
            Id = "overnight",
            Name = "Overnight Shipping",
            Carrier = "FedEx",
            Cost = 39.99m,
            EstimatedDelivery = TimeSpan.FromDays(1),
            EstimatedDeliveryText = "Next business day"
        });

        await Task.CompletedTask;

        return new ShippingCalculationResult
        {
            Methods = methods,
            CheapestCost = methods.Where(m => m.IsAvailable).Min(m => m.Cost),
            FastestDeliveryHours = methods.Where(m => m.IsAvailable).Min(m => m.EstimatedDelivery.TotalHours),
            FreeShippingAvailable = request.OrderTotal >= 50,
            AmountForFreeShipping = request.OrderTotal >= 50 ? 0 : 50 - request.OrderTotal
        };
    }

    public async Task<ShippingRateDto?> GetShippingRateAsync(string carrier, string serviceLevel, decimal weight, string countryCode)
    {
        await Task.CompletedTask;

        return new ShippingRateDto
        {
            Name = $"{carrier} {serviceLevel}",
            Carrier = carrier,
            ServiceLevel = serviceLevel,
            BaseRate = 9.99m,
            PerKgRate = 0.50m,
            EstimatedTransitTime = TimeSpan.FromDays(5),
            IsActive = true
        };
    }

    public async Task<List<ShippingCarrierDto>> GetAvailableCarriersAsync(string countryCode)
    {
        await Task.CompletedTask;

        return
        [
            new() { Code = "USPS", Name = "USPS", IsActive = true, SupportsTracking = true },
            new() { Code = "FedEx", Name = "FedEx", IsActive = true, SupportsTracking = true },
            new() { Code = "UPS", Name = "UPS", IsActive = true, SupportsTracking = true },
            new() { Code = "DHL", Name = "DHL", IsActive = true, SupportsTracking = true }
        ];
    }
}
