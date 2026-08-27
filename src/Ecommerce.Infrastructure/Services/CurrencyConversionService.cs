namespace Ecommerce.Infrastructure.Services;

public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly ICacheService _cacheService;
    private readonly Dictionary<string, decimal> _exchangeRates = new()
    {
        ["USD"] = 1.0m,
        ["EUR"] = 0.92m,
        ["GBP"] = 0.79m,
        ["CAD"] = 1.36m,
        ["AUD"] = 1.53m,
        ["JPY"] = 149.50m,
        ["CNY"] = 7.24m,
        ["INR"] = 83.12m,
        ["BRL"] = 4.97m,
        ["MXN"] = 17.15m
    };

    public CurrencyConversionService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
    {
        if (fromCurrency == toCurrency) return amount;

        var rates = await GetExchangeRatesAsync();
        if (!rates.TryGetValue(fromCurrency.ToUpper(), out var fromRate) ||
            !rates.TryGetValue(toCurrency.ToUpper(), out var toRate))
        {
            throw new InvalidOperationException($"Unsupported currency: {fromCurrency} or {toCurrency}");
        }

        var usdAmount = amount / fromRate;
        return Math.Round(usdAmount * toRate, 2);
    }

    public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string baseCurrency = "USD")
    {
        var cacheKey = $"exchange-rates:{baseCurrency}";
        var cached = await _cacheService.GetAsync<Dictionary<string, decimal>>(cacheKey);
        if (cached != null) return cached;

        var rates = new Dictionary<string, decimal>();
        foreach (var kvp in _exchangeRates)
        {
            rates[kvp.Key] = Math.Round(kvp.Value / _exchangeRates[baseCurrency.ToUpper()], 6);
        }

        await _cacheService.SetAsync(cacheKey, rates, TimeSpan.FromHours(1));
        return rates;
    }

    public async Task<List<string>> GetSupportedCurrenciesAsync()
    {
        await Task.CompletedTask;
        return _exchangeRates.Keys.ToList();
    }

    public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
    {
        var rates = await GetExchangeRatesAsync(fromCurrency);
        return rates.TryGetValue(toCurrency.ToUpper(), out var rate) ? rate : 0;
    }
}
