namespace Ecommerce.Infrastructure.Services;

public class ExportService : IExportService
{
    private readonly ICacheService _cacheService;

    public ExportService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<byte[]> ExportToCsvAsync<T>(List<T> data, List<string> columns, CancellationToken ct = default)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(string.Join(",", columns));
        foreach (var item in data)
        {
            var type = typeof(T);
            var values = columns.Select(c =>
            {
                var prop = type.GetProperty(c);
                var value = prop?.GetValue(item)?.ToString() ?? string.Empty;
                return $"\"{value.Replace("\"", "\"\"")}\"";
            });
            sb.AppendLine(string.Join(",", values));
        }
        await Task.CompletedTask;
        return System.Text.Encoding.UTF8.GetBytes(sb.ToString());
    }

    public async Task<byte[]> ExportToExcelAsync<T>(List<T> data, List<string> columns, string sheetName = "Sheet1", CancellationToken ct = default)
    {
        var csvBytes = await ExportToCsvAsync(data, columns, ct);
        await Task.CompletedTask;
        return csvBytes;
    }

    public async Task<byte[]> ExportToPdfAsync(string htmlContent, CancellationToken ct = default)
    {
        await Task.CompletedTask;
        return System.Text.Encoding.UTF8.GetBytes(htmlContent);
    }

    public async Task<string> GenerateExportUrlAsync(string reportType, Dictionary<string, string> parameters, TimeSpan? expiry = null)
    {
        var id = Guid.NewGuid().ToString("N");
        await _cacheService.SetAsync($"export:{id}", new { ReportType = reportType, Parameters = parameters }, expiry ?? TimeSpan.FromHours(1));
        return $"/api/exports/{id}/download";
    }

    public async Task<List<string>> GetAvailableReportTypesAsync()
    {
        await Task.CompletedTask;
        return
        [
            "sales", "orders", "products", "customers", "inventory",
            "revenue", "shipping", "reviews", "coupons", "audit"
        ];
    }
}
