using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalCurrencyConversionService : ICurrencyConversionService
{
    private readonly ILogger<LocalCurrencyConversionService> _logger;
    private readonly Dictionary<string, decimal> _rates = new()
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

    public LocalCurrencyConversionService(ILogger<LocalCurrencyConversionService> logger)
    {
        _logger = logger;
    }

    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        if (fromCurrency == toCurrency) return amount;
        if (!_rates.TryGetValue(fromCurrency.ToUpper(), out var fromRate)) throw new ArgumentException($"Unsupported currency: {fromCurrency}");
        if (!_rates.TryGetValue(toCurrency.ToUpper(), out var toRate)) throw new ArgumentException($"Unsupported currency: {toCurrency}");

        var usdAmount = amount / fromRate;
        var result = usdAmount * toRate;
        _logger.LogDebug("Converted {Amount} {From} to {Result} {To}", amount, fromCurrency, result, toCurrency);
        return Math.Round(result, 2);
    }

    public decimal GetRate(string fromCurrency, string toCurrency)
    {
        if (fromCurrency == toCurrency) return 1.0m;
        if (!_rates.TryGetValue(fromCurrency.ToUpper(), out var fromRate)) throw new ArgumentException($"Unsupported currency: {fromCurrency}");
        if (!_rates.TryGetValue(toCurrency.ToUpper(), out var toRate)) throw new ArgumentException($"Unsupported currency: {toCurrency}");
        return Math.Round(toRate / fromRate, 4);
    }

    public List<string> GetSupportedCurrencies() => _rates.Keys.ToList();
}

public interface ICurrencyConversionService
{
    decimal Convert(decimal amount, string fromCurrency, string toCurrency);
    decimal GetRate(string fromCurrency, string toCurrency);
    List<string> GetSupportedCurrencies();
}
